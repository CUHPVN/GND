# Phase 1: OOP Foundation

> *"The one constant in software development is CHANGE."*  
> — Head First Design Patterns

OOP không phải lý thuyết — OOP là công cụ modeling. Phase này dạy bạn cách **nghĩ theo objects** và đặt nền tảng cho Design Patterns.

---

## Tại sao Phase này quan trọng?

Trong **Head First Design Patterns** (Chapter 1), câu chuyện **SimUDuck** minh họa một bài học quan trọng:

> **Bối cảnh:** Joe làm việc tại công ty game, dự án SimUDuck là game mô phỏng vịt bơi và kêu.
> 
> **Ban đầu:** Dùng inheritance đơn giản
> ```
> Duck (base class)
>   ├── quack(), swim(), display()
>   ├── MallardDuck (vịt xanh)
>   └── RedheadDuck (vịt đầu đỏ)
> ```
>
> **Vấn đề:** Sếp yêu cầu thêm `fly()` → Joe thêm vào Duck base class → **RubberDuck cũng bay được!** 😱
>
> **Fix tạm:** Override `fly()` trong RubberDuck để không làm gì → Nhưng mỗi loại vịt mới lại phải override!

**Bài học:** Inheritance tạo ra coupling chặt. Thay đổi ở base class ảnh hưởng TẤT CẢ subclasses. 

📖 *Đọc thêm: Head First Design Patterns, Chapter 1 "Welcome to Design Patterns" (trang 1-24)*

Phase 1 giúp bạn **tự trải nghiệm** các vấn đề này để hiểu **tại sao** cần Design Patterns.

---

## 🎮 Game Context: Last z: Survival Shooter

Phase này sử dụng context game **Survival Shooter** — kiểu *Vampire Survivors*, *Last War*.
Vui lòng xem vid sau để hiểu Gameplay hướng đến. [Last Z Survival Shooter](../RESOURCES.md#game-context--references)

### Core Gameplay
```
🎮 Di chuyển (trái phải) → 🔫 Tự động bắn → 💀 Tiêu diệt zombies!
```

### Game Entities

| Entity | Mô tả | OOP Concept |
|--------|-------|-------------|
| **Survivor** | Nhân vật chính với health, speed | Encapsulation |
| **Zombie** | Kẻ địch đuổi theo, nhiều loại | Inheritance, Variation |
| **Weapon** | Auto-fire, nhiều loại | Interface, Composition |

---

## Mục tiêu

Sau phase này, bạn sẽ:
- Hiểu **Class = cách model khái niệm thực tế**
- Biết khi nào dùng Inheritance, Interface, Composition
- **Nhận ra coupling** và cách giảm nó
- Sẵn sàng học Design Principles ở Phase 2

👉 Xem [CHECKLIST.md](./CHECKLIST.md) để biết chi tiết những gì cần đạt được.

---

## Các Module

| Module | Nội dung | Thời gian ước tính |
|--------|----------|-------------------|
| [Module 1: Modeling](./Module1_Modeling/README.md) | Tạo objects với state + behavior | 2-3 giờ |
| [Module 2: Variation](./Module2_Variation/README.md) | Xử lý nhiều loại objects | 2-3 giờ |
| [Module 3: Dependency](./Module3_Dependency/README.md) | Hiểu và giảm coupling | 2-3 giờ |

---

## Design Maxims (Ghi nhớ!)

Từ **Head First Design Patterns** (Chapter 1, trang 9), 4 nguyên tắc nền tảng:

| # | Nguyên tắc | Ý nghĩa |
|---|-----------|---------|
| 1 | **Encapsulate what varies** | Tách riêng phần hay thay đổi |
| 2 | **Program to interfaces** | Phụ thuộc abstraction, không phải implementation |
| 3 | **Favor composition over inheritance** | HAS-A thường tốt hơn IS-A |
| 4 | **Strive for loose coupling** | Objects tương tác ít biết về nhau càng tốt |

> [!TIP]
> Bạn sẽ **tự khám phá** các nguyên tắc này qua các tasks. Sau Phase 1, quay lại đây và thử giải thích từng cái!

---

## Quy tắc

1. Làm tuần tự từ Module 1 → 3
2. Commit sau mỗi Task
3. Không skip bài nào — mỗi bài build lên bài trước

---

## Khi hoàn thành

Commit với message:
```
feat(oop): complete phase 1 foundation
```

Sau đó chuyển sang [Phase 2: Design Principles](../Phase2_Design_Principles/README.md)
