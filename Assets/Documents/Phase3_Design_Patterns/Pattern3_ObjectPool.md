# Pattern 3: Object Pool

> *"Improve performance and memory use by reusing objects from a fixed pool instead of allocating and freeing them individually."*  
> — Game Programming Patterns

Reuse objects thay vì tạo/hủy liên tục để tránh GC spikes.

---

## 🎮 Game-Specific Pattern

Đây là pattern **đặc trưng cho game development** — không thường thấy trong enterprise apps.

Từ Game Programming Patterns:
> *"This pattern is used heavily in games for obvious things like game entities and visual effects, but it is also used for less visible data structures such as currently playing sounds."*

---

## Recall Phase 1 & 2 🔙

- **Phase 1 (Task_Weapon)**: Bạn đã biết `Instantiate/Destroy` liên tục gây lag.
- **Phase 2 (Composition)**: Object Pool thường đi kèm với **Factory Pattern** (sắp học) để tạo object ban đầu.

---

## Feature: Bullet Spawning

Game bắn súng spawn hàng trăm bullets mỗi giây.

Vấn đề: `Instantiate()` và `Destroy()` gây lag vì Garbage Collector.

---

## Phần 1: Cách sai — Instantiate/Destroy

```csharp
public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    
    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Destroy(bullet, 3f);  // Destroy after 3 seconds
    }
}
```

### Vấn đề

| Issue | Impact |
|-------|--------|
| Mỗi `Instantiate()` = allocate memory | Memory fragmentation |
| Mỗi `Destroy()` = trigger GC eventually | Frame spikes |
| Nhiều bullets = GC spike = frame drop | Bad gameplay |

---

## Phần 2: Object Pool Pattern

### Cấu trúc

```mermaid
graph TD
    subgraph Pool [Object Pool]
        Obj1[Obj (off)]
        Obj2[Obj (off)]
        Obj3[Obj (off)]
    end

    Game[Game World] 
    Active[Active Objects]

    Pool -- "Get()" --> Game
    Game -- "Activates" --> Active
    Active -- "Return()" --> Pool
```

---

## Phần 3: Implementation — Understanding How It Works 🧠

Để hiểu bản chất, hãy xem cách tự viết một Pool đơn giản bằng `Queue`:

### Generic Object Pool (Conceptual)

```csharp
public class ObjectPool<T> where T : Component
{
    private T prefab;
    private Queue<T> pool = new Queue<T>();
    private Transform parent;
    
    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        
        // Pre-warm: Tạo trước định lượng object
        for (int i = 0; i < initialSize; i++)
        {
            T obj = CreateNew();
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    
    public T Get()
    {
        T obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue(); // Lấy từ hàng đợi
        }
        else
        {
            obj = CreateNew(); // Hết hàng thì tạo mới (Expand pool)
        }
        
        obj.gameObject.SetActive(true);
        return obj;
    }
    
    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj); // Trả về hàng đợi
    }
    
    private T CreateNew()
    {
        return Object.Instantiate(prefab, parent);
    }
}
```

> **Note:** Code trên để học. Trong dự án thật, hãy dùng **Unity Way** ở phần dưới.


## Phần 4: Implementation — The "Unity Way" (Standard) 🏆

Từ Unity 2021+, ta **không cần viết class Pool thủ công** nữa. Hãy dùng `UnityEngine.Pool`.

### 1. Setup Gun script

```csharp
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;

    // Built-in Pool class của Unity
    private IObjectPool<Bullet> bulletPool;

    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(
            createFunc: CreateBullet,
            actionOnGet: OnTakeFromPool,
            actionOnRelease: OnReturnToPool,
            actionOnDestroy: OnDestroyBullet,
            collectionCheck: true, // Check xem có return đúp không (Safety)
            defaultCapacity: 20,
            maxSize: 100
        );
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.SetPool(bulletPool); // Đưa reference pool cho bullet để nó tự trả về
        return bullet;
    }

    private void OnTakeFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
    }

    private void OnReturnToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void Fire()
    {
        bulletPool.Get();
    }
}
```

### 2. Setup Bullet script

```csharp
public class Bullet : MonoBehaviour
{
    private IObjectPool<Bullet> pool;

    public void SetPool(IObjectPool<Bullet> pool) => this.pool = pool;

    private void OnDisable()
    {
        // Khi disable (ẩn đi), trả về pool
        // Cần check để tránh lỗi trả về khi đang quit game
        pool?.Release(this); 
    }
    
    // Tự deactivate sau 2s (sẽ kích hoạt OnDisable -> Release)
    private void Start() => Invoke(nameof(Deactivate), 2f);
    private void Deactivate() => gameObject.SetActive(false);
}
```

---

## Phần 5: Advanced — "Dirty State" Problem ⚠️

Vấn đề lớn nhất của Object Pool: **Object cũ mang theo dữ liệu cũ.**

Ví dụ:
1. Bullet 1 bay nhanh (`speed = 100`).
2. Bullet 1 trúng địch, bị trả về pool.
3. Bullet 1 được lấy ra lại, nhưng code quên reset speed.
4. Player bắn ra Bullet 1, nhưng nó bay với tốc độ cũ (có thể đã bị slow effect giảm còn 10).

### Giải pháp: `IPoolable` Interface

Dùng interface để force reset data.

```csharp
public interface IPoolable
{
    void OnSpawn();
    void OnDespawn();
}

public class Bullet : MonoBehaviour, IPoolable
{
    public void OnSpawn()
    {
        currentSpeed = maxSpeed; // Reset speed!
        damage = 10;             // Reset damage!
        trailRenderer.Clear();   // Xóa vệt đạn cũ!
    }

    public void OnDespawn()
    {
        // Cleanup physics, stop coroutines
        rb.velocity = Vector3.zero;
    }
}
```
*Lưu ý: Gọi `OnSpawn` trong function `actionOnGet` của Pool.*

---

---

## Phần 6: Ưu & Nhược điểm (Góc nhìn thực tế)

| Ưu điểm (Pros) | Nhược điểm (Cons) |
|----------------|-------------------|
| **Performance**: Tránh GC spikes, game mượt mà. | **Complexity**: Code phức tạp hơn Instantiate/Destroy thuần. |
| **Control**: Kiểm soát được số lượng object tối đa (tránh crash). | **Memory**: Chiếm RAM ngay từ đầu (preload/pre-warm). |
| **Fragment**: Giảm phân mảnh bộ nhớ. | **Reset State**: PHẢI reset trạng thái object hoàn toàn khi lấy ra (dễ quên reset HP, speed...). |

---

## Phần 7: Khi nào DÙNG? (Khi nào KHÔNG?)

### ✅ Khi nào DÙNG:
- Object được **spawn và despawn liên tục** với tần suất cao (Bullets, VFX, Enemies).
- Object tốn nhiều thời gian để khởi tạo (`new`, `Instantiate`).
- Game bị giật (stutter) do Garbage Collector chạy.

### ❌ Khi nào KHÔNG dùng:
- Object **hiếm khi tạo/hủy** (Level Boss, UI Panel chính).
- Object dùng bộ nhớ quá lớn, giữ trong pool lãng phí RAM.
- Project nhỏ, chưa cần optimize (Premature Optimization là nguồn gốc của mọi tội lỗi).

---

## Phần 8: Thực hành

### Bước 1: Setup Gun
Viết script `Gun` sử dụng `UnityEngine.Pool.ObjectPool`.

### Bước 2: Setup Bullet
Viết script `Bullet` tự deactivate sau 2 giây.

### Bước 3: Test Editor
Bắn liên tục và quan sát Hierarchy.
Bạn sẽ thấy số lượng Bullet trong Hierarchy KHÔNG tăng mãi, mà chỉ bật/tắt các object cũ.

### Bước 4: Dirty State
Thử giảm tốc độ đạn khi bắn trúng enemy (nhưng không reset).
Bắn object đó lần tiếp theo -> Nó vẫn chậm! -> Fix bằng `ActionOnGet`.

---

## Kiểm tra

- ✅ Dùng `UnityEngine.Pool` chuẩn của Unity.
- ✅ KHÔNG có `Destroy()` hay `Instantiate()` trong Update loop.
- ✅ Hiểu vấn đề "Dirty State" và biết cách reset data (`ActionOnGet`).
- ✅ Biết dùng `CollectionCheck` để debug lỗi return đúp.

---

## Kiến thức rút ra

| Khái niệm | Áp dụng |
|-----------|---------|
| **Object Pool** | Reuse instead of create/destroy |
| **Pre-warming** | Create objects upfront |
| **Memory Management** | Avoid GC spikes |
| **Performance** | Consistent frame rate |

---

## Commit

```
feat(patterns): implement object pool for bullets
```

Tiếp theo: [Pattern 4: Factory](./Pattern4_Factory.md)
