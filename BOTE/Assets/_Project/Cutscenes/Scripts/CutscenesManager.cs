using UnityEngine;
using HisaGames.Cutscene;

public class CutsceneManagers : MonoBehaviour
{
    public EcCutscene cutscene;
    public GameObject background;

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
        cutscene.StartCutscene();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Activate();
        }
    }
}