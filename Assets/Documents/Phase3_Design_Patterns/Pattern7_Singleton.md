# Pattern 7: Singleton

> *"Ensure a class has one instance, and provide a global point of access to it."*  
> — GoF Design Patterns

> ⚠️ *"Like much of the game development community, I think the Singleton pattern is more trouble than it's worth."*  
> — Game Programming Patterns

Pattern phổ biến nhất... và cũng gây tranh cãi nhất trong game development.

---

## ⚠️ Cảnh báo trước!

**Singleton là pattern bạn CẦN BIẾT, nhưng NÊN HẠN CHẾ dùng.**

Tại sao vẫn dạy?
- Rất phổ biến trong Unity tutorials
- Cần hiểu để biết khi nào KHÔNG dùng
- Có use cases hợp lý

---

## Feature: Global Managers

Game cần các managers:
- AudioManager — chơi nhạc/SFX
- GameManager — game state, score
- InputManager — xử lý input

Làm sao access từ mọi nơi mà không cần reference?

---

## Phần 1: Basic Singleton

```csharp
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void PlaySound(string soundName)
    {
        // Play sound logic
    }
}

// Usage từ bất kỳ đâu
AudioManager.Instance.PlaySound("explosion");
```

---

## Phần 2: Generic Singleton

```csharp
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static readonly object lockObj = new object();
    
    public static T Instance
    {
        get
        {
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    
                    if (instance == null)
                    {
                        var singletonObject = new GameObject(typeof(T).Name);
                        instance = singletonObject.AddComponent<T>();
                    }
                }
                return instance;
            }
        }
    }
    
    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this as T;
        DontDestroyOnLoad(gameObject);
    }
}

// Usage
public class GameManager : Singleton<GameManager>
{
    public int Score { get; set; }
}
```

---

## Phần 3: Vấn đề của Singleton ⚠️

### 1. Hidden Dependencies

```csharp
public class Player : MonoBehaviour
{
    public void TakeDamage(int amount)
    {
        health -= amount;
        AudioManager.Instance.PlaySound("hit");  // Hidden dependency!
        UIManager.Instance.UpdateHealth(health); // Hidden dependency!
        GameManager.Instance.AddScore(-10);      // Hidden dependency!
    }
}
```

**Vấn đề:** Đọc code Player, không biết nó depend on những gì!

**Vi phạm Phase 2:** Low Coupling bị phá vỡ.

### 2. Khó Test

```csharp
[Test]
public void TestPlayerTakeDamage()
{
    var player = new Player();
    player.TakeDamage(10);
    
    // Làm sao test mà không có AudioManager.Instance?
    // Làm sao mock?
}
```

### 3. Tight Coupling

```csharp
// Mọi nơi đều couple với Singleton
class A { void Do() => GameManager.Instance.X(); }
class B { void Do() => GameManager.Instance.Y(); }
class C { void Do() => GameManager.Instance.Z(); }
```

### 4. Order of Initialization

```csharp
// Bug tiềm ẩn: GameManager access AudioManager trong Awake
// Nhưng AudioManager chưa Awake!
public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        AudioManager.Instance.PlaySound("start"); // Có thể null!
    }
}
```

---

## Phần 4: Khi nào OK dùng Singleton?

| ✅ OK | ❌ Tránh (Avoid) |
|------|---------|
| **Thực sự Global** (Chỉ 1 instance duy nhất) | **Có thể có nhiều instance** (Player, Enemy) |
| **Infrastructure** (Audio, Input, System) | **Game Logic** (Gameplay, Mechanics) |
| **Stateless** hoặc rất ít state | **Quản lý state phức tạp** |
| **Game jam / Prototype** (Làm nhanh) | **Production code** (Dự án đường dài) |

---

## Phần 5: Alternatives — Tốt hơn Singleton!

### 1. Dependency Injection (manual)

```csharp
public class Player : MonoBehaviour
{
    private IAudioService audioService;
    
    public void Initialize(IAudioService audioService)
    {
        this.audioService = audioService;
    }
    
    public void TakeDamage(int amount)
    {
        health -= amount;
        audioService.PlaySound("hit");  // Explicit dependency!
    }
}
```

### 2. Service Locator

```csharp
public class Player : MonoBehaviour
{
    public void TakeDamage(int amount)
    {
        health -= amount;
        ServiceLocator.Get<IAudioService>().PlaySound("hit");
    }
}
```

### 3. ScriptableObject Events (Unity-specific)

```csharp
[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();
    
    public void Raise() => listeners.ForEach(l => l.OnEventRaised());
    public void RegisterListener(GameEventListener listener) => listeners.Add(listener);
}
```

### 4. Zenject / VContainer (DI Frameworks)

Professional Unity projects thường dùng DI frameworks.

> [!TIP]
> Xem video [Reference Objects Flawlessly](../RESOURCES.md#phase-3-design-patterns) để hiểu sâu hơn!

---

## Phần 6: Nếu PHẢI dùng Singleton...

### Checklist an toàn:

- [ ] Chỉ cho infrastructure (Audio, Input, Analytics)
- [ ] KHÔNG có game logic
- [ ] Stateless hoặc minimal state
- [ ] Document rõ ràng là Singleton
- [ ] Consider Service Locator thay thế

### Safer Singleton Pattern

```csharp
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    
    // Không tự động tạo — explicit setup
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("AudioManager not initialized! Add to Bootstrap.");
            }
            return instance;
        }
    }
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple AudioManager instances!");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;  // Cleanup!
        }
    }
}
```

---

## Kiểm tra

- ✅ Hiểu Singleton pattern
- ✅ Biết các pitfalls và trade-offs
- ✅ Biết alternatives (DI, Service Locator)
- ✅ Biết khi nào OK / không OK dùng

---

## Phần 7: Ưu & Nhược điểm (Sự thật mất lòng)

| Ưu điểm (Pros) | Nhược điểm (Cons) |
|----------------|-------------------|
| **Easy Access**: Gọi được từ bất kỳ đâu. | **Hidden Dependencies**: Class phụ thuộc vào Singleton mà không khai báo. |
| **Ensure Single Instance**: Đảm bảo chỉ có 1 AudioManager. | **Tight Coupling**: Khó thay đổi, khó test, khó reuse code. |
| **Quick implement**: Viết 5 dòng code là xong. | **State Global**: Debug ác mộng khi ai đó thay đổi state của Singleton. |

---

## Phần 8: Khi nào dùng? (Khi nào KHÔNG?)

### ✅ Khi nào DÙNG (Cẩn thận):
- Manager quản lý tài nguyên hệ thống (Audio, FileSystem, Network, Logger).
- Objects thực sự Global và Single (GameManager, InputManager).
- **Prototype nhanh** để test idea.

### ❌ Khi nào KHÔNG dùng:
- **Game Logic**: Player, Enemy, Inventory (Không bao giờ dùng Singleton cho những thứ này!).
- Khi có thể dùng **Dependency Injection** hoặc **Reference dragging** trong Inspector.
- Khi muốn viết Unit Test.

---

## Kiến thức rút ra

| Khái niệm | Học được |
|-----------|----------|
| **Singleton** | Global access, single instance |
| **Hidden Dependencies** | Vấn đề chính của Singleton |
| **Alternatives** | DI, Service Locator, SO Events |
| **Pragmatism** | OK cho infrastructure, avoid cho logic |

---

## Tài liệu đọc thêm

- [Game Programming Patterns - Singleton](../RESOURCES.md#phase-3-design-patterns)
- [Reference Objects Flawlessly](../RESOURCES.md#phase-3-design-patterns)

---

## Commit

```
feat(patterns): add singleton pattern with pitfalls warning
```

---

## 🎉 Hoàn thành Phase 3!

Bạn đã hoàn thành **Phase 3: Design Patterns** với 7 patterns!

| Pattern | Use Case | Caution Level |
|---------|----------|---------------|
| Strategy | AI, Movement | ✅ Safe |
| Observer | Events | ✅ Safe (remember unsubscribe) |
| Object Pool | Bullets, VFX | ✅ Safe |
| Factory | Spawning | ✅ Safe |
| State | Player states | ✅ Safe |
| Command | Undo, Replay | ✅ Safe |
| **Singleton** | Global managers | ⚠️ Use with caution! |

Tiếp theo: [Phase 4: Architecture](../Phase4_Architecture/README.md)
