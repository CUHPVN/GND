# Module 4: Mini Project

> *"The proof of the pudding is in the eating."*

Wiring tất cả kiến thức từ Phase 1-4 thành game hoàn chỉnh. 🎮

---

## Mục tiêu

Tạo một mini game áp dụng:
- OOP từ Phase 1
- Design Principles từ Phase 2
- Design Patterns từ Phase 3
- MVP Architecture từ Phase 4

---

## Game: Simple Shooter

### Features

1. Player di chuyển và bắn
2. Enemies spawn và đuổi player
3. Bullets từ pool
4. Score system
5. Health system
6. Game over / Restart

---

## Architecture Overview

```
Bootstrap
    │
    ├── Models
    │   ├── PlayerModel
    │   ├── EnemyModel
    │   └── GameModel (score, state)
    │
    ├── Views
    │   ├── PlayerView
    │   ├── EnemyView
    │   ├── BulletView
    │   └── UIView (score, health, game over)
    │
    ├── Presenters
    │   ├── PlayerPresenter
    │   ├── EnemyPresenter
    │   └── GamePresenter
    │
    └── Services
        ├── ObjectPool<Bullet>  ← Phase 3
        ├── EnemyFactory        ← Phase 3
        └── EventBus           ← Phase 3
```

---

## 🔗 Áp dụng kiến thức từ TOÀN BỘ lộ trình

### Phase 1: OOP Foundation

| Concept | Áp dụng | Recall |
|---------|---------|--------|
| Encapsulation | Model classes với private fields | Principle 1 |
| Inheritance | EnemyBase → MeleeEnemy, RangedEnemy | Principle 2 |
| Interface | IEnemy, IWeapon, IPoolable | Principle 3 |
| Composition | Player HAS Weapon, HAS Health | Modeling |

### Phase 2: Design Principles

| Principle | Áp dụng | Recall |
|-----------|---------|--------|
| Encapsulate What Changes | Movement strategy, AI strategy | Principle 1 |
| Composition Over Inheritance | Player components | Principle 2 |
| Program to Abstraction | Services via interface | Principle 3 |
| Low Coupling | Event-driven communication | Principle 4 |

### Phase 3: Design Patterns

| Pattern | Áp dụng | Recall |
|---------|---------|--------|
| Strategy | Enemy AI behaviors | Pattern 1 |
| Observer | Event system cho score, damage | Pattern 2 |
| Object Pool | Bullets, VFX | Pattern 3 |
| Factory | Enemy spawning | Pattern 4 |
| State | Game states (Playing, Paused, GameOver) | Pattern 5 |
| Command | (Optional) Input replay | Pattern 6 |

### Phase 4: Architecture

| Component | Role |
|-----------|------|
| Models | Pure data + events |
| Views | MonoBehaviour, visuals only |
| Presenters | Wire Model ↔ View |
| Bootstrap | Create và wire mọi thứ |

---

## Folder Structure

```
Assets/
├── _Project/
│   └── MiniShooter/
│       ├── Scripts/
│       │   ├── Bootstrap/
│       │   │   └── GameBootstrap.cs
│       │   │
│       │   ├── Models/
│       │   │   ├── PlayerModel.cs
│       │   │   ├── EnemyModel.cs
│       │   │   └── GameModel.cs
│       │   │
│       │   ├── Views/
│       │   │   ├── PlayerView.cs
│       │   │   ├── EnemyView.cs
│       │   │   ├── BulletView.cs
│       │   │   └── GameUIView.cs
│       │   │
│       │   ├── Presenters/
│       │   │   ├── PlayerPresenter.cs
│       │   │   ├── EnemyPresenter.cs
│       │   │   └── GamePresenter.cs
│       │   │
│       │   ├── Services/           ← Phase 3 patterns!
│       │   │   ├── ObjectPool.cs
│       │   │   ├── EnemyFactory.cs
│       │   │   └── EventBus.cs
│       │   │
│       │   ├── Strategies/         ← Phase 3 Strategy!
│       │   │   ├── IAIBehavior.cs
│       │   │   ├── ChaseAI.cs
│       │   │   └── PatrolAI.cs
│       │   │
│       │   └── Interfaces/
│       │       ├── IEnemy.cs
│       │       ├── IPoolable.cs
│       │       └── IView.cs
│       │
│       ├── Prefabs/
│       ├── Scenes/
│       └── Data/
│           └── EnemyConfig.asset
```

---

