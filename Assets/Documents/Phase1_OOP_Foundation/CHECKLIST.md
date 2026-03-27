# Phase 1: Checklist

Hoàn thành Phase 1 khi bạn đánh dấu được tất cả các mục dưới đây.

---

## Kiến thức PHẢI RÚT RA

### Module 1: Modeling
- [ ] Hiểu Class là công cụ modeling, không phải lý thuyết trừu tượng
- [ ] Biết khi nào dùng `private` vs `public`
- [ ] Hiểu **Encapsulation = bảo vệ state + expose behavior**
- [ ] Biết tại sao không nên truy cập trực tiếp vào fields
- [ ] Nhận ra **"what varies"** trong code

### Module 2: Variation
- [ ] Phân biệt được khi nào dùng Inheritance vs Interface
- [ ] Hiểu vấn đề của deep inheritance hierarchy
- [ ] Biết **Composition = kết hợp nhiều behaviors nhỏ**
- [ ] Hiểu **"Program to interface"** nghĩa là gì
- [ ] Nhận ra khi inheritance gây ra vấn đề (như SimUDuck!)

### Module 3: Dependency
- [ ] Nhận ra được **tight coupling** trong code
- [ ] Biết dùng **Dependency Injection đơn giản** thay vì `FindObjectOfType`
- [ ] Hiểu **Events giúp loose coupling** như thế nào
- [ ] Biết tách abstraction (interface) để giảm phụ thuộc
- [ ] Hiểu **Law of Demeter** — chỉ nói chuyện với "friends" trực tiếp

---

## Design Maxims (Từ Head First Design Patterns)

Sau Phase 1, bạn nên hiểu ý nghĩa thực tế của:

| Maxim | Bạn đã thấy ở đâu? |
|-------|-------------------|
| **"Encapsulate what varies"** | Module 1: private fields, Module 2: behaviors tách riêng |
| **"Program to interfaces"** | Module 2: IWeapon, Module 3: IDamageable |
| **"Favor composition over inheritance"** | Module 2: so sánh Zombie hierarchy vs Weapon composition |
| **"Strive for loose coupling"** | Module 3: DI đơn giản, Events, Interface abstraction |

---

## Tài liệu PHẢI ĐỌC

| Tài liệu | Chương/Phần | Lý do |
|----------|-------------|-------|
| Head First Design Patterns | Chapter 1: Intro (trang 1-30) | Hiểu SimUDuck story và 4 design maxims |
| Game Programming Patterns | [Architecture](../RESOURCES.md#phase-1-oop-foundation) | Context về game dev |

---

## Video NÊN XEM (khi stuck)

| Video | Thời lượng | Khi nào xem |
|-------|------------|-------------|
| [OOP Unity Tutorial](../RESOURCES.md#phase-1-oop-foundation) | ~20 phút | Sau khi làm Module 1 |
| [Composition vs Inheritance - Git Amend](../RESOURCES.md#phase-1-oop-foundation) | ~15 phút | Sau khi làm Module 2 |

---

## Tự kiểm tra — Câu hỏi sâu

Không chỉ định nghĩa, mà **trả lời được "tại sao"**:

### Nền tảng
1. **Class vs Object:** Khác nhau như thế nào?
2. **Encapsulation:** Tại sao cần `private`? (Hint: không chỉ về security!)

### Variation
3. **Inheritance:** Khi nào KHÔNG nên dùng? Cho ví dụ từ bài tập.
4. **Interface:** Khác gì với abstract class? Khi nào dùng cái nào?
5. **Composition:** Tại sao Survivor có `IWeapon` thay vì kế thừa từ Weapon?

### Dependency
6. **Coupling:** Làm sao **nhận ra** code đang tight coupling?
7. **FindObjectOfType:** Tại sao là code smell? Thay bằng gì?
8. **Events:** Tại sao UI fire events thay vì gọi trực tiếp GameManager?
9. **Law of Demeter:** Tại sao Slot không nên truy cập `Food.Config.Type`?

### Design Thinking
10. **"What varies?":** Trong Survivor + Weapon, cái gì hay thay đổi?
11. **Tradeoffs:** Khi nào inheritance vẫn là lựa chọn tốt?

---

## Kết nối với Phase tiếp theo

> [!NOTE]
> Phase 1 đặt nền tảng. Phase 2 (Design Principles) sẽ **formalize** những gì bạn đã trải nghiệm:
> - SOLID Principles
> - "Encapsulate What Varies" (chi tiết)
> - "Favor Composition Over Inheritance" (chi tiết)

---

## Hoàn thành

Khi đánh dấu hết checklist, bạn đã sẵn sàng cho Phase 2.
