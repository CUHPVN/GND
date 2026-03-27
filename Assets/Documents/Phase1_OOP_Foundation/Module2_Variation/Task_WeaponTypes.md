# Task: Weapon Types

Tạo nhiều loại Weapon: Pistol, Shotgun, Laser.

---

## Mục tiêu

- Học **Interface** và tại sao nó flexible hơn inheritance
- Hiểu nguyên tắc **"Program to Interfaces"**
- **Bonus**: Đây chính là **Strategy Pattern**! 🎉

---

## Yêu cầu

Trong survival shooters, mỗi weapon có đặc điểm riêng:
- **Pistol**: Đơn phát, damage trung bình, fire rate cao
- **Shotgun**: Nhiều đạn mỗi lần bắn, damage cao ở gần, spread rộng
- **Laser**: Xuyên qua nhiều targets, damage thấp, continuous beam

---

## Phần 1: Vấn đề với Inheritance

Thử tạo:
```
Weapon (base)
├── Pistol
├── Shotgun
└── Laser
```

Câu hỏi:
- Laser có cần `bulletCount`? → Không! (continuous beam)
- Shotgun có cần `beamDuration`? → Không!
- Nếu thêm `LaserShotgun` (shotgun với laser projectiles)?

### Deep Inheritance = Problems

```
Weapon
├── ProjectileWeapon
│   ├── Pistol
│   └── Shotgun
└── BeamWeapon
    ├── Laser
    └── LaserShotgun  ← Vừa Projectile vừa Beam??
```

Đây là **Diamond Problem** mà SimUDuck gặp phải!

---

## Phần 2: Giải pháp — Interface

> *"Program to an interface, not an implementation."*  
> — Head First Design Patterns

### Định nghĩa interface

```csharp
public interface IWeapon
{
    int Damage { get; }
    float Cooldown { get; }
    bool CanAttack { get; }
    
    void Attack(Vector3 targetDirection);
}
```

### Pistol

```csharp
public class Pistol : MonoBehaviour, IWeapon
{
    [SerializeField] private int damage = 15;
    [SerializeField] private float cooldown = 0.3f;
    [SerializeField] private GameObject bulletPrefab;
    
    private float nextAttackTime;
    
    public int Damage => damage;
    public float Cooldown => cooldown;
    public bool CanAttack => Time.time >= nextAttackTime;
    
    public void Attack(Vector3 targetDirection)
    {
        if (!CanAttack) return;
        
        nextAttackTime = Time.time + cooldown;
        // Spawn single bullet
        SpawnBullet(targetDirection);
    }
}
```

### Shotgun

```csharp
public class Shotgun : MonoBehaviour, IWeapon
{
    [SerializeField] private int damagePerPellet = 8;
    [SerializeField] private float cooldown = 0.8f;
    [SerializeField] private int pelletCount = 5;
    [SerializeField] private float spreadAngle = 30f;
    
    private float nextAttackTime;
    
    public int Damage => damagePerPellet * pelletCount;
    public float Cooldown => cooldown;
    public bool CanAttack => Time.time >= nextAttackTime;
    
    public void Attack(Vector3 targetDirection)
    {
        if (!CanAttack) return;
        
        nextAttackTime = Time.time + cooldown;
        // Spawn multiple pellets with spread
        for (int i = 0; i < pelletCount; i++)
        {
            Vector3 spreadDir = ApplySpread(targetDirection, spreadAngle);
            SpawnPellet(spreadDir);
        }
    }
}
```

### Laser

```csharp
public class Laser : MonoBehaviour, IWeapon
{
    [SerializeField] private int damagePerSecond = 20;
    [SerializeField] private float beamRange = 10f;
    
    public int Damage => damagePerSecond;
    public float Cooldown => 0f; // Continuous
    public bool CanAttack => true; // Always can attack
    
    public void Attack(Vector3 targetDirection)
    {
        // Raycast to hit all enemies in line
        // Apply damage over time
        DrawBeam(targetDirection);
        DamageAllInPath(targetDirection);
    }
}
```

---

## Phần 3: Survivor sử dụng IWeapon

```csharp
public class Survivor : MonoBehaviour
{
    private IWeapon currentWeapon;  // Interface, không phải concrete class!
    
    public void EquipWeapon(IWeapon weapon)
    {
        currentWeapon = weapon;
    }
    
    public void Fire(Vector3 targetDirection)
    {
        if (currentWeapon != null && currentWeapon.CanAttack)
        {
            currentWeapon.Attack(targetDirection);
        }
    }
}
```

### Lợi ích

| Benefit | Giải thích |
|---------|------------|
| Survivor không biết đang dùng Pistol, Shotgun hay Laser | **Loose coupling** |
| Có thể đổi weapon runtime (pickup!) | **Flexibility** |
| Dễ thêm loại weapon mới | **Open for extension** |
| Dễ test | Mock IWeapon |

---

## 🎉 Đây là Strategy Pattern!

> *"Define a family of algorithms, encapsulate each one, and make them interchangeable."*  
> — Head First Design Patterns

Bạn vừa implement **Strategy Pattern** mà không biết!

| Strategy Pattern | Weapon Example |
|-----------------|----------------|
| Context | Survivor |
| Strategy Interface | IWeapon |
| Concrete Strategies | Pistol, Shotgun, Laser |

> [!TIP]
> Ở Phase 3, bạn sẽ học chính thức về Strategy Pattern. Bây giờ, bạn đã hiểu nền tảng!

---

## Phần 4: Gate Modifiers (Survival Shooter Bonus!)

Trong survival shooters, **Gates** modify weapon stats:

```csharp
public interface IModifiable
{
    void ApplyModifier(float multiplier);
}

public class Pistol : MonoBehaviour, IWeapon, IModifiable
{
    public void ApplyModifier(float multiplier)
    {
        damage = Mathf.RoundToInt(damage * multiplier);
    }
}
```

Khi Survivor qua Gate "+50% Damage":
```csharp
public class DamageGate : MonoBehaviour
{
    [SerializeField] private float damageMultiplier = 1.5f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IModifiable>(out var modifiable))
        {
            modifiable.ApplyModifier(damageMultiplier);
        }
    }
}
```

---

## Kiểm tra

- ✅ Survivor có thể equip Pistol, Shotgun, hoặc Laser
- ✅ Cả 3 đều attack được qua `IWeapon`
- ✅ Shotgun bắn nhiều pellets
- ✅ Laser có thể hit nhiều enemies

---

## Kiến thức rút ra

| Khái niệm | Áp dụng |
|-----------|---------|
| **Interface** | `IWeapon` định nghĩa contract |
| **Composition** | Nhiều interfaces nhỏ thay vì 1 class lớn |
| **Program to interface** | Survivor dùng `IWeapon`, không dùng `Pistol` |
| **Flexibility** | Swap weapon dễ dàng |
| **Strategy Pattern** | Đây là foundation cho Pattern đầu tiên! |

---

## So sánh tổng kết

| Inheritance | Interface + Composition |
|-------------|------------------------|
| "is-a" relationship | "can-do" relationship |
| Rigid hierarchy | Flexible combinations |
| Share implementation | Share contract |
| One parent only | Multiple interfaces |
| Couples to base class | Loose coupling |

---

## Commit

```
feat(oop): implement weapon interface and types
```

---

## Hoàn thành Module 2

Commit milestone:
```
feat(oop): variation handling with composition
```

Tiếp theo: [Module 3: Dependency](../Module3_Dependency/README.md)
