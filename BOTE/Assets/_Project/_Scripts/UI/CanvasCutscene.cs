using UnityEngine;
using HisaGames.CutsceneManager;
using UnityEngine.UIElements;

public class CanvasCutscene : UICanvas
{
    public GameObject background;   
    public override void Open()
    {
        base.Open();
        TurnOnCutscene();
    }
    public override void CloseDirectly()
    {
        TurnOffCutscene();
        base.CloseDirectly();
    }
    public void TurnOnCutscene()
    {
        background.SetActive(true);
        EcCutsceneManager.instance.gameObject.SetActive(true);
    }

    public void TurnOffCutscene()
    {
        background.SetActive(false);
        EcCutsceneManager.instance.gameObject.SetActive(false);
    }
}
