using HisaGames.CutsceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject Barnaby;
    private void OnEnable()
    {
        // Đăng ký lắng nghe
        if(AchievementManager.Instance != null) AchievementManager.Instance.OnAchievementUnlocked += HandleAchievementUnlocked;
        if(GamePlayManager.Instance != null) GamePlayManager.Instance.BlockInput = true;
    }

    private void OnDisable()
    {
        // Hủy đăng ký để tránh memory leak
        if (AchievementManager.Instance != null)
            AchievementManager.Instance.OnAchievementUnlocked -= HandleAchievementUnlocked;
        if (GamePlayManager.Instance != null)
            GamePlayManager.Instance.BlockInput = false;
    }

    private void HandleAchievementUnlocked(string id)
    {
        switch (id)
        {
            case "Silas":
                if(GamePlayManager.Instance != null) GamePlayManager.Instance.BlockInput = false;
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
            case "Day1":
                TurnOffBackground();
                break;
            case "TuoiCay1":
                TurnOnBackground();
                break;
            case "Day2":
                TurnOffBackground();
                break;
            case "TuoiCay2":
                TurnOffCutscene();
                break;
            case "next_day_2":
                TurnOnBarnaby();
                TurnOnCutscene();
                NextCutscene();
                break;
            case "interact_with_barnaby":
                TurnOffCutscene();
                break;
            case "exit_interact_with_barnaby":
                TurnOnCutscene();
                TurnOnBackground();
                NextCutscene();
                break;
            case "day_2_use_wateringcan":
                NextCutscene();
                break;
            case "Silasxphandien":
                EndDemo();
                break;
        }
    }
    private void EndDemo(){
        SceneManager.LoadScene("End");
    }
    private void TurnOnBarnaby()
    {
        if(Barnaby != null) Barnaby.SetActive(true);
        if(InteractGridManager.Instance != null) InteractGridManager.Instance.SetInteractObject(Barnaby);
    }
    private void TurnOffBackground(){
        if(GamePlayManager.Instance != null) GamePlayManager.Instance.BlockInput = false;
        CutsceneManagers.Instance.TurnOffBackground();
    }
    private void TurnOnBackground(){
        if(GamePlayManager.Instance != null) GamePlayManager.Instance.BlockInput = true;
        CutsceneManagers.Instance.TurnOnBackground();
    }
    private void TurnOnCutscene()
    {
        if(GamePlayManager.Instance != null) GamePlayManager.Instance.BlockInput = true;
        if(EcCutsceneManager.instance != null) EcCutsceneManager.instance.gameObject.SetActive(true);
    }

    private void TurnOffCutscene()
    {
        if(GamePlayManager.Instance != null) GamePlayManager.Instance.BlockInput = false;
        if(EcCutsceneManager.instance != null) EcCutsceneManager.instance.gameObject.SetActive(false);
    }

    private void TryNextWhenMoveAndDrag()
    {
        if(AchievementManager.Instance == null) return;
        if(AchievementManager.Instance.IsCompleted("first_drag") && AchievementManager.Instance.IsCompleted("first_move"))
        {
            NextCutscene();
        }
    }
    private void NextCutscene()
    {
        Debug.Log("NextCutscene");
        if(EcCutsceneManager.instance==null) return;
        EcCutsceneManager.instance.PlayNextCutsceneWithAchievement();
    }
}