# Task: Survivor

Tạo nhân vật survivor với state và behavior.

---

## Mục tiêu

Hiểu **Encapsulation** — nguyên tắc cốt lõi đầu tiên của OOP.

> *"Encapsulate what varies"* — Head First Design Patterns

> [!NOTE]
> **Game Context**: Đây là **nhân vật survivor** trong game Survival Shooter — kiểu *Vampire Survivors* hoặc *Last War*. Survivor di chuyển để né đòn và có weapon tự động bắn!

---

## Yêu cầu

Tạo script `Survivor.cs` với:

### State (properties)
- Health hiện tại
- Health tối đa
- Tốc độ di chuyển
- Fire rate (tốc độ bắn)

### Behavior (methods)
- Di chuyển
- Nhận damage
- Hồi máu
- Chết khi health <= 0

---

## Hướng dẫn

### Bước 1: Tạo script

```csharp
public class Survivor : MonoBehaviour
{
    // TODO: Khai báo state ở đây
    
    // TODO: Implement các methods
}
```

### Bước 2: Suy nghĩ — "What varies?"

Trả lời các câu hỏi:
- State nào **hay thay đổi**? → Nên `private`
- State nào cần cho bên ngoài **đọc**? → Property với getter
- Method nào bên ngoài cần **gọi**? → `public`

> [!TIP]
> Câu hỏi *"What varies?"* là nền tảng của nguyên tắc *"Encapsulate what varies"*!

### Bước 3: Implement

Viết code theo suy nghĩa của bạn.

---

## Kiểm tra

Sau khi hoàn thành, code của bạn nên:

- ✅ Không cho phép truy cập trực tiếp vào `currentHealth`
- ✅ Có property để đọc health từ bên ngoài
- ✅ Không cho heal vượt quá maxHealth
- ✅ Fire event hoặc destroy khi health <= 0

---

## Code Smells — Cảnh báo!

❌ **Đừng làm thế này:**

```csharp
public class Survivor : MonoBehaviour
{
    public float currentHealth;  // ❌ Public field!
    public float moveSpeed;      // ❌ Ai cũng sửa được!
}
```

**Vấn đề:**
- Bất kỳ script nào cũng có thể `survivor.currentHealth = 9999;`
- Không có validation
- Bug rất khó track

✅ **Làm thế này:**

```csharp
public class Survivor : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth = 100f;
    
    public float CurrentHealth => currentHealth; // Chỉ đọc
    public float HealthPercent => currentHealth / maxHealth;
    
    public void TakeDamage(float amount) 
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
}
```

---

## Kiến thức rút ra

| Khái niệm | Áp dụng |
|-----------|---------|
| Class | `Survivor` là model của nhân vật |
| Encapsulation | `private` fields + `public` methods |
| Validation | Kiểm tra logic trong methods |
| **"What varies?"** | `currentHealth`, `moveSpeed` hay thay đổi → private |

---

## Kết nối với Design Principles

> [!NOTE]
> Ở Phase 2, bạn sẽ học nguyên tắc đầy đủ: **"Encapsulate What Varies"**
> 
> Bây giờ, chỉ cần nhớ: **những thứ hay thay đổi nên được bảo vệ**.

---

## Commit

```
feat(oop): implement survivor with encapsulation
```

Tiếp theo: [Task: Player](./Task_Player.md)
