# Phase 2: Design Principles

> *"Good OO designs are reusable, extensible, and maintainable."*  
> — Head First Design Patterns

4 mindsets quan trọng nhất trước khi học Design Patterns.

---

## Bạn đã thấy những Principles này ở Phase 1!

Ở Phase 1, bạn đã **trải nghiệm** những principles này mà không biết tên:

| Phase 1 Task | Bạn đã làm gì? | Phase 2 Principle sẽ học |
|--------------|----------------|--------------------------|
| **Task_Car** | Tách `speed`, `fuel` thành private fields | → Encapsulate What Changes |
| **Task_Weapon** | Car HAS Weapon (composition) | → Composition Over Inheritance |
| **Task_WeaponTypes** | Car dùng `IWeapon` interface | → Program to Abstraction |
| **Task_Events** | Dùng events để notify | → Low Coupling / High Cohesion |

Phase 2 **đặt tên chính thức** cho những gì bạn đã tự khám phá!

---

## SOLID là gì?

SOLID là 5 nguyên tắc thiết kế OOP (từ Robert C. Martin - "Uncle Bob"):

| Chữ | Tên đầy đủ | Ý nghĩa ngắn gọn | Ví dụ vi phạm |
|-----|------------|------------------|---------------|
| **S** | Single Responsibility | Mỗi class chỉ làm 1 việc | `PlayerController` vừa xử lý input, vừa render UI, vừa save game |
| **O** | Open/Closed | Mở để mở rộng, đóng để sửa đổi | Thêm enemy type → phải sửa `switch` trong code cũ |
| **L** | Liskov Substitution | Class con thay thế được class cha | `Square` extends `Rectangle` nhưng `setWidth()` hoạt động sai |
| **I** | Interface Segregation | Chia nhỏ interface | `IPlayer` có `Fly()` nhưng `GroundPlayer` không bay được |
| **D** | Dependency Inversion | Phụ thuộc abstraction, không phải implementation | `GameManager` gọi trực tiếp `PlayerPrefs.Save()` |

---

## Tại sao không dạy SOLID trực tiếp?

SOLID giống như **5 điều luật** — nhưng luật không giải thích **tại sao** phải tuân theo.

Phase này dạy **4 mindsets** — là bản chất đằng sau SOLID:

| Mindset của Phase 2 | Bao gồm SOLID nào | Dẫn đến Pattern nào |
|---------------------|-------------------|---------------------|
| Encapsulate What Changes | O (Open/Closed) | **Strategy** |
| Composition Over Inheritance | L, I (Liskov, ISP) | **Decorator**, **Component** |
| Program to Abstraction | D (Dependency Inversion) | **Factory**, **Abstract Factory** |
| Low Coupling / High Cohesion | S (Single Responsibility) | **Observer**, **Command** |

Nếu hiểu 4 mindsets này, SOLID sẽ tự nhiên mà đến, và Design Patterns cũng vậy!

---

## Phân biệt: Dependency Inversion vs Dependency Injection

Hai khái niệm này HAY BỊ NHẦM:

| Khái niệm | Là gì | Ví dụ |
|-----------|-------|-------|
| **Dependency Inversion (DIP)** | NGUYÊN TẮC — Depend on abstraction, not implementation | Dùng `ISaveService` thay vì `PlayerPrefs` |
| **Dependency Injection (DI)** | KỸ THUẬT — Cách truyền dependency vào | Inject qua constructor: `new GameManager(saveService)` |

```csharp
// Dependency Inversion: phụ thuộc interface
public class GameManager
{
    private ISaveService saveService;  // ← DIP: depend on abstraction
    
    // Dependency Injection: được truyền vào từ bên ngoài
    public GameManager(ISaveService saveService)  // ← DI: inject dependency
    {
        this.saveService = saveService;
    }
}
```

**DIP** = quy tắc cần theo  
**DI** = cách thực hiện DIP

---

## Mục tiêu

Sau phase này, bạn sẽ:
- Biết **tách phần dễ thay đổi** ra
- Ưu tiên **Composition** hơn Inheritance
- Code theo **abstraction**, không phải implementation
- Thiết kế **low coupling, high cohesion**

👉 Xem [CHECKLIST.md](./CHECKLIST.md) để biết chi tiết những gì cần đạt được.

---

## Các Principle

| Principle | Feature minh họa | Thời gian | Pattern Preview |
|-----------|------------------|-----------|-----------------|
| [1. Encapsulate What Changes](./Principle1_EncapsulateChange.md) | Movement System | 1-2 giờ | Strategy |
| [2. Composition Over Inheritance](./Principle2_CompositionOverInheritance.md) | Ability System | 1-2 giờ | Decorator |
| [3. Program to Abstraction](./Principle3_ProgramToAbstraction.md) | Save System | 1-2 giờ | Factory |
| [4. Low Coupling / High Cohesion](./Principle4_LowCouplingHighCohesion.md) | Event System | 1-2 giờ | Observer |

---

## Quy tắc

1. Mỗi principle gắn với 1 feature thực tế
2. Làm "cách sai" trước, sau đó refactor
3. Commit sau mỗi principle

---

## Khi hoàn thành

Commit với message:
```
feat(principles): complete phase 2 design principles
```

Sau đó chuyển sang [Phase 3: Design Patterns](../Phase3_Design_Patterns/README.md)
