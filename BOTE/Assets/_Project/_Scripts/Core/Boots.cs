using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boots : MonoBehaviour
{
    [SerializeField] private Button playButton;
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Game");
    }
}
