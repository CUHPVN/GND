# Principle 4: Low Coupling / High Cohesion

> *"Strive for loosely coupled designs between objects that interact."*  
> — Head First Design Patterns

Giảm phụ thuộc giữa các modules, tăng tính tập trung trong mỗi module.

---

## Recall Phase 1 🔙

Bạn đã thấy principle này:
- **Task_Coupling**: FindObjectOfType = tight coupling
- **Task_Events**: Events giúp loose coupling
- **Observer foundation** đã được giới thiệu!

---

## Định nghĩa

| Khái niệm | Ý nghĩa | Mục tiêu |
|-----------|---------|----------|
| **Coupling** | Mức độ phụ thuộc giữa các modules | **LOW** |
| **Cohesion** | Mức độ tập trung của một module vào một nhiệm vụ | **HIGH** |

---

## Single Responsibility Principle (SRP)

> *"A class should have only one reason to change."*

High Cohesion = SRP Applied!

| Class | Có bao nhiêu "reasons to change"? |
|-------|-----------------------------------|
| ❌ Player (làm mọi thứ) | Nhiều: input, health, abilities, UI... |
| ✅ Player (focused) | Một: player behavior |

---

## Feature: Achievement System

Khi player đạt thành tích:
- Unlock achievement
- Show notification
- Play sound
- Update UI
- Save progress

---

## Phần 1: Cách sai — High Coupling

```csharp
public class Player : MonoBehaviour
{
    [SerializeField] private AchievementManager achievementManager;
    [SerializeField] private NotificationUI notification;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AchievementUI achievementUI;
    [SerializeField] private SaveManager saveManager;
    
    private int enemiesKilled;
    
    public void OnEnemyKilled()
    {
        enemiesKilled++;
        
        if (enemiesKilled == 10)
        {
            achievementManager.Unlock("first_blood");
            notification.Show("Achievement Unlocked!");
            audioManager.Play("achievement_sound");
            achievementUI.Refresh();
            saveManager.Save();
        }
    }
}
```

### Vấn đề

| Issue | Problem |
|-------|---------|
| Player biết quá nhiều thứ | 5 dependencies! |
| Thêm feature → sửa Player | Low cohesion |
| Không thể test Player riêng | Tight coupling |
| Player có Low Cohesion | Làm quá nhiều việc |

---

## Phần 2: Giải pháp — Event System

### Event definition

```csharp
public class GameEvents
{
    // Singleton for simplicity (có cách tốt hơn ở Phase 3)
    public static GameEvents Instance { get; } = new GameEvents();
    
    public event Action<string> OnAchievementUnlocked;
    public event Action<int> OnEnemyKilled;
    
    public void TriggerAchievementUnlocked(string achievementId)
    {
        OnAchievementUnlocked?.Invoke(achievementId);
    }
    
    public void TriggerEnemyKilled(int totalKills)
    {
        OnEnemyKilled?.Invoke(totalKills);
    }
}
```

### Player (Focused, High Cohesion)

```csharp
public class Player : MonoBehaviour
{
    private int enemiesKilled;
    
    public void OnEnemyKilled()
    {
        enemiesKilled++;
        GameEvents.Instance.TriggerEnemyKilled(enemiesKilled);
    }
}
```

**Player chỉ làm 1 việc:** track player state và fire events!

### Achievement System (Focused)

```csharp
public class AchievementSystem : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.Instance.OnEnemyKilled += CheckKillAchievements;
    }
    
    private void OnDisable()
    {
        GameEvents.Instance.OnEnemyKilled -= CheckKillAchievements;
    }
    
    private void CheckKillAchievements(int totalKills)
    {
        if (totalKills == 10)
        {
            UnlockAchievement("first_blood");
        }
    }
    
    private void UnlockAchievement(string id)
    {
        // Unlock logic
        GameEvents.Instance.TriggerAchievementUnlocked(id);
    }
}
```

### Các modules khác (Subscribe)

```csharp
// NotificationSystem
public class NotificationSystem : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.Instance.OnAchievementUnlocked += ShowNotification;
    }
    
    private void ShowNotification(string achievementId)
    {
        // Show notification
    }
}

// AudioSystem
public class AudioSystem : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.Instance.OnAchievementUnlocked += PlayAchievementSound;
    }
    
    private void PlayAchievementSound(string achievementId)
    {
        // Play sound
    }
}
```

---

## Phần 3: Kết quả

### Trước (High Coupling)

```
Player → AchievementManager
Player → NotificationUI
Player → AudioManager
Player → AchievementUI
Player → SaveManager
(5 dependencies!)
```

### Sau (Low Coupling)

```
Player → GameEvents (chỉ 1 dependency)
     ↓ (event)
AchievementSystem
NotificationSystem
AudioSystem
SaveSystem
(tất cả độc lập)
```

---

## 🎉 Observer Pattern Preview

> *"Define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified."*

| Observer Pattern | Event Example |
|-----------------|---------------|
| Subject | GameEvents |
| Observers | AchievementSystem, AudioSystem, ... |
| Notify | `OnAchievementUnlocked?.Invoke()` |

> [!TIP]
> Ở Phase 3, bạn sẽ học **Observer Pattern** chính thức!

---

## Phần 4: High Cohesion

Mỗi class chỉ làm 1 việc (**SRP**):

| Class | Single Responsibility |
|-------|----------------------|
| Player | Player behavior |
| AchievementSystem | Track & unlock achievements |
| NotificationSystem | Show notifications |
| AudioSystem | Play sounds |
| SaveSystem | Save data |

---

## Phần 5: Thực hành

### Bước 1: Tạo `GameEvents` class

### Bước 2: Refactor Player
- Chỉ trigger events, không gọi trực tiếp

### Bước 3: Tạo các systems
- Mỗi system subscribe events cần thiết

### Bước 4: Test
- Delete một system → game vẫn chạy (graceful)

---

## Kiểm tra

- ✅ Player không reference trực tiếp các managers
- ✅ Các systems không biết nhau
- ✅ Thêm feature mới không sửa existing code
- ✅ Có thể test từng system riêng

---

## Kiến thức rút ra

| Khái niệm | Áp dụng |
|-----------|---------|
| **Low Coupling** | Events thay direct references |
| **High Cohesion** | Mỗi class 1 responsibility |
| **SRP** | Single Responsibility Principle |
| **Observer preview** | Event-based communication |
| **Maintainability** | Dễ thêm/bớt features |

---

## Metrics

| Metric | ❌ Bad | ✅ Good |
|--------|--------|---------|
| Dependencies per class | 5+ | 1-2 |
| Reasons to change | Nhiều | 1 |
| Lines per class | 500+ | <200 |

---

## Commit

```
feat(principles): implement low coupling high cohesion
```

---

## Hoàn thành Phase 2! 🎉

Bạn đã hoàn thành **Phase 2: Design Principles**.

### Tổng kết:

| Principle | SOLID | Pattern Preview |
|-----------|-------|-----------------|
| Encapsulate What Changes | O (OCP) | Strategy |
| Composition Over Inheritance | L, I (LSP, ISP) | Decorator, Component |
| Program to Abstraction | D (DIP) | Factory |
| Low Coupling / High Cohesion | S (SRP) | Observer |

### Commit milestone:
```
feat(principles): complete phase 2 design principles
```

---

## Tiếp theo

[Phase 3: Design Patterns](../Phase3_Design_Patterns/README.md)

Ở Phase 3, bạn sẽ học các **patterns** — là cách áp dụng principles này vào các vấn đề cụ thể!
