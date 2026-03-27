# Pattern 4: Factory

> *"Define an interface for creating an object, but let subclasses decide which class to instantiate. Factory Method lets a class defer instantiation to subclasses."*  
> — Head First Design Patterns

Tạo objects mà không cần biết class cụ thể.

---

## Recall Phase 2 🔙

Bạn đã thấy Factory ở **Principle 3: Program to Abstraction**:
- `ISaveService` interface
- Nhiều implementations: `PlayerPrefsSaveService`, `JsonFileSaveService`
- Factory có thể tạo đúng service dựa trên config

Factory Pattern là **cách tạo objects** theo abstraction!

---

## Feature: Enemy Spawning

Game có nhiều loại enemy:
- Zombie
- Skeleton
- Vampire
- Boss

Spawner cần tạo enemy theo type mà không hardcode.

---

## Phần 1: Cách sai — Switch/Case

```csharp
public class EnemySpawner : MonoBehaviour
{
    public void SpawnEnemy(string type)
    {
        GameObject enemy = null;
        
        switch (type)
        {
            case "zombie":
                enemy = Instantiate(zombiePrefab);
                break;
            case "skeleton":
                enemy = Instantiate(skeletonPrefab);
                break;
            case "vampire":
                enemy = Instantiate(vampirePrefab);
                break;
            case "boss":
                enemy = Instantiate(bossPrefab);
                break;
        }
    }
}
```

### Vấn đề

| Issue | Violates |
|-------|----------|
| Thêm enemy type → sửa Spawner | **Open/Closed** (Phase 2) |
| Spawner biết quá nhiều về enemy types | Single Responsibility |
| Duplicate setup logic | DRY |
| Khó test | Testability |

---

## Phần 2: Factory Pattern

### Có 2 biến thể chính:

| Factory Method | Simple Factory |
|----------------|----------------|
| Interface + multiple implementations | Single class with create logic |
| Most flexible | Most common in games |

Chúng ta focus **Simple Factory** trước — phổ biến trong game.

---

## Phần 3: Simple Factory Implementation

### Interface

```csharp
public interface IEnemy
{
    void Initialize();
    void TakeDamage(int amount);
}
```

### Simple Factory

```csharp
public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private GameObject skeletonPrefab;
    [SerializeField] private GameObject vampirePrefab;
    
    public IEnemy Create(EnemyType type)
    {
        GameObject prefab = type switch
        {
            EnemyType.Zombie => zombiePrefab,
            EnemyType.Skeleton => skeletonPrefab,
            EnemyType.Vampire => vampirePrefab,
            _ => throw new ArgumentException($"Unknown enemy type: {type}")
        };
        
        var obj = Instantiate(prefab);
        var enemy = obj.GetComponent<IEnemy>();
        enemy.Initialize();
        return enemy;
    }
}
```

### Spawner sử dụng Factory

```csharp
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory enemyFactory;
    
    public void SpawnWave()
    {
        enemyFactory.Create(EnemyType.Zombie);
        enemyFactory.Create(EnemyType.Skeleton);
        enemyFactory.Create(EnemyType.Zombie);
    }
}
```

---

## Phần 4: Factory Method Pattern

Flexible hơn — mỗi factory là một class:

### Interface

```csharp
public interface IEnemyFactory
{
    IEnemy CreateEnemy();
}
```

### Concrete Factories

```csharp
public class ZombieFactory : IEnemyFactory
{
    private GameObject prefab;
    
    public ZombieFactory(GameObject prefab)
    {
        this.prefab = prefab;
    }
    
    public IEnemy CreateEnemy()
    {
        var obj = Object.Instantiate(prefab);
        var zombie = obj.GetComponent<Zombie>();
        zombie.Initialize();
        return zombie;
    }
}
```

### Spawner với Factory Method

```csharp
public class EnemySpawner : MonoBehaviour
{
    private Dictionary<string, IEnemyFactory> factories = new Dictionary<string, IEnemyFactory>();
    
    public void RegisterFactory(string type, IEnemyFactory factory)
    {
        factories[type] = factory;
    }
    
    public IEnemy SpawnEnemy(string type)
    {
        if (factories.TryGetValue(type, out var factory))
        {
            return factory.CreateEnemy();
        }
        
        Debug.LogError($"No factory registered for type: {type}");
        return null;
    }
}
```

---

## Phần 5: Factory với ScriptableObject

Unity-friendly approach:

```csharp
[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public GameObject prefab;
    public int health;
    public float speed;
}

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private EnemyData[] enemyDataList;
    
    private Dictionary<string, EnemyData> dataDict;
    
    private void Awake()
    {
        dataDict = enemyDataList.ToDictionary(d => d.enemyName, d => d);
    }
    
    public IEnemy Create(string enemyName)
    {
        if (!dataDict.TryGetValue(enemyName, out var data))
        {
            return null;
        }
        
        var obj = Instantiate(data.prefab);
        var enemy = obj.GetComponent<EnemyBase>();
        enemy.Initialize(data.health, data.speed);
        return enemy;
    }
}
```

---

## Phần 6: Ưu & Nhược điểm

| Ưu điểm (Pros) | Nhược điểm (Cons) |
|----------------|-------------------|
| **Decoupling**: Client không cần biết class cụ thể nào đang được tạo. | **Complexity**: Cần tạo nhiều class interface/factory. |
| **SRP**: Factory chịu trách nhiệm tạo object, Client chịu trách nhiệm sử dụng. | **Debug**: Khó theo dõi luồng tạo object nếu factory quá phức tạp. |
| **Mocking**: Dễ dàng inject MockFactory để test. | |

---

## Phần 7: Khi nào dùng? (Khi nào KHÔNG?)

### ✅ Khi nào DÙNG:
- Khi class không biết trước nó cần tạo object của class nào (Enemy Spawner).
- Khi logic tạo object phức tạp (cần tìm prefab, load data, config stats).
- Khi muốn hệ thống dễ mở rộng (thêm Enemy mới chỉ cần thêm Data/Factory mới).

### ❌ Khi nào KHÔNG dùng:
- Khi chỉ tạo 1-2 object đơn giản (`new List<string>()` là đủ).
- Khi không có nhu cầu mở rộng hay thay thế implementation.

---

## Phần 8: Thực hành

### Bước 1: Tạo `IEnemy` interface

### Bước 2: Tạo `EnemyFactory`
- Simple factory với ScriptableObject config

### Bước 3: Refactor Spawner
- Không có switch/case về enemy types

### Bước 4: Thêm enemy mới
- Chỉ thêm data, không sửa code

---

## Kiểm tra

- ✅ Spawner không biết concrete enemy classes
- ✅ Thêm enemy type = thêm prefab + data
- ✅ Factory centralize creation logic
- ✅ Dễ test với mock factory

---

## Kiến thức rút ra

| Khái niệm | Áp dụng |
|-----------|---------|
| **Factory Pattern** | Centralize object creation |
| **Simple Factory** | Single create method |
| **Factory Method** | Interface + multiple factories |
| **ScriptableObject** | Data-driven factories |

---

## Commit

```
feat(patterns): implement factory pattern for enemies
```

Tiếp theo: [Pattern 5: State](./Pattern5_State.md)
