# 📚 Tài Liệu Tham Khảo

> **Quy tắc vàng:** Chỉ xem khi stuck quá 2 giờ. Video là công cụ hỗ trợ, không phải nguồn chính.

---

## 💡 Những Lưu Ý Nhỏ Nhưng "Có Võ" (Small but Important)

**Đọc cẩn thận từng câu từng chữ. Thắc mắc thì ib hỏi ngay.**

| Topic | Do | Why / Don't |
|-------|----|-------------|
| **Enum** | Hãy luôn gán số cụ thể cho Enum (`Type = 1`). | **Why:** Nếu sau này chèn thêm enum vào đầu/giữa, ID của các enum sau sẽ bị trượt đi, làm sai lệch data đã lưu. |
| **#IF_EDITOR** | Dùng cẩn thận. | **Don't:** Tuyệt đối không để trong code Core Game Logic. <br>**Why:** Khó track bug. Nếu Build có vấn đề, tìm lại rất khó chịu. Unity sẽ cảnh báo nhưng dễ bị trôi. <br>**Don't:** Không bao quanh `[Serializable]` hoặc class gắn trên GameObject -> Build ra sẽ báo missing script. |
| **Cheat/Debug Tools** | Nên có trong mọi game. | **Why:** Giúp test nhanh, skip flow, thêm tiền để test shop... Rất quan trọng khi làm feature. |
| **Struct và Interface** | Dùng riêng biệt. | **Don't:** Đừng ép Struct implement Interface. <br>**Why:** Gây **Boxing** (tạo rác GC) và giảm performance nghiêm trọng. |
| **LINQ** | Dùng cho Editor tools hoặc xử lý data lúc khởi tạo. | **Don't:** Tuyệt đối đừng dùng trong Core Game (Update loop). <br>**Why:** Tạo rác (GC). Độ phức tạp thường là O(n), trong khi Dictionary là O(1). |
| **Magic Number / String** | Tách ra làm `GameConstants` (static class) hoặc đưa vào ScriptableObject. | **Don't:** Tuyệt đối không hard-code số (VD: `duration = 0.5f`) hay string tag. <br>**Why:** Cực khó maintain. Sửa 1 chỗ phải đi tìm 10 chỗ khác. Không gom nhóm được. |
| **Dấu hiệu Trừu tượng hoá quá mức** | Giữ mọi thứ đơn giản (KISS). | **Don't:** Thấy code có quá nhiều `if-else` hoặc các class con không có nhiều điểm chung thực sự logic. |
| **Data-Driven Design** | Tách dữ liệu config ra ScriptableObject (SO). | **Code:** `[field: SerializeField] public float Speed { get; private set; }` <br>**Why:** Giúp biến hiển thị trên Inspector (nhờ `[field: SerializeField]`) nhưng vẫn đảm bảo tính đóng gói (Encapsulation) trong code (`private set`). Bên ngoài chỉ được đọc, không được sửa bậy. |
| **Audio Manager** | Dùng Event-based hoặc Observer. | **Thay vì:** Gọi trực tiếp `AudioManager.Instance.PlaySound(...)` từ Gameplay. <br>**Nên:** Hệ thống Audio "lắng nghe" sự kiện từ Gameplay. Giúp tách biệt code âm thanh và logic game. |
| **Vòng đời (Life Cycle)** | Hiểu rõ Awake, Start, Update, OnEnable, OnDisable. | Quan trọng để tránh lỗi cập nhật UI khi data chưa load xong, hoặc lỗi NullReference. |
| **Coroutine/Callback Hell** | Dùng **UniTask**. | **Lưu ý:** PHẢI HIỂU RÕ luồng Coroutine trước, hiểu tại sao nó tệ (khó đọc, khó try-catch, tạo rác) rồi mới dùng UniTask để thay thế. |

---

## 🎮 Game Context & References

