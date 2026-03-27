# Task: Zombie Types

Tạo nhiều loại Zombie: Basic, Fast và Tank.

---

## Mục tiêu

- Học **khi nào** Inheritance là lựa chọn đúng
- Hiểu **abstract class** và **polymorphism**

---

## Yêu cầu

Tạo 3 loại zombie (phổ biến trong survival games):
- **BasicZombie**: Tốc độ trung bình, HP trung bình — số đông trong horde
- **FastZombie**: Nhanh, HP thấp — nguy hiểm khi bị bao vây
- **TankZombie**: Chậm, HP cao, damage cao — boss mini

---

## Phần 1: Cách sai (Hãy thử trước!)

### Thử dùng if/else

```csharp
public class Zombie : MonoBehaviour
{
    public enum ZombieType { Basic, Fast, Tank }
    
    [SerializeField] private ZombieType type;
    
    private void Attack()
    {
        if (type == ZombieType.Basic)
        {
            // Attack bình thường
        }
        else if (type == ZombieType.Fast)
        {
            // Attack nhanh, nhiều hit
        }
        else if (type == ZombieType.Tank)
        {
            // Attack mạnh, knockback
        }
    }
}
```

### Vấn đề

| Issue | Tại sao xấu? |
|-------|-------------|
| Thêm loại mới | Sửa nhiều if/else |
| Class phình to | Chứa code của tất cả types |
| Khó test | Không test riêng từng loại được |
| Violates OCP | Phải modify class để extend |

> [!NOTE]
> **OCP = Open/Closed Principle** — Class nên mở cho extension, đóng cho modification.  
> Bạn sẽ học chi tiết ở Phase 2!

---

## Phần 2: Giải pháp — Inheritance

### Khi nào dùng Inheritance?

✅ **Dùng khi:**
- Có quan hệ **IS-A** rõ ràng: FastZombie **IS-A** Zombie
- Các subclasses **share significant behavior** (di chuyển về phía target, chết khi HP = 0)
- Hierarchy **không quá sâu** (1-2 levels)

❌ **Không dùng khi:**
- Chỉ muốn tái sử dụng code
- Objects cần **mix behaviors** (như vịt vừa bay vừa kêu)
- Hierarchy phức tạp (3+ levels)

### Base class

```csharp
public abstract class ZombieBase : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 50;
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected int contactDamage = 10;
    
    protected int currentHealth;
    protected Transform target;
    
    public virtual void Initialize(Transform target)
    {
        this.target = target;
        currentHealth = maxHealth;
    }
    
    protected virtual void Update()
    {
        MoveTowardsTarget();
    }
    
    protected void MoveTowardsTarget()
    {
        if (target == null) return;
        transform.position = Vector3.MoveTowards(
            transform.position, 
            target.position, 
            moveSpeed * Time.deltaTime
        );
    }
    
    public abstract void Attack();  // Subclasses MUST implement
    
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) Die();
    }
    
    protected virtual void Die()
    {
        // Drop XP, play death animation
        Destroy(gameObject);
    }
}
```

### BasicZombie

```csharp
public class BasicZombie : ZombieBase
{
    public override void Attack()
    {
        // Standard bite attack
        Debug.Log("Basic zombie attacks!");
    }
}
```

### FastZombie

```csharp
public class FastZombie : ZombieBase
{
    [SerializeField] private float speedMultiplier = 2f;
    
    protected override void Update()
    {
        base.Update();
        // Có thể thêm dash ability
    }
    
    public override void Attack()
    {
        // Quick swipe attacks
        Debug.Log("Fast zombie rapid attacks!");
    }
}
```

### TankZombie

```csharp
public class TankZombie : ZombieBase
{
    [SerializeField] private float knockbackForce = 5f;
    
    public override void Attack()
    {
        // Heavy slam with knockback
        Debug.Log("Tank zombie slams with knockback!");
    }
    
    protected override void Die()
    {
        // Explosion on death? Drop better loot?
        base.Die();
    }
}
```

---

## Phần 3: Refactor

1. Xóa class `Zombie` cũ (hoặc rename thành `ZombieBase`)
2. Tạo `BasicZombie`, `FastZombie`, `TankZombie`
3. Test cả 3 loại trong scene

---

## Kiểm tra

- ✅ BasicZombie di chuyển và attack bình thường
- ✅ FastZombie di chuyển nhanh hơn
- ✅ TankZombie có HP cao hơn và attack mạnh hơn
- ✅ Tất cả đều nhận damage và chết đúng cách

---

## Liskov Substitution Preview

> *"Subtypes must be substitutable for their base types."*

```csharp
// Bất kỳ ZombieBase nào cũng hoạt động
void ProcessZombie(ZombieBase zombie)
{
    zombie.Attack();  // Works for Basic, Fast, Tank, or any future type!
}

// SpawnManager có thể spawn bất kỳ loại zombie
void SpawnZombie(ZombieBase prefab)
{
    var zombie = Instantiate(prefab);
    zombie.Initialize(survivor.transform);
}
```

Đây là **Liskov Substitution Principle (LSP)** — sẽ học chi tiết ở Phase 2.

---

## Kiến thức rút ra

| Khái niệm | Áp dụng |
|-----------|---------|
| **Abstract class** | `ZombieBase` định nghĩa template |
| **Inheritance** | Các loại zombie kế thừa behavior chung |
| **Polymorphism** | `Attack()` làm khác nhau tùy loại |
| **Virtual/Override** | Customize behavior khi cần |
| **When to inherit** | IS-A, shared behavior, shallow hierarchy |

---

## Commit

```
feat(oop): implement zombie inheritance
```

Tiếp theo: [Task: Weapon Types](./Task_WeaponTypes.md)
