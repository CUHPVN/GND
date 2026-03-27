# Principle 3: Program to Abstraction

> *"Program to an interface, not an implementation."*  
> — Head First Design Patterns

Depend on abstractions, not concrete implementations.

---

## Recall Phase 1 🔙

Bạn đã thấy principle này:
- **Task_WeaponTypes**: Survivor dùng `IWeapon`, không dùng `Pistol` trực tiếp
- **Task_Coupling**: Bullet dùng `IDamageable`, không dùng `Zombie`

---

## Feature: Save System

Game cần save data với nhiều cách:
- PlayerPrefs (đơn giản)
- JSON file (local)
- Cloud (online)

---

## Phần 1: Cách sai — Depend on Implementation

```csharp
public class GameManager : MonoBehaviour
{
    public void SaveGame()
    {
        PlayerPrefs.SetInt("level", currentLevel);
        PlayerPrefs.SetInt("score", currentScore);
        PlayerPrefs.SetFloat("health", playerHealth);
        PlayerPrefs.Save();
    }
    
    public void LoadGame()
    {
        currentLevel = PlayerPrefs.GetInt("level");
        currentScore = PlayerPrefs.GetInt("score");
        playerHealth = PlayerPrefs.GetFloat("health");
    }
}
```

### Vấn đề

| Issue | Problem |
|-------|---------|
| GameManager phụ thuộc trực tiếp vào `PlayerPrefs` | Tight coupling |
| Đổi sang JSON | Sửa GameManager |
| Testing | PlayerPrefs khó mock |
| Multiple save methods | Không thể có cùng lúc |

---

## Phần 2: Giải pháp — Abstract the Storage

### Interface

```csharp
public interface ISaveService
{
    void Save(string key, object data);
    T Load<T>(string key);
    bool HasKey(string key);
    void Delete(string key);
}
```

### PlayerPrefs Implementation

```csharp
public class PlayerPrefsSaveService : ISaveService
{
    public void Save(string key, object data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }
    
    public T Load<T>(string key)
    {
        string json = PlayerPrefs.GetString(key);
        return JsonUtility.FromJson<T>(json);
    }
    
    public bool HasKey(string key) => PlayerPrefs.HasKey(key);
    
    public void Delete(string key) => PlayerPrefs.DeleteKey(key);
}
```

### JSON File Implementation

```csharp
public class JsonFileSaveService : ISaveService
{
    private string savePath;
    
    public JsonFileSaveService(string path)
    {
        savePath = path;
    }
    
    public void Save(string key, object data)
    {
        string json = JsonUtility.ToJson(data);
        string filePath = Path.Combine(savePath, key + ".json");
        File.WriteAllText(filePath, json);
    }
    
    public T Load<T>(string key)
    {
        string filePath = Path.Combine(savePath, key + ".json");
        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<T>(json);
    }
    
    public bool HasKey(string key)
    {
        string filePath = Path.Combine(savePath, key + ".json");
        return File.Exists(filePath);
    }
    
    public void Delete(string key)
    {
        string filePath = Path.Combine(savePath, key + ".json");
        File.Delete(filePath);
    }
}
```

### GameManager refactored

```csharp
public class GameManager : MonoBehaviour
{
    private ISaveService saveService;
    
    public void Initialize(ISaveService saveService)
    {
        this.saveService = saveService;
    }
    
    public void SaveGame()
    {
        var saveData = new SaveData
        {
            level = currentLevel,
            score = currentScore,
            health = playerHealth
        };
        
        saveService.Save("game_save", saveData);
    }
    
    public void LoadGame()
    {
        var saveData = saveService.Load<SaveData>("game_save");
        currentLevel = saveData.level;
        currentScore = saveData.score;
        playerHealth = saveData.health;
    }
}

[Serializable]
public class SaveData
{
    public int level;
    public int score;
    public float health;
}
```

---

## Dependency Inversion Principle (DIP)

> *"High-level modules should not depend on low-level modules. Both should depend on abstractions."*

### Trước (Violation)

```
GameManager ──depends on──▶ PlayerPrefs
(high-level)                (low-level)
```

### Sau (DIP Applied)

```
GameManager ──depends on──▶ ISaveService ◀──implements── PlayerPrefsSaveService
(high-level)                (abstraction)                (low-level)
```

**Cả hai** đều depend on abstraction!

---

## Phần 3: Lợi ích

### Swap implementation dễ dàng

```csharp
// Development
gameManager.Initialize(new PlayerPrefsSaveService());

// Production
gameManager.Initialize(new JsonFileSaveService(Application.persistentDataPath));

// Cloud
gameManager.Initialize(new CloudSaveService(apiKey));
```

### Test dễ dàng

```csharp
public class MockSaveService : ISaveService
{
    private Dictionary<string, object> data = new Dictionary<string, object>();
    
    public void Save(string key, object data) => this.data[key] = data;
    public T Load<T>(string key) => (T)data[key];
    // ...
}

// Unit test
var mockSave = new MockSaveService();
var gameManager = new GameManager();
gameManager.Initialize(mockSave);
// Test without touching real storage
```

---

## 🎉 Factory Pattern Preview

> *"Define an interface for creating an object, but let subclasses decide which class to instantiate."*

Khi cần **tạo** objects mà không biết concrete type:

```csharp
public interface ISaveServiceFactory
{
    ISaveService Create();
}

public class ProductionSaveServiceFactory : ISaveServiceFactory
{
    public ISaveService Create() => new JsonFileSaveService(Application.persistentDataPath);
}
```

> [!TIP]
> Ở Phase 3, bạn sẽ học **Factory Pattern** chính thức!

---

## Phần 4: Thực hành

### Bước 1: Tạo `ISaveService` interface

### Bước 2: Tạo `PlayerPrefsSaveService`

### Bước 3: Refactor code sử dụng save
- Inject `ISaveService` thay vì gọi `PlayerPrefs` trực tiếp

### Bước 4: Tạo `JsonFileSaveService`
- Swap và test — không sửa code khác!

---

## Kiểm tra

- ✅ Không có `PlayerPrefs` trong business logic
- ✅ Có thể swap save service mà không sửa GameManager
- ✅ Có thể tạo MockSaveService để test

---

## Kiến thức rút ra

| Khái niệm | Áp dụng |
|-----------|---------|
| **Abstraction** | `ISaveService` |
| **Dependency Inversion** | GameManager → ISaveService (không phải PlayerPrefs) |
| **Swap-ability** | Đổi implementation không ảnh hưởng caller |
| **Testability** | Mock dễ dàng |
| **Factory preview** | Create objects without knowing concrete type |

---

## Áp dụng trong game

Những gì nên abstract:
- Storage (save/load)
- Network requests
- Audio playback
- Analytics
- Platform-specific features

---

## Commit

```
feat(principles): implement program to abstraction
```

Tiếp theo: [Principle 4: Low Coupling / High Cohesion](./Principle4_LowCouplingHighCohesion.md)
