# Task: Zombie

Tạo kẻ địch đuổi theo Survivor.

---

## Mục tiêu

- Tạo Zombie với AI đơn giản
- **Nhận ra vấn đề coupling** (sẽ fix ở Module 3)

> [!NOTE]
> **Game Context**: Zombie là **kẻ địch chính** trong game Survival Shooter. Chúng liên tục xuất hiện và đuổi theo Survivor — giống như trong *Vampire Survivors*, *Last War*, hay *Zombies.io*!

---

## Yêu cầu

Tạo script `Zombie.cs` với:

### State
- Health
- Tốc độ di chuyển
- Damage khi chạm
- Target (Survivor)

### Behavior
- Di chuyển về phía target
- Nhận damage
- Chết khi health <= 0 (drop XP/gold)

---

## Hướng dẫn

### Bước 1: Tạo Zombie

```csharp
public class Zombie : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int contactDamage = 10;
    
    private int currentHealth;
    private Transform target;
    
    // TODO: Implement movement và damage
}
```

### Bước 2: Tìm target

```csharp
private void Start()
{
    // Tìm Survivor trong scene
    // Gợi ý: FindObjectOfType<Survivor>()
}
```

### Bước 3: Di chuyển

```csharp
private void Update()
{
    // Di chuyển về phía target
    // Gợi ý: Vector3.MoveTowards hoặc transform.position +=
}
```

### Bước 4: Nhận damage

Thêm method `TakeDamage(int amount)` để Weapon có thể gây damage.

---

## Kiểm tra

Code của bạn nên:

- ✅ Zombie tự di chuyển về phía Survivor
- ✅ Zombie nhận damage từ Weapon/Bullet
- ✅ Zombie bị destroy khi health <= 0

---

## ⚠️ Coupling Alert — Dừng lại và suy nghĩ!

Hiện tại code dùng `FindObjectOfType<Survivor>()`:

```csharp
private void Start()
{
    target = FindObjectOfType<Survivor>().transform;
}
```

### Vấn đề

| Tình huống | Kết quả |
|------------|---------|
| Có nhiều Survivors? | Lấy survivor nào? |
| Survivor chưa spawn? | **NullReferenceException!** |
| Muốn test Zombie riêng? | Phải có Survivor trong scene |
| Đổi tên class Survivor? | Phải sửa tất cả Zombie |

### Đây là Tight Coupling!

Zombie **phụ thuộc trực tiếp** vào Survivor class:
- Zombie *biết quá nhiều* về Survivor
- Zombie *tự đi tìm* Survivor

> *"Strive for loosely coupled designs between objects that interact."*  
> — Head First Design Patterns

---

## Ghi nhớ câu hỏi này

> **Làm sao để Zombie không cần biết về Survivor class cụ thể?**

Đây là vấn đề bạn sẽ giải quyết ở [Module 3: Dependency](../Module3_Dependency/README.md):
- **Dependency Injection (đơn giản)**: Ai đó "đưa" target vào, không tự tìm
- **Interface Abstraction**: Zombie chỉ cần `ITarget`, không cần biết là Survivor

---

## Kiến thức rút ra

| Khái niệm | Áp dụng |
|-----------|---------|
| Object interaction | Zombie ↔ Survivor ↔ Weapon |
| State management | Health, damage logic |
| **Problem awareness** | `FindObjectOfType` = code smell |
| **Coupling** | Zombie phụ thuộc trực tiếp vào Survivor |

---

## Commit

```
feat(oop): implement zombie chasing survivor
```

---

## Hoàn thành Module 1

Bạn đã hoàn thành Module 1: Modeling.

### Tổng kết những gì đã học:

| Task | Concept |
|------|---------|
| Survivor | Encapsulation, "What varies?" |
| Player | Separation of Concerns, Input handling |
| Weapon | Composition, HAS-A relationship |
| Zombie | Object interaction, **Coupling problem** |

Commit milestone:
```
feat(oop): complete basic modeling
```

Tiếp theo: [Module 2: Variation](../Module2_Variation/README.md)
