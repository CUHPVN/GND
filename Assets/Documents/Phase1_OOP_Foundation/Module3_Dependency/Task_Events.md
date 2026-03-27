# Task: Events

Học C# Events và UnityEvents — foundation cho Observer Pattern.

---

## Mục tiêu

- Hiểu **Events** là gì và tại sao quan trọng
- Phân biệt **C# Events** vs **UnityEvents**
- **Preview**: Đây là foundation cho **Observer Pattern** (Phase 3)

---

## Tại sao cần Events?

### Vấn đề: Bidirectional Coupling

```csharp
// ❌ Survivor biết về UI
public class Survivor : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ScorePanel scorePanel;
    
    public void TakeDamage(int amount)
    {
        health -= amount;
        healthBar.UpdateHealth(health);  // Survivor biết UI!
    }
    
    public void CollectXP(int amount)
    {
        xp += amount;
        scorePanel.UpdateXP(xp);  // Coupling!
    }
}
```

**Vấn đề:**
- Survivor phụ thuộc vào UI components
- Thêm UI mới → sửa Survivor
- Không thể tái sử dụng Survivor ở project khác

### Giải pháp: Events

```csharp
// ✅ Survivor không biết ai đang listen
public class Survivor : MonoBehaviour
{
    public event Action<int> OnHealthChanged;
    public event Action<int> OnXPChanged;
    
    public void TakeDamage(int amount)
    {
        health -= amount;
        OnHealthChanged?.Invoke(health);  // Broadcast, không biết ai listen
    }
    
    public void CollectXP(int amount)
    {
        xp += amount;
        OnXPChanged?.Invoke(xp);
    }
}
```

---

## Phần 1: C# Events Cơ bản

### Syntax

```csharp
using System;

public class Survivor : MonoBehaviour
{
    // Khai báo event
    public event Action OnDied;                    // Không có parameter
    public event Action<int> OnDamaged;            // 1 parameter
    public event Action<int, int> OnHealthChanged; // 2 parameters (current, max)
    
    private void Die()
    {
        OnDied?.Invoke();  // Fire event (null-safe)
    }
    
    private void TakeDamage(int amount)
    {
        OnDamaged?.Invoke(amount);
    }
}
```

### Subscribe và Unsubscribe

```csharp
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Survivor survivor;
    
    private void OnEnable()
    {
        survivor.OnDamaged += HandleDamaged;  // Subscribe
    }
    
    private void OnDisable()
    {
        survivor.OnDamaged -= HandleDamaged;  // Unsubscribe (QUAN TRỌNG!)
    }
    
    private void HandleDamaged(int amount)
    {
        // Update UI
    }
}
```

> [!WARNING]
> **Luôn unsubscribe** trong `OnDisable()` hoặc `OnDestroy()` để tránh memory leaks!

---

## Phần 2: Thực hành — Survival Shooter Health System

### Bước 1: Survivor fire events

```csharp
public class Survivor : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    
    public event Action<int, int> OnHealthChanged;  // (current, max)
    public event Action OnDied;
    public event Action<int> OnXPGained;
    
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        
        if (currentHealth <= 0)
        {
            OnDied?.Invoke();
        }
    }
    
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}
```

### Bước 2: HealthBar listen

```csharp
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Survivor survivor;
    [SerializeField] private Image fillImage;
    
    private void OnEnable()
    {
        survivor.OnHealthChanged += UpdateBar;
    }
    
    private void OnDisable()
    {
        survivor.OnHealthChanged -= UpdateBar;
    }
    
    private void UpdateBar(int current, int max)
    {
        fillImage.fillAmount = (float)current / max;
    }
}
```

### Bước 3: DeathHandler listen

```csharp
public class DeathHandler : MonoBehaviour
{
    [SerializeField] private Survivor survivor;
    
    private void OnEnable()
    {
        survivor.OnDied += HandleDeath;
    }
    
    private void OnDisable()
    {
        survivor.OnDied -= HandleDeath;
    }
    
    private void HandleDeath()
    {
        // Play death animation, show game over, respawn, etc.
    }
}
```

