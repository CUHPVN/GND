# Phase 4: Checklist

Hoàn thành Phase 4 khi bạn đánh dấu được tất cả các mục dưới đây.

---

## Kiến thức PHẢI RÚT RA

### Module 1: MVC Foundation
- [ ] Hiểu vai trò của Model, View, Controller
- [ ] Biết data flow trong MVC
- [ ] Nhận ra vấn đề của MVC thuần trong game
- [ ] **Liên hệ**: Observer pattern từ Phase 3 dùng cho Model → View

### Module 2: MVP in Unity
- [ ] Phân biệt MVC vs MVP
- [ ] Biết tại sao MVP phù hợp hơn cho Unity
- [ ] Implement được Presenter pattern
- [ ] **Liên hệ**: Interface từ Phase 1, Low Coupling từ Phase 2

### Module 3: Reality Check
- [ ] Hiểu game loop khác web app
- [ ] Biết Unity dùng Component-based
- [ ] Nhận ra khi nào cần/không cần strict architecture
- [ ] **Liên hệ**: Composition Over Inheritance từ Phase 2

### Module 4: Mini Project
- [ ] Áp dụng được OOP từ Phase 1
- [ ] Áp dụng được Principles từ Phase 2
- [ ] Áp dụng được Patterns từ Phase 3
- [ ] Tổ chức code theo MVP

---

## Phase 1-3 Integration Map

| Concept | Phase | Where in Architecture |
|---------|-------|----------------------|
| Encapsulation | 1 | Model classes |
| Interfaces | 1 | IView, IModel |
| Low Coupling | 2 | MVP separation |
| Composition | 2 | View HAS presenter |
| Observer | 3 | Model → View events |
| Factory | 3 | Service creation |
| State | 3 | Game states |

---

## Tài liệu PHẢI ĐỌC

| Tài liệu | Chương/Phần | Lý do |
|----------|-------------|-------|
| Game Programming Patterns | [Game Loop](../RESOURCES.md#phase-4-architecture) | Hiểu game khác app |
| Game Programming Patterns | [Component](../RESOURCES.md#phase-2-design-principles) | Unity architecture |
| Unity Best Practices | [Project Architecture](../RESOURCES.md#phase-4-architecture) | Production patterns |

---

## Video NÊN XEM (khi stuck)

| Video | Thời lượng | Khi nào xem |
|-------|------------|-------------|
| [MVC vs MVP - Jason Weimann](../RESOURCES.md#phase-4-architecture) | ~15 phút | Module 1-2 |
| [Clean Architecture Unity - Infallible Code](../RESOURCES.md#phase-4-architecture) | ~25 phút | Module 3 |
| [Project Structure - git-amend](../RESOURCES.md#phase-4-architecture) | ~20 phút | Module 4 |

---

## Tự kiểm tra — Câu hỏi sâu

### Concept Questions
1. **MVC vs MVP:** Presenter khác Controller như thế nào?
2. **Unity specifics:** Tại sao Unity không dùng pure MVC?
3. **When to use:** Khi nào cần strict architecture, khi nào không?

### Integration Questions
4. **Observer**: Model notify View như thế nào trong MVP?
5. **Factory**: Làm sao tạo services trong Bootstrap?
6. **Strategy**: Fit Strategy pattern vào MVP architecture như thế nào?

### Recall Previous Phases
7. Phase 2 "Low Coupling" áp dụng như thế nào cho MVP?
8. Phase 3 Observer pattern dùng ở đâu trong architecture?
9. Model class cần những gì từ Phase 1 OOP?

---

## Grading Your Learning

Khi hoàn thành Phase 4, bạn nên đánh giá được:

| Level | Mô tả |
|-------|-------|
| **Basic** | Hiểu MVC/MVP diagram |
| **Intermediate** | Implement được MVP kiểu básico |
| **Advanced** | Biết khi nào cần/không cần strict architecture |
| **Expert** | Có thể adapt architecture cho từng project |

---

## Hoàn thành

Khi đánh dấu hết checklist, bạn đã hoàn thành lộ trình. 🎉
