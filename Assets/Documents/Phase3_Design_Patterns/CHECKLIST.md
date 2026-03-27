# Phase 3: Checklist

Hoàn thành Phase 3 khi bạn đánh dấu được tất cả các mục dưới đây.

---

## Kiến thức PHẢI RÚT RA

### Pattern 1: Strategy
- [ ] Biết khi nào dùng Strategy thay vì if/else
- [ ] Hiểu cách swap behavior runtime
- [ ] Áp dụng được cho AI, Movement, Attack
- [ ] **Liên hệ**: "Encapsulate What Changes" từ Phase 2

### Pattern 2: Observer
- [ ] Hiểu Pub/Sub model
- [ ] Biết implement event system
- [ ] Tránh được memory leak với unsubscribe
- [ ] **Liên hệ**: "Low Coupling" từ Phase 2

### Pattern 3: Object Pool
- [ ] Hiểu vấn đề GC trong game
- [ ] Biết implement pool cơ bản
- [ ] Áp dụng cho bullets, enemies, VFX
- [ ] Biết dùng Unity's built-in ObjectPool

### Pattern 4: Factory
- [ ] Phân biệt Factory Method vs Simple Factory
- [ ] Biết khi nào dùng Factory thay vì new trực tiếp
- [ ] Áp dụng cho spawning system
- [ ] **Liên hệ**: "Program to Abstraction" từ Phase 2

### Pattern 5: State
- [ ] Hiểu State Machine concept
- [ ] Biết implement State pattern
- [ ] Áp dụng cho Player, Enemy, UI states
- [ ] Phân biệt **State vs Strategy**

### Pattern 6: Command
- [ ] Hiểu Command như "action object"
- [ ] Biết implement undo/redo
- [ ] Áp dụng cho input buffer, replay

### Pattern 7: Singleton ⚠️
- [ ] Hiểu Singleton pattern cơ bản
- [ ] Biết **pitfalls** và tại sao gây tranh cãi
- [ ] Biết **alternatives** (DI, Service Locator)
- [ ] Biết khi nào OK / không OK dùng
- [ ] **Video**: [Reference Objects In Your Unity Game Flawlessly](../RESOURCES.md#phase-3-design-patterns)

---

## Principle → Pattern Mapping

| Phase 2 Principle | Pattern trong Phase 3 |
|-------------------|----------------------|
| Encapsulate What Changes | **Strategy** (swap algorithms) |
| Composition Over Inheritance | **Object Pool** (reuse objects) |
| Program to Abstraction | **Factory** (create via abstraction) |
| Low Coupling / High Cohesion | **Observer** (event-based) |

---

## Tài liệu PHẢI ĐỌC

| Tài liệu | Chương/Phần | Lý do |
|----------|-------------|-------|
| Head First Design Patterns | Chapter 1: Strategy | Foundation |
| Head First Design Patterns | Chapter 2: Observer | Core pattern |
| Game Programming Patterns | [Object Pool](../RESOURCES.md#phase-3-design-patterns) | Game-specific |
| Game Programming Patterns | [State](../RESOURCES.md#phase-3-design-patterns) | Game-specific |
| Game Programming Patterns | [Command](../RESOURCES.md#phase-3-design-patterns) | Game-specific |

---

## Video NÊN XEM (khi stuck)

| Video | Thời lượng | Khi nào xem |
|-------|------------|-------------|
| [Strategy Pattern - Derek Banas](../RESOURCES.md#phase-3-design-patterns) | ~10 phút | Pattern 1 |
| [Object Pooling - Brackeys](../RESOURCES.md#phase-3-design-patterns) | ~15 phút | Pattern 3 |
| [State Machine - Jason Weimann](../RESOURCES.md#phase-3-design-patterns) | ~20 phút | Pattern 5 |

---

## Tự kiểm tra — Câu hỏi sâu

### Concept Questions
1. **Strategy vs State:** Khác nhau như thế nào?
2. **Observer:** Làm sao tránh memory leak?
3. **Object Pool:** Khi nào pool, khi nào instantiate?
4. **Factory:** Factory khác gì với constructor?
5. **Command:** Command pattern giúp gì cho undo?

### Application Questions
6. Nếu enemy cần đổi behavior dựa trên health, dùng Strategy hay State?
7. Khi nào cần Event Bus vs direct event subscription?
8. Object Pool có thể kết hợp với Factory không? Làm sao?

### Recall Phase 2
9. "Encapsulate What Changes" dẫn đến pattern nào?
10. Task_Events ở Phase 1 là foundation cho pattern nào?

---

## Kết nối với Phase tiếp theo

> [!NOTE]
> Phase 3 dạy **individual patterns**. Phase 4 dạy **architecture** — cách kết hợp nhiều patterns thành hệ thống:
> - Service Locator = Registry cho services
> - Dependency Injection = Automated wiring
> - Event-Driven Architecture = Observer at scale

---

## Hoàn thành

Khi đánh dấu hết checklist, bạn đã sẵn sàng cho Phase 4.