| Game / Topic | Mô tả | Link |
|--------------|-------|------|
| **Last Z Survival** | Gameplay mẫu để tham khảo (Shorts). | [Xem Video](https://youtube.com/shorts/YdyuAJv-S1I?si=0yIiIQdvRb8CCMJo) |
| **Last Z Survival** | Gameplay mẫu (Full Video 1). | [Xem Video](https://youtu.be/xYRFwFXNskc?si=Mldt56VJ6iodvQgE) |
| **Last Z Survival** | Gameplay mẫu (Full Video 2). | [Xem Video](https://youtube.com/shorts/izXNWqTZoDA?si=d5w1bbhXv9-47QE8) |
| **Foodie Sizzle** | Match-3 Game Reference (Cấu trúc). | [Xem Video](https://youtu.be/YIk_Yts_9I0?si=JFItOzWApf9P9K_W) |
| **Game Patterns** | **Game Programming Patterns** (Web Book). | [Website](https://gameprogrammingpatterns.com/contents.html) |

---

## 📘 Phase 1: OOP Foundation

| Topic | Video / Link | Ghi chú |
|-------|--------------|---------|
| **OOP Unity** | [OOP Unity Tutorial](https://youtube.com/playlist?list=PLUVc07JzzNRTKBoXL68I3YZTTmiMZXTmk&si=T7JAzz9VfJUHsz7y) | Cơ bản về OOP trong Unity. |
| **Inheritance** | [Composition vs Inheritance (git-amend)](https://youtu.be/mAfpfUYhpAs?si=XXd6uCim1Ot-np0Q) | Tại sao nên ưu tiên Composition. |
| **Architecture** | [Architecture, Performance and Games](https://gameprogrammingpatterns.com/architecture-performance-and-games.html) | Hiểu sự khác biệt code game vs app. |

---

## 📙 Phase 2: Design Principles

| Topic | Video / Link | Ghi chú |
|-------|--------------|---------|
| **Components** | [Component Pattern](https://gameprogrammingpatterns.com/component.html) | Cốt lõi của Unity (Composition). |
| **SOLID** | [SOLID Principles (Infallible Code)](https://www.youtube.com/watch?v=QDldZWvNK_E) | Khi nào dùng SOLID trong Unity. |
| **Composition** | [SIMPLE Tip For Better Unity Game Architecture](https://youtu.be/mAfpfUYhpAs?si=6i_flPRvVaE7XH5b) | Như tên video. huh?|

---

## 📗 Phase 3: Design Patterns

| Pattern | Video / Link | Ghi chú |
|---------|--------------|---------|
| **Factory** | [When to use Factory and Abstract Factory Programming Patterns](https://youtu.be/Z1CDJASi4SQ?si=2POEg_F8ltoK-FjB) | Khi nào dùng Factory? |
| **Factory** | [Your Factory Pattern Might Be Wrong and You Don't Even Know It](https://youtu.be/s1Pa48sxjck?si=V7F4YT8XSHtJE0c8) | Sai lầm thường gặp. |
| **Singleton** | [Singleton trong Unity (thực hiện đúng)](https://youtu.be/yhlyoQ2F-NM?si=MwMGyw2J1przqivR) | Thread-safe & Lazy load. |
| **Singleton** | [Singleton Pattern (Book)](https://gameprogrammingpatterns.com/singleton.html) | Chương sách về Singleton. |
| **Singleton** | [Reference Objects Flawlessly](https://youtu.be/xYRFwFXNskc?si=U61vX27BDp5Ec5xY) | 5 Alternatives to Singleton (tránh `FindObjectOfType`). |
| **Strategy** | [Clean Code using Strategy](https://youtu.be/QrxiD2dfdG4?si=Gmt25xybgJiw3ImL) | Ví dụ thực tế. |
| **Observer** | [Learn to Build an Advanced Event Bus](https://youtu.be/4_DTAnigmaQ?si=AY6Mvh1cCxN3iyZa) | Cách làm Event Bus xịn. |
| **Observer** | [Observer Pattern (Book)](https://gameprogrammingpatterns.com/observer.html) | Chương sách về Observer. |
| **Object Pool** | [Object Pooling (Brackeys)](https://www.youtube.com/watch?v=tdSmKaJvCoA) | Visual dễ hiểu. |
| **Object Pool** | [Object Pool Pattern (Book)](https://gameprogrammingpatterns.com/object-pool.html) | Chương sách về Pool. |
| **State Machine** | [How to Code a Simple State Machine (Infallible Code)](https://www.youtube.com/watch?v=G1bd75R10m4) | Simple State Machine trong Unity. |
| **State Machine** | [Finite-State Machine (FSM) in Unity (Infallible Code)](https://youtu.be/5PTd0WdKB-4?si=tyACR21z3HxVstbV) | Simple State Machine trong Unity. |
| **State** | [State Pattern (Book)](https://gameprogrammingpatterns.com/state.html) | Chương sách về State. |
| **Command** | [Command Pattern (Book)](https://gameprogrammingpatterns.com/command.html) | Undo/Redo & Input. |

---

## 📕 Phase 4: Architecture

| Topic | Video / Link | Ghi chú |
|-------|--------------|---------|
| **MVC/MVP** | [Improve Your Unity Code with MVC/MVP Architectural Patterns](https://youtu.be/v2c589RaiwY?si=THNsSN8GeEG-Hm0l) | Cơ bản về tách MVC/MVP|
| **Structure** | [Unity Architecture for Noobs - Game Structure](https://youtu.be/tE1qH8OxO2Y?si=H1_nnS6BXcklun91) | Tổ chức project gọn gàng. |
| **Testing** | [Unit Testing for MVP/MVC](https://youtu.be/Wh27sG0DXzU?si=_cBxx1FNBfSwZsVg) | Test logic khi đã tách view. |
| **Game Loop** | [Game Loop Pattern](https://gameprogrammingpatterns.com/game-loop.html) | Tim mạch của game. |
| **Unity Docs** | [Organizing Your Project](https://unity.com/how-to/organizing-your-project) | Best practices từ Unity. |

---

## 🎥 Các Channel Hay (Nên Sub)

| Channel | Nội dung | Link |
|---------|----------|------|
| **git-amend** | Advanced Unity, Architecture. Rất clean code. | [Link](https://www.youtube.com/playlist?list=PLnJJ5frTPwRMCCDVE_wFIt3WIj163Q81V) |
| **Infallible Code** | Design Patterns & Code Review. | [Link](https://www.youtube.com/@InfallibleCode) |
| **Jason Weimann** | Unity Architecture, Job System. | [Link](https://www.youtube.com/@Unity3dCollege) |
| **Code Monkey** | Unity Patterns, Tutorials dễ hiểu. | [Link](https://www.youtube.com/@CodeMonkeyUnity) |

---

## 🚀 Hướng Đi Tiếp Theo (Future Path)

Sau khi nắm vững OOP Foundation và Architecture, hãy tìm hiểu các công cụ này để nâng cao level:

1.  **DoTween**: Thư viện Animation bằng code. *Must-have* cho mọi dự án game.
2.  **UniTask**: Thay thế Coroutine. Code bất đồng bộ (Async/Await) clean hơn, performance tốt hơn.
3.  **Reflex / VContainer / Zenject**: Dependency Injection (DI) Frameworks. Giúp quản lý dependencies chuyên nghiệp hơn (Thay thế Singleton/Service Locator).
4.  **R3 (Reactive Extensions)**: Lập trình phản ứng (Reactive Programming). Cực mạnh để xử lý luồng sự kiện phức tạp.

---

## ⚠️ Lưu Ý Quan Trọng

1.  **Video là công cụ hỗ trợ, không phải nguồn chính**.
2.  **Luôn quay lại code sau khi xem**.
3.  **Đánh dấu checkbox khi hoàn thành** để track progress.
4.  **Ghi note những gì rút ra được** — không chỉ xem rồi quên.
