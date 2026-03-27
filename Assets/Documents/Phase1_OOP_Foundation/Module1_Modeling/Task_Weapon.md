# Task: Weapon

Gắn vũ khí auto-fire vào Survivor.

---

## Mục tiêu

Hiểu **Composition** — "HAS-A" relationship.

> *"Favor composition over inheritance."* — Head First Design Patterns

> [!NOTE]
> **Game Context**: Đây là **vũ khí tự động bắn (auto-fire)** - trong Game ref chúng ta hướng đến, vũ khi sẽ tự bắn đạn đi thẳng/toả ra. Player chỉ cần focus di chuyển!

---

## Yêu cầu

Tạo script `Weapon.cs` với:

### State
- Damage
- Fire rate (thời gian giữa các phát bắn)
- Bullet count (số đạn bắn mỗi lần)

### Behavior
- Tự động bắn (auto-fire)
- Spawn bullet (Hiện tại chỉ nên tối đa 10 bullets - nhiểu sẽ lag)

---

## Hướng dẫn

### Bước 1: Tạo Weapon

```csharp
public class Weapon : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private int bulletCount = 1;
    
    private float nextFireTime;
    
    // TODO: Implement auto-fire logic
}
```

### Bước 2: Gắn vào Survivor

Mở `Survivor.cs`, thêm:
- Reference đến Weapon
- Weapon tự động Fire trong Update

### Bước 3: Auto-fire logic

```csharp
private void Update()
{
    if (Time.time >= nextFireTime)
    {
        Fire();
        nextFireTime = Time.time + 1f / fireRate;
    }
}

private void Fire()
{
    // Tìm enemy gần nhất
    // Spawn bullet về phía enemy
}
```

---

## Composition trong thực tế

```
Player ──has──▶ Survivor ──has──▶ Weapon
```

| Quan hệ | Giải thích |
|---------|------------|
| Player HAS Survivor | Player điều khiển Survivor |
| Survivor HAS Weapon | Survivor sở hữu Weapon |

### Tại sao không kế thừa?

❌ `Survivor : Weapon`? → Nhân vật không phải là vũ khí!  
❌ `Weapon : Survivor`? → Vũ khí không phải là nhân vật!  
✅ `Survivor` **có** `Weapon` → Hợp lý!

---

## Auto-fire ví dụ đơn giản

Trong survival shooters, weapon thường:
1. **Tự động tìm target** (enemy gần nhất / bắn thẳng như Game Ref)
2. **Tự động bắn** theo fire rate
3. **Không cần player input** để attack

```csharp
public class Weapon : MonoBehaviour
{
    private void Update()
    {
        if (CanFire())
        {
            Zombie target = FindClosestZombie();
            if (target != null)
            {
                FireAt(target);
            }
        }
    }
}
```

> [!TIP]
> Đây là cách phổ biến trong mobile survival games — giảm complexity cho player, tăng engagement!

---

## Kiểm tra

Code của bạn nên:

- ✅ Weapon tự quản lý cooldown (fireRate)
- ✅ Weapon tự động bắn khi có enemy trong range
- ✅ Survivor có reference đến Weapon

---

## Kiến thức rút ra

| Khái niệm | Áp dụng |
|-----------|---------|
| **Composition** | Survivor "có" Weapon |
| Auto-fire pattern | Weapon hoạt động độc lập |
| State management | Weapon tự quản lý cooldown, damage |
| **HAS-A > IS-A** | Composition thay vì inheritance |

---

## Suy nghĩ thêm

> Nếu sau này cần Pistol, Shotgun, Laser (weapon khác nhau)?  
> Làm sao để Survivor có thể dùng nhiều loại weapon?

Gợi ý: Sẽ giải quyết ở Module 2 với **Interfaces**!

---

## Commit

```
feat(oop): implement auto-fire weapon
```

Tiếp theo: [Task: Zombie](./Task_Zombie.md)
