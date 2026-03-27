# Principle 2: Composition Over Inheritance

> *"Favor composition over inheritance."*  
> — Head First Design Patterns

Ưu tiên kết hợp behaviors thay vì kế thừa.

---

## Recall Phase 1 🔙

Bạn đã thấy principle này:
- **Task_Weapon**: Survivor HAS Weapon (không IS Weapon)
- **Task_WeaponTypes**: Pistol, Shotgun implement IWeapon (không kế thừa từ Weapon base)
- **SimUDuck story**: Vịt cao su bay được vì dùng inheritance!

---

## Feature: Ability System

Character cần có nhiều abilities:
- Attack (melee/ranged)
- Heal
- Shield
- Dash

Một character có thể có nhiều abilities khác nhau.

---

## Phần 1: Cách sai — Deep Inheritance

```
Character
├── MeleeCharacter
│   ├── MeleeWithHeal
│   └── MeleeWithShield
├── RangedCharacter
│   ├── RangedWithHeal
│   └── RangedWithDash
└── HybridCharacter  ← Melee + Ranged???
```

### Vấn đề

| Issue | Problem |
|-------|---------|
| Explosion of classes | 2 attacks × 4 abilities = 8+ classes |
| Diamond problem | MeleeWithHealAndShield? |
| Runtime lock | Không thể thay đổi abilities |
| Duplicate code | Heal logic ở nhiều nhánh |

Đây chính là vấn đề của **SimUDuck**!

---

## Phần 2: Giải pháp — Composition

### Interface cho ability

```csharp
public interface IAbility
{
    string Name { get; }
    float Cooldown { get; }
    bool IsReady { get; }
    
    void Activate();
}
```

### Các abilities

```csharp
public class MeleeAttack : IAbility
{
    public string Name => "Melee Attack";
    public float Cooldown => 1f;
    public bool IsReady => Time.time >= nextUseTime;
    
    private float nextUseTime;
    
    public void Activate()
    {
        if (!IsReady) return;
        
        // Melee attack logic
        nextUseTime = Time.time + Cooldown;
    }
}

public class HealAbility : IAbility
{
    public string Name => "Heal";
    public float Cooldown => 5f;
    public bool IsReady => Time.time >= nextUseTime;
    
    private float nextUseTime;
    private int healAmount = 20;
    
    public void Activate()
    {
        if (!IsReady) return;
        
        // Heal logic
        nextUseTime = Time.time + Cooldown;
    }
}
```

### Character với Composition

```csharp
public class Character : MonoBehaviour
{
    private List<IAbility> abilities = new List<IAbility>();
    
    public void AddAbility(IAbility ability)
    {
        abilities.Add(ability);
    }
    
    public void RemoveAbility(IAbility ability)
    {
        abilities.Remove(ability);
    }
    
    public void UseAbility(int index)
    {
        if (index < abilities.Count)
        {
            abilities[index].Activate();
        }
    }
}
```

---

## Phần 3: Unity Component Style

Từ **Game Programming Patterns**:

> *"The Component pattern allows a single entity to span multiple domains without coupling the domains to each other."*

Unity đã dùng Composition sẵn!

```csharp
// Thay vì inheritance
public class MeleeCharacter : Character { }

// Dùng components
public class Character : MonoBehaviour
{
    // Có sẵn các components
}

// Thêm ability như component
public class MeleeAttackComponent : MonoBehaviour, IAbility
{
    public void Activate() { /* ... */ }
}
```

### Setup trong Unity

```csharp
// Build character bằng components
GameObject warrior = new GameObject("Warrior");
warrior.AddComponent<Character>();
warrior.AddComponent<MeleeAttackComponent>();
warrior.AddComponent<ShieldComponent>();
```

---

## 🎉 Pattern Previews

### Decorator Pattern
> *"Attach additional responsibilities to an object dynamically."*

Composition cho phép "wrap" thêm behaviors!

### Component Pattern
Từ Game Programming Patterns — decouples domains trong game objects.

> [!TIP]
> Ở Phase 3, bạn sẽ học **Decorator Pattern** chính thức!

---

## Phần 4: Thực hành

### Bước 1: Tạo `IAbility` interface

### Bước 2: Tạo 3 abilities
- `MeleeAttack`
- `RangedAttack`
- `HealAbility`

### Bước 3: Tạo `Character` class
- Có `List<IAbility>`
- Methods: `AddAbility`, `UseAbility`

### Bước 4: Test combinations
- Warrior: Melee + Heal
- Mage: Ranged + Heal
- Paladin: Melee + Ranged + Heal

---

## Kiểm tra

- ✅ Không có inheritance hierarchy
- ✅ Character có thể có bất kỳ combination nào
- ✅ Thêm ability mới không sửa Character
- ✅ Có thể add/remove abilities runtime

---

## Kiến thức rút ra

| Khái niệm | Áp dụng |
|-----------|---------|
| **Composition** | Character HAS abilities |
| **vs Inheritance** | Character IS NOT a type |
| **Flexibility** | Mix and match |
| **Runtime changes** | Add/remove behaviors |
| **Component Pattern** | Unity's design philosophy |

---

## So sánh

| Inheritance | Composition |
|-------------|-------------|
| IS-A relationship | HAS-A relationship |
| Compile-time | Runtime flexibility |
| Single inheritance (C#) | Multiple behaviors |
| Rigid | Flexible |
| Share implementation | Share contract (interface) |

---

## Khi nào dùng Inheritance?

Inheritance vẫn hữu ích khi:
- Có quan hệ **IS-A rõ ràng** (Dog IS-A Animal)
- Muốn **share implementation** (không chỉ interface)
- Hierarchy **ổn định, ít thay đổi**
- **Max 1-2 levels** deep

---

## Commit

```
feat(principles): implement composition over inheritance
```

Tiếp theo: [Principle 3: Program to Abstraction](./Principle3_ProgramToAbstraction.md)
