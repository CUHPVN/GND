# Module 3: Reality Check

> *"The simplest explanation is, games are unique because they are simulations. You often don't have any choice but to accept a certain amount of complexity."*  
> — Game Programming Patterns

Game production code ≠ Textbook architecture.

---

## Mục đích

Module này giúp bạn:
- Không bị sốc khi đọc code production
- Hiểu khi nào cần strict architecture
- Biết trade-offs thực tế

---

## Điểm khác biệt chính

### 1. Game Loop vs Request-Response

**Web app (MVC truyền thống):**
```
User Request → Controller → Model → View → Response
```

**Game:**
```
60 FPS game loop
├── Input polling (liên tục)
├── Physics update
├── Game logic update
├── Animation update
└── Render
```

Game không "chờ" user input như web.

> *"The game loop is the heartbeat of a game."*  
> — Game Programming Patterns

---

### 2. Component-based vs Layer-based

**Textbook MVC:**
```
Controllers/
Models/
Views/
```

**Unity thực tế:**
```
Player/
├── PlayerController.cs   (input + logic)
├── PlayerView.cs         (visuals)
└── PlayerData.cs         (model)

Enemy/
├── EnemyAI.cs
├── EnemyView.cs
└── EnemyStats.cs
```

Unity tổ chức theo **feature**, không phải layer — đây là **Composition Over Inheritance** từ Phase 2!

---

### 3. MonoBehaviour là gì?

MonoBehaviour là hybrid:
- Có lifecycle (Update, Start)
- Có serialization
- Được attach vào GameObject

Nó **không map 1:1** với MVC component nào.

**Giải pháp thực tế:**
- MonoBehaviour làm View + Controller "light"
- Pure C# class làm Model
- Presenter/Manager làm orchestration

---

### 4. Strict Architecture ≠ Better Game

| Game type | Architecture cần thiết |
|-----------|------------------------|
| Game jam (48h) | Không cần, ship là chính |
| Prototype | Minimal, dễ iterate |
| Mid-size indie | Some structure |
| AAA / Live service | Strict, scalable |

> [!TIP]
> Architecture phục vụ game, không phải ngược lại.

---

## Khi nào KHÔNG cần strict MVP

### Rapid Prototyping

```csharp
// Đừng làm thế này trong production
// Nhưng OK cho prototype
public class QuickPrototype : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private int score;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            score++;
            scoreText.text = $"Score: {score}";
        }
    }
}
```

### Game Jam

> "Make it work, make it right, make it fast."
> Game jam = "Make it work"

### Tiny Features

Nếu feature chỉ có 1 script 50 dòng → không cần 4 classes.

---

## Khi nào CẦN strict architecture

### 1. Team > 2 người

Cần contracts rõ ràng (interfaces) — **Program to Abstraction** từ Phase 2!

### 2. Live service game

Code sẽ sống nhiều năm → maintainability quan trọng.

### 3. Complex features

Nhiều systems tương tác → cần separation.

### 4. Test-driven

Cần mock/stub → cần interfaces.

---

## Pattern thực tế trong Unity games

### 1. Service Locator

```csharp
public class ServiceLocator
{
    private static Dictionary<Type, object> services = new Dictionary<Type, object>();
    
    public static void Register<T>(T service)
    {
        services[typeof(T)] = service;
    }
    
    public static T Get<T>()
    {
        return (T)services[typeof(T)];
    }
}

// Usage — Factory pattern từ Phase 3!
ServiceLocator.Register<IAudioService>(new AudioService());
var audio = ServiceLocator.Get<IAudioService>();
```

### 2. ScriptableObject as Data

```csharp
[CreateAssetMenu]
public class PlayerConfig : ScriptableObject
{
    public int maxHealth = 100;
    public float moveSpeed = 5f;
}
```

### 3. Event-driven (không strict MVP)

```csharp
// Không cần Presenter, dùng events trực tiếp
// Observer pattern từ Phase 3!
public class Player : MonoBehaviour
{
    public static event Action<int> OnScoreChanged;
    
    private void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
    }
}

public class ScoreUI : MonoBehaviour
{
    private void OnEnable() => Player.OnScoreChanged += UpdateUI;
    private void OnDisable() => Player.OnScoreChanged -= UpdateUI;
}
```

---

## Tóm tắt

| Textbook | Production |
|----------|------------|
| Pure MVC layers | Feature-based folders |
| Strict separation | Pragmatic separation |
| Every feature = full pattern | Small = simple, Large = pattern |
| Learn all before use | Learn → Use → Refactor |

---

## Connections to Previous Phases

| Phase | Concept | Reality Check Application |
|-------|---------|---------------------------|
| Phase 2 | Composition Over Inheritance | Feature-based folders |
| Phase 2 | Program to Abstraction | Interfaces for team work |
| Phase 3 | Observer | Event-driven communication |
| Phase 3 | Factory | Service Locator pattern |

---

## Kiến thức rút ra

- Architecture là tool, không phải religion
- Context quyết định mức độ complexity
- Premature optimization = premature architecture
- "Perfect" architecture cho game không ship = thất bại

---

## Commit

```
feat(architecture): understand reality check
```

Tiếp theo: [Module 4: Mini Project](./Module4_MiniProject.md)
