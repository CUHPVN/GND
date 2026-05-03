using HisaGames.CutsceneManager;
using UnityEngine;
public class TutorialManager : MonoBehaviour
{
    private void OnEnable()
    {
        // Đăng ký lắng nghe
        AchievementManager.Instance.OnAchievementUnlocked += HandleAchievementUnlocked;
        GamePlayManager.Instance.BlockInput = true;
    }

    private void OnDisable()
    {
        // Hủy đăng ký để tránh memory leak
        if (AchievementManager.Instance != null)
            AchievementManager.Instance.OnAchievementUnlocked -= HandleAchievementUnlocked;
    }

    private void HandleAchievementUnlocked(string id)
    {
        switch (id)
        {
            case "Silas":
                GamePlayManager.Instance.BlockInput = false;
                break;
            case "stand_on_farmland":
                NextCutscene();
                break;
            case "first_move":
                TryNextWhenMoveAndDrag();
                break;
            case "first_drag":
                TryNextWhenMoveAndDrag();
                break;
            case "equip_hoe":
                NextCutscene();
                break;
        }
    }
    private void TryNextWhenMoveAndDrag()
    {
        if(AchievementManager.Instance.IsCompleted("first_drag") && AchievementManager.Instance.IsCompleted("first_move"))
        {
            NextCutscene();
        }
    }
    private void NextCutscene()
    {
        Debug.Log("NextCutscene");
        EcCutsceneManager.instance.PlayNextCutsceneWithAchievement();
    }
}