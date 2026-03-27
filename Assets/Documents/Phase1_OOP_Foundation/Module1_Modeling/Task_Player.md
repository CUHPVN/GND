# Task: Player

Tạo Player điều khiển Survivor đã tạo ở task trước.

---

## Mục tiêu

Hiểu **Separation of Concerns** — mỗi class có một trách nhiệm riêng.

> [!NOTE]
> **Game Context**: Player là **người điều khiển survivor** — xử lý input di chuyển (AD - trái/phải *HOẶC* Swipe nếu các bạn làm Mobile). Trong *Vampire Survivors* hay *Last War*, Player chỉ cần lo di chuyển để né đòn, Weapon sẽ tự động bắn!

---

## Yêu cầu

Tạo script `Player.cs` với:

### State
- Tên người chơi
- Reference đến Survivor đang điều khiển

### Behavior
- Nhận input từ bàn phím
- Điều khiển Survivor di chuyển (không phải tấn công — weapon tự động!)

---

## Hướng dẫn

### Bước 1: Setup scene

1. Tạo GameObject "Player"
2. Attach script `Player.cs`
3. Assign Survivor vào Player qua Inspector

### Bước 2: Implement input

```csharp
public class Player : MonoBehaviour
{
    [SerializeField] private string playerName;
    [SerializeField] private Survivor survivor;
    
    private void Update()
    {
        // Đọc input WASD
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        
        // Gọi method Move của Survivor
        survivor.Move(direction);
    }
}
```

### Bước 3: Suy nghĩ — Separation

- Player có nên biết Survivor lưu trữ health như thế nào không?
- Player **chỉ cần biết những gì** về Survivor?

---

## Separation of Concerns

| Class | Trách nhiệm | KHÔNG lo |
|-------|-------------|----------|
| Player | Đọc input, ra lệnh di chuyển | Cách Survivor implement movement |
| Survivor | Quản lý health, speed, position | Input đến từ đâu |

> *"Each class should have only one reason to change."*

Nếu bạn thay đổi cách đọc input → chỉ sửa Player  
Nếu bạn thay đổi tốc độ di chuyển → chỉ sửa Survivor

---

## Kiểm tra

Code của bạn nên:

- ✅ Player chỉ gọi `public` methods của Survivor
- ✅ Player không truy cập trực tiếp vào fields của Survivor
- ✅ WASD điều khiển được survivor

---

## HAS-A vs IS-A

Player **có** (HAS-A) một Survivor, không phải Player **là** (IS-A) một Survivor.
Thậm chí theo Design Game sau này, 1 Player có thể có nhiều Survivor.

```csharp
// ❌ IS-A (Player kế thừa Survivor??)
public class Player : Survivor { }

// ✅ HAS-A (Player có reference đến Survivor)
public class Player : MonoBehaviour
{
    private Survivor survivor;
}
```

> [!TIP]
> *"HAS-A can be better than IS-A"* — Head First Design Patterns
> 
> Đây là preview cho nguyên tắc **"Favor Composition over Inheritance"**!

---

## Kiến thức rút ra

| Khái niệm | Áp dụng |
|-----------|---------|
| Encapsulation | Player không biết internal của Survivor |
| Public interface | Player chỉ dùng public methods |
| Separation | Player = input, Survivor = logic nhân vật |
| **HAS-A** | Player "có" Survivor, không "là" Survivor |

---

## Commit

```
feat(oop): implement player controlling survivor
```

Tiếp theo: [Task: Weapon](./Task_Weapon.md)