## Implementation Plan

### Bước 1: Setup (30 phút)
- [ ] Tạo folder structure
- [ ] Tạo scene với ground
- [ ] Tạo player prefab

### Bước 2: Core Systems (1-2 giờ)
- [ ] `EventBus` — từ **Phase 3 Observer**
- [ ] `ObjectPool<T>` — từ **Phase 3 Object Pool**
- [ ] `GameModel` với states — từ **Phase 3 State**

### Bước 3: Player (1-2 giờ)
- [ ] `PlayerModel` — health, can move
- [ ] `PlayerView` — sprites, animations
- [ ] `PlayerPresenter` — wire together (**MVP**)
- [ ] Movement với **Strategy pattern**

### Bước 4: Weapons (1 giờ)
- [ ] `IWeapon` interface — **Program to Abstraction**
- [ ] `Gun` implementation
- [ ] Bullets từ **Object Pool**

### Bước 5: Enemies (1-2 giờ)
- [ ] `EnemyModel`, `EnemyView`
- [ ] `EnemyFactory` — từ **Phase 3 Factory**
- [ ] AI với **Strategy pattern**
- [ ] Spawner

### Bước 6: Game Flow (1 giờ)
- [ ] Game states (**State pattern**)
- [ ] Score system (**Observer**)
- [ ] Win/Lose conditions
- [ ] Restart

### Bước 7: UI (1 giờ)
- [ ] Health bar
- [ ] Score display
- [ ] Game over screen
- [ ] **MVP** wire cho UI

---

## Checklist hoàn thành

### OOP (Phase 1)
- [ ] Có ít nhất 1 abstract class
- [ ] Có ít nhất 2 interfaces
- [ ] Không có public fields (dùng properties)

### Principles (Phase 2)
- [ ] Không có God class (SRP)
- [ ] Composition được sử dụng
- [ ] Dependencies inject qua constructor/method

### Patterns (Phase 3)
- [ ] Strategy cho AI hoặc Movement
- [ ] Observer cho events
- [ ] Object Pool cho bullets
- [ ] Factory cho enemies

### Architecture (Phase 4)
- [ ] Models không biết Views
- [ ] Views không có business logic
- [ ] Presenters làm trung gian
- [ ] Bootstrap wire mọi thứ

---

## Lưu ý quan trọng

### Không cần hoàn hảo

Mini project này để **thực hành**, không phải AAA game.

**Ưu tiên:**
1. Hoàn thành core loop
2. Áp dụng đúng patterns
3. Code clean

**KHÔNG ưu tiên:**
- Graphics đẹp
- Nhiều features
- Polish

### Commit thường xuyên

```bash
feat(player): implement player movement with strategy
feat(enemy): implement enemy factory and spawner
feat(game): implement game state machine
```

---

## Khi hoàn thành

### Commit final

```
feat(project): complete mini shooter with full architecture
```

### Tự Review & Self-Correction 🛠️

Trước khi commit, hãy tự trả lời các câu hỏi sau (Trung thực nhé!):

| Câu hỏi | Câu trả lời mong đợi | Sai thì sửa sao? |
|---------|-----------------------|------------------|
| **View có chứa logic game không?** | KHÔNG. View chỉ nhận data và hiển thị. | Đưa logic đó về Presenter. |
| **Model có reference tới View không?** | KHÔNG. Model là pure C# (hoặc MonoBehaviour không ref UI). | Xóa reference, dùng C# Events (Observer). |
| **Presenter có reference tới concrete implementations?** | KHÔNG. Nên dùng Interface (`IPlayerView`, `IEnemy`). | Tạo interface và inject vào constructor. |
| **Có class nào dài hơn 300 dòng không?** | KHÔNG. | Tách nhỏ ra (SRP). Dùng Composition. |
| **Nếu xóa UI khỏi scene, game logic còn chạy không?** | CÓ. (Trừ việc hiển thị). | Đảm bảo Model & Presenter độc lập với View components. |

---

## 🎉 Chúc mừng!

Nếu bạn hoàn thành Mini Project này, bạn đã:

| Achievement | From |
|-------------|------|
| ✅ Hiểu OOP thực tế | Phase 1 |
| ✅ Áp dụng được Design Principles | Phase 2 |
| ✅ Sử dụng Design Patterns đúng chỗ | Phase 3 |
| ✅ Tổ chức code theo Architecture | Phase 4 |

---

## 🏆 Hoàn thành lộ trình!