---

## Phần 3: Zombie Events

Zombie cũng có thể fire events:

```csharp
public class Zombie : MonoBehaviour
{
    public event Action<Zombie> OnDied;  // Pass self for reference
    public event Action<int> OnDamaged;
    
    public void TakeDamage(int amount)
    {
        health -= amount;
        OnDamaged?.Invoke(amount);
        
        if (health <= 0)
        {
            OnDied?.Invoke(this);  // "I died!"
            Destroy(gameObject);
        }
    }
}

// Score system listens to zombie deaths
public class ScoreManager : MonoBehaviour
{
    private int score;
    
    public void RegisterZombie(Zombie zombie)
    {
        zombie.OnDied += HandleZombieDied;
    }
    
    private void HandleZombieDied(Zombie zombie)
    {
        score += zombie.ScoreValue;
        // Update UI...
    }
}
```

---

## Phần 4: UnityEvents (Bonus)

Unity cung cấp `UnityEvent` — có thể configure trong Inspector.

```csharp
using UnityEngine.Events;

public class Survivor : MonoBehaviour
{
    public UnityEvent OnDied;  // Có thể assign trong Inspector!
    
    private void Die()
    {
        OnDied?.Invoke();
    }
}
```

### C# Events vs UnityEvents

| Feature | C# Events | UnityEvents |
|---------|-----------|-------------|
| Performance | Nhanh hơn | Chậm hơn |
| Inspector | Không | Có |
| Code flexibility | Cao | Thấp hơn |
| Scene references | Khó | Dễ |
| Recommended | Logic code | Designer-facing |

---

## Kiểm tra

- ✅ Survivor fire events khi health thay đổi
- ✅ HealthBar update mà không biết về Survivor internal
- ✅ Có thể thêm listeners mới mà không sửa Survivor
- ✅ Đã unsubscribe đúng cách

---

## 🎉 Đây là Observer Pattern Foundation!

> *"Define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified."*  
> — Head First Design Patterns

| Observer Pattern | Events Example |
|-----------------|----------------|
| Subject | Survivor |
| Observers | HealthBar, DeathHandler, ScoreManager |
| Notify | `OnHealthChanged?.Invoke()` |
| Subscribe | `+= HandleEvent` |

> [!TIP]
> Ở Phase 3, bạn sẽ học **Observer Pattern** chi tiết. Bây giờ bạn đã hiểu foundation!

---

## Kiến thức rút ra

| Khái niệm | Áp dụng |
|-----------|---------|
| **Events** | One-to-many communication |
| **Loose coupling** | Subject không biết observers |
| **Unsubscribe** | Tránh memory leaks |
| **Observer foundation** | Events là basis cho pattern |

---

## Commit

```
feat(oop): implement events foundation
```

---

## Hoàn thành Phase 1! 🎉

Bạn đã hoàn thành **Phase 1: OOP Foundation**.

### Tổng kết những gì đã học:

| Module | Core Concepts |
|--------|---------------|
| **1. Modeling** | Encapsulation, Composition, Separation of Concerns |
| **2. Variation** | Inheritance, Interfaces, Strategy Pattern preview |
| **3. Dependency** | DI đơn giản, Interface Abstraction, Events, Observer preview |

### Design Maxims đã trải nghiệm:

1. ✅ **Encapsulate what varies** — private fields, behaviors tách riêng
2. ✅ **Program to interfaces** — IWeapon, IDamageable
3. ✅ **Favor composition over inheritance** — Survivor HAS Weapon
4. ✅ **Strive for loose coupling** — DI, Events

### Commit milestone:
```
feat(oop): complete phase 1 foundation
```

---

## Tiếp theo

[Phase 2: Design Principles](../../Phase2_Design_Principles/README.md)

Ở Phase 2, bạn sẽ **formalize** những gì đã trải nghiệm:
- SOLID Principles (chi tiết)
- "Encapsulate What Varies"
- "Favor Composition Over Inheritance"
