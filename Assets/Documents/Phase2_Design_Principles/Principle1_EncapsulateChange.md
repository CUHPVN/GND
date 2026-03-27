# Principle 1: Encapsulate What Changes

> *"Identify the aspects of your application that vary and separate them from what stays the same."*  
> — Head First Design Patterns

Tách phần dễ thay đổi ra khỏi phần ổn định.

---

## Recall Phase 1 🔙

Bạn đã thấy principle này ở Phase 1:
- **Task_Survivor**: Hỏi "What varies?" → `currentHealth`, `moveSpeed` → private
- **Task_WeaponTypes**: Tách `IWeapon` ra khỏi `Survivor`

Giờ ta **formalize** nó!

---

## Feature: Movement System

Player cần di chuyển với nhiều kiểu:
- Walk
- Run  
- Fly
- Swim

---

## Phần 1: Cách sai (Hãy thử trước)

```csharp
public class Player : MonoBehaviour
{
    public enum MovementType { Walk, Run, Fly, Swim }
    
    [SerializeField] private MovementType movementType;
    
    private void Update()
    {
        switch (movementType)
        {
            case MovementType.Walk:
                WalkMovement();
                break;
            case MovementType.Run:
                RunMovement();
                break;
            case MovementType.Fly:
                FlyMovement();
                break;
            case MovementType.Swim:
                SwimMovement();
                break;
        }
    }
    
    private void WalkMovement() { /* ... */ }
    private void RunMovement() { /* ... */ }
    private void FlyMovement() { /* ... */ }
    private void SwimMovement() { /* ... */ }
}
```

### Vấn đề

| Issue | Violates |
|-------|----------|
| Player class ngày càng phình | Single Responsibility |
| Thêm movement mới → sửa Player | **Open/Closed** |
| Không thể thay đổi movement runtime | Flexibility |
| Khó test từng movement riêng | Testability |

---

## Phần 2: Nhận diện "What Changes"

Hỏi: **Phần nào dễ thay đổi?**

| Ổn định | Dễ thay đổi |
|---------|-------------|
| Player cần di chuyển | Cách di chuyển |
| Player có position | Logic movement |
| Input handling | Kiểu movement |

**Movement logic** là phần thay đổi → **Encapsulate nó!**

---

## Phần 3: Giải pháp — Tách Movement ra

### Interface

```csharp
public interface IMovement
{
    void Move(Transform transform, Vector3 input);
}
```

### Các implementations

```csharp
public class WalkMovement : IMovement
{
    private float speed = 5f;
    
    public void Move(Transform transform, Vector3 input)
    {
        transform.position += input * speed * Time.deltaTime;
    }
}

public class FlyMovement : IMovement
{
    private float speed = 10f;
    private float verticalSpeed = 5f;
    
    public void Move(Transform transform, Vector3 input)
    {
        Vector3 movement = input * speed;
        // Thêm vertical movement
        if (Input.GetKey(KeyCode.Space)) movement.y = verticalSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) movement.y = -verticalSpeed;
        
        transform.position += movement * Time.deltaTime;
    }
}
```

### Player refactored

```csharp
public class Player : MonoBehaviour
{
    private IMovement currentMovement;
    
    public void SetMovement(IMovement movement)
    {
        currentMovement = movement;
    }
    
    private void Update()
    {
        Vector3 input = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );
        
        currentMovement?.Move(transform, input);
    }
}
```

---

## 🎉 Đây là Strategy Pattern!

> *"Define a family of algorithms, encapsulate each one, and make them interchangeable."*

| Strategy Pattern | Movement Example |
|-----------------|------------------|
| Context | Player |
| Strategy Interface | IMovement |
| Concrete Strategies | WalkMovement, FlyMovement |

> [!TIP]
> Ở Phase 3, bạn sẽ học **Strategy Pattern** chính thức. Đây là preview!

---

## Open/Closed Principle (OCP)

> *"Classes should be open for extension, closed for modification."*

| Trước | Sau |
|-------|-----|
| Thêm SwimMovement → sửa Player | Thêm SwimMovement → tạo class mới |
| Player có switch/case | Player không biết có bao nhiêu movements |

**Open**: Có thể thêm movements mới (extension)  
**Closed**: Không cần sửa Player (modification)

---

## Phần 4: Thực hành

### Bước 1: Tạo interface `IMovement`

### Bước 2: Tạo 2 implementations
- `WalkMovement`
- `RunMovement` (tốc độ nhanh hơn)

### Bước 3: Refactor Player

### Bước 4: Thêm `FlyMovement`
Không cần sửa Player!

---

## Kiểm tra

- ✅ Player không có switch/case về movement
- ✅ Có thể đổi movement runtime
- ✅ Thêm movement mới không sửa Player

---

## Kiến thức rút ra

| Khái niệm | Áp dụng |
|-----------|---------|
| **Identify what varies** | Movement logic |
| **Encapsulate** | Tách ra IMovement |
| **Open for extension** | Thêm movement mới dễ dàng |
| **Closed for modification** | Không sửa Player |
| **Strategy Pattern preview** | IMovement + implementations |

---

## Áp dụng trong game

Những gì thường thay đổi:
- AI behaviors
- Attack patterns
- Movement styles
- Animation logic
- Sound effects

---

## Commit

```
feat(principles): implement encapsulate what changes
```

Tiếp theo: [Principle 2: Composition Over Inheritance](./Principle2_CompositionOverInheritance.md)
