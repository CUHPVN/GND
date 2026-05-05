using UnityEngine;
using HisaGames.Cutscene;

public class CutsceneManagers : Singleton<CutsceneManagers>
{
    public EcCutscene cutscene;
    public GameObject background;
    public GameObject intro;

    private bool isOn = false;

    public void Start()
    {
        Activate();
    }

    public void Activate()
    {
        if (isOn) return;

        isOn = true;

        cutscene.gameObject.SetActive(true);
        background.SetActive(true);
        intro.SetActive(true);
        cutscene.StartCutscene();
    }
    public void TurnOffBackground()
    {
        if(background != null) background.SetActive(false);
    }
    public void TurnOnBackground()
    {
        if(background != null) background.SetActive(true);
    }
    private void Update()
    {
        // if(Input.GetKeyDown(KeyCode.A))
        // {
        //     Activate();
        // }
    }
}