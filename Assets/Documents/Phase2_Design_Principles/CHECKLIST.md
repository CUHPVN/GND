# Phase 2: Checklist

Hoàn thành Phase 2 khi bạn đánh dấu được tất cả các mục dưới đây.

---

## Kiến thức PHẢI RÚT RA

### Principle 1: Encapsulate What Changes
- [ ] Nhận ra được phần code nào **dễ thay đổi**
- [ ] Biết tách behavior ra khỏi class chính
- [ ] Hiểu **"identify aspects that vary and separate them"**
- [ ] Liên hệ với **Open/Closed Principle**

### Principle 2: Composition Over Inheritance
- [ ] Biết khi nào Inheritance **không phù hợp**
- [ ] Hiểu **Component-based design** (Unity style)
- [ ] Biết kết hợp behaviors thay vì kế thừa
- [ ] Liên hệ với **Liskov** và **Interface Segregation** Principles

### Principle 3: Program to Abstraction
- [ ] Hiểu tại sao nên **depend on interface**
- [ ] Biết inject dependency qua constructor
- [ ] Có thể **swap implementation** mà không sửa caller
- [ ] Liên hệ với **Dependency Inversion Principle**

### Principle 4: Low Coupling / High Cohesion
- [ ] Định nghĩa được **Coupling** và **Cohesion**
- [ ] Biết dùng **Events** để giảm coupling
- [ ] Hiểu **Single Responsibility** ở class level
- [ ] Liên hệ với **Single Responsibility Principle**

---

## SOLID Mapping (Chi tiết)

| SOLID | Full Name | Phase 2 Principle | Bạn đã thấy ở Phase 1 |
|-------|-----------|-------------------|----------------------|
| **S** | Single Responsibility | Low Coupling / High Cohesion | Player, Car, Weapon riêng biệt |
| **O** | Open/Closed | Encapsulate What Changes | Thêm weapon mới không sửa Car |
| **L** | Liskov Substitution | Composition Over Inheritance | EnemyBase → MeleeEnemy, RangedEnemy |
| **I** | Interface Segregation | Composition Over Inheritance | IWeapon, IReloadable riêng |
| **D** | Dependency Inversion | Program to Abstraction | Car dùng IWeapon, không Gun |

---

## Pattern Connections

| Phase 2 Principle | Dẫn đến Pattern (Phase 3) |
|-------------------|--------------------------|
| Encapsulate What Changes | **Strategy** — swap algorithms |
| Composition Over Inheritance | **Decorator**, **Component** |
| Program to Abstraction | **Factory**, **Abstract Factory** |
| Low Coupling / High Cohesion | **Observer**, **Command** |

---

## Tài liệu PHẢI ĐỌC

| Tài liệu | Chương/Phần | Lý do |
|----------|-------------|-------|
| Head First Design Patterns | Chapter 1: Strategy (trang 1-32) | Foundation của 4 principles |
| Game Programming Patterns | [Component](../RESOURCES.md#phase-2-design-principles) | Composition trong game |

---

## Video NÊN XEM (khi stuck)

| Video | Thời lượng | Khi nào xem |
|-------|------------|-------------|
| [SOLID Principles - Infallible Code](../RESOURCES.md#phase-2-design-principles) | ~25 phút | Sau khi làm xong 4 principles |
| [Composition in Unity - git-amend](../RESOURCES.md#phase-2-design-principles) | ~20 phút | Sau Principle 2 |

---

## Tự kiểm tra — Câu hỏi sâu

### Concept Questions
1. **Encapsulate What Changes:** Cho ví dụ 1 phần code dễ thay đổi trong game
2. **Composition:** Tại sao Unity dùng Component thay vì Inheritance?
3. **Abstraction:** Interface và Abstract class khác nhau thế nào?
4. **Coupling:** Định nghĩa "loosely coupled" bằng lời của bạn

### Application Questions
5. **Strategy Pattern:** Giải thích tại sao IMovement + WalkMovement/FlyMovement là Strategy Pattern
6. **DIP vs DI:** Phân biệt hai khái niệm này và cho ví dụ
7. **Cohesion:** Làm sao biết một class có "high cohesion"?
8. **Tradeoffs:** Khi nào inheritance vẫn là lựa chọn đúng?

### Recall Phase 1
9. Trong Phase 1, bạn đã áp dụng "Program to Interface" ở task nào?
10. Task_Events đã giới thiệu foundation cho principle nào?

---

## Kết nối với Phase tiếp theo

> [!NOTE]
> Phase 2 dạy **principles**. Phase 3 dạy **patterns** — là cách áp dụng principles vào các vấn đề cụ thể:
> - **Strategy Pattern** = Encapsulate What Changes applied
> - **Observer Pattern** = Low Coupling applied
> - **Decorator Pattern** = Composition applied

---

## Hoàn thành

Khi đánh dấu hết checklist, bạn đã sẵn sàng cho Phase 3.
