using System;
using HisaGames.Cutscene;
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
            case "use_hoe":
                NextCutscene();
                break;
            case "fill_wateringcan":
                NextCutscene();
                break;
            case "plant_seed":
                NextCutscene();
                break;
            case "next_day_1":
                TurnOnCutscene();
                NextCutscene();
                break;
            case "open_house":
                TurnOffCutscene();
                break;
        }
    }

    private void TurnOnCutscene()
    {
        EcCutsceneManager.instance.gameObject.SetActive(true);
    }

    private void TurnOffCutscene()
    {
        EcCutsceneManager.instance.gameObject.SetActive(false);
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