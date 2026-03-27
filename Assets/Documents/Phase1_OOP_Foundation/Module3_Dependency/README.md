# Module 3: Dependency Awareness

> *"Strive for loosely coupled designs between objects that interact."*  
> — Head First Design Patterns

Hiểu coupling và học cách giảm nó.

---

## Vấn đề từ Module 1

Nhớ lại code Zombie:

```csharp
public class Zombie : MonoBehaviour
{
    private void Start()
    {
        target = FindObjectOfType<Survivor>().transform;
    }
}
```

### Tight Coupling Issues

| Vấn đề | Tại sao xấu? |
|--------|-------------|
| Zombie phụ thuộc trực tiếp vào Survivor | Không test được riêng |
| `FindObjectOfType` chậm | Performance issue |
| Nếu Survivor chưa spawn | NullReferenceException |
| Đổi tên Survivor class | Phải sửa tất cả Zombie |

---

## Loose Coupling là gì?

> Objects biết **ít nhất có thể** về nhau nhưng vẫn hoạt động cùng nhau.

| Tight Coupling | Loose Coupling |
|----------------|----------------|
| A biết chính xác B là gì | A biết B "can do" something |
| A tự tìm B | B được đưa vào A |
| Thay B → sửa A | Thay B → A không đổi |

---

## Ví dụ thực tế: Foodie Sizzle

Trong game match-3 [**Foodie Sizzle**](../../RESOURCES.md#game-context--references), xét cấu trúc:

```mermaid
flowchart LR
    Slot --> Food
    Food --> FoodType
    FoodType -.->|via| FoodConfig
```

### ❌ COUPLING XẤU: Truy cập xuyên qua hierarchy

```csharp
// Slot kiểm tra match 3 bằng cách truy cập trực tiếp Type
public class Slot : MonoBehaviour
{
    public Food food;
    
    public bool CanMatch(Slot other)
    {
        // ❌ CẤM! Slot truy cập trực tiếp FoodType
        return food.config.type == other.food.config.type;
    }
}
```

**Vấn đề:**
- Slot biết quá nhiều về internal structure của Food
- Nếu Food thay đổi cách lưu type → phải sửa Slot
- Không thể test Slot độc lập

### ✅ COUPLING TỐT: Thông qua Food

```csharp
// Food expose method để kiểm tra, Slot không cần biết internal
public class Food : MonoBehaviour
{
    [SerializeField] private FoodConfig config;
    
    public bool IsSameType(Food other)
    {
        return config.type == other.config.type;
    }
}

public class Slot : MonoBehaviour
{
    public Food food;
    
    public bool CanMatch(Slot other)
    {
        // ✅ NÊN! Thông qua Food's public method
        return food.IsSameType(other.food);
    }
}
```

> [!IMPORTANT]
> **Nguyên tắc**: Tránh chuỗi phụ thuộc sâu.
> Hãy tương tác qua interface/method public của object,
> không truy cập cấu trúc nội bộ của nó.

---

## 3 Kỹ thuật trong Module này

| Task | Kỹ thuật | Vấn đề giải quyết |
|------|----------|-------------------|
| [Task: Coupling](./Task_Coupling.md) | **Dependency Injection (đơn giản)**, Interface | Direct dependencies |
| [Task: Events](./Task_Events.md) | C# Events, UnityEvents | Bidirectional coupling |

---

## Kết nối với Design Patterns

Module này là **foundation** cho nhiều patterns:

| Pattern (Phase 3) | Kỹ thuật ở đây |
|-------------------|----------------|
| **Observer** | Events |
| **Strategy** | Interface injection |
| **Factory** | Dependency Injection |

> [!TIP]
> Events bạn học ở đây chính là cơ sở cho **Observer Pattern**!

---

## From Game Programming Patterns

Trong game dev, có nhiều cách quản lý dependencies:

| Cách | Pro | Con |
|------|-----|-----|
| **Dependency Injection (đơn giản)** | Clean, testable | Cần setup |
| **Service Locator** | Flexible, global | Có thể abuse |
| **Events** | Very loose | Harder to debug |
| **FindObjectOfType** | Easy | **Tight coupling!** |

Bạn sẽ học các cách hợp lý trong module này.

---

## Milestone

Sau khi hoàn thành, commit:
```
feat(oop): loose coupling foundation
```

---

## Hoàn thành Phase 1

Sau module này, commit:
```
feat(oop): complete phase 1 foundation
```

Tiếp theo: [Phase 2: Design Principles](../../Phase2_Design_Principles/README.md)
