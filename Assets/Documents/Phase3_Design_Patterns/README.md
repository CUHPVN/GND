# Phase 3: Design Patterns

> *"Patterns aren't invented, they're discovered."*  
> — Game Programming Patterns

Các patterns phổ biến nhất trong Game Development.

---

## Bạn đã PREVIEW những Patterns này ở Phase 2!

Ở mỗi Principle trong Phase 2, bạn đã **vô tình implement một phần** của các patterns:

| Phase 2 Principle | Bạn đã làm gì? | Pattern đầy đủ |
|-------------------|----------------|----------------|
| **Encapsulate What Changes** | Tạo `IMovement` interface, swap behaviors | → **Strategy** (Pattern 1) |
| **Low Coupling / High Cohesion** | Dùng C# events để notify | → **Observer** (Pattern 2) |
| **Program to Abstraction** | `ISaveService` interface | → **Factory** (Pattern 4) |
| **Encapsulate What Changes** | States có thể swap (tương tự IMovement) | → **State** (Pattern 5) |

Phase 3 sẽ dạy các patterns **đầy đủ và formal** dựa trên nền tảng bạn đã có!

---

## Tại sao học Patterns?

Patterns là **giải pháp đã được chứng minh** cho các vấn đề thường gặp.

> *"Design patterns are tools, not rules."*

Bạn không cần học hết catalog — chỉ cần **7 patterns này** cho game dev.

---

## Patterns trong Phase này

### MUST — Phải biết

| Pattern | Vấn đề giải quyết | Feature minh họa |
|---------|-------------------|------------------|
| [1. Strategy](./Pattern1_Strategy.md) | Thay đổi behavior runtime | AI Behavior |
| [2. Observer](./Pattern2_Observer.md) | Notify nhiều objects | Event System |
| [3. Object Pool](./Pattern3_ObjectPool.md) | Tránh GC, tối ưu spawn | Bullet Spawning |
| [4. Factory](./Pattern4_Factory.md) | Tạo objects linh hoạt | Enemy Spawning |

### GOOD — Nên biết

| Pattern | Vấn đề giải quyết | Feature minh họa |
|---------|-------------------|------------------|
| [5. State](./Pattern5_State.md) | Quản lý trạng thái phức tạp | Player States |
| [6. Command](./Pattern6_Command.md) | Undo, replay, input buffer | Input System |

### CAUTION — Biết nhưng cẩn thận!

| Pattern | Vấn đề giải quyết | ⚠️ Lưu ý |
|---------|-------------------|----------|
| [7. Singleton](./Pattern7_Singleton.md) | Global access | Dễ lạm dụng, có alternatives tốt hơn |

---

## Cách học Pattern hiệu quả

**ĐỪNG LÀM:**
```
Slide → Definition → Diagram → Quên
```

**HÃY LÀM:**
```
1. Gặp problem
2. Cố giải quyết (đau đớn)
3. Refactor together
4. Đặt tên pattern SAU CÙNG
```

---

## Mối liên hệ với Phase 2

| Phase 2 Principle | Pattern áp dụng |
|-------------------|-----------------|
| Encapsulate What Changes | **Strategy**, State |
| Composition Over Inheritance | Strategy, **Object Pool** |
| Program to Abstraction | **Factory** |
| Low Coupling / High Cohesion | **Observer**, Command |

👉 Xem [CHECKLIST.md](./CHECKLIST.md) để biết chi tiết những gì cần đạt được.

---

## Quy tắc

1. Làm MUST patterns trước
2. Mỗi pattern = 1 feature hoàn chỉnh
3. Commit sau mỗi pattern

---

## Khi hoàn thành

Commit với message:
```
feat(patterns): complete phase 3 design patterns
```

Sau đó chuyển sang [Phase 4: Architecture](../Phase4_Architecture/README.md)
