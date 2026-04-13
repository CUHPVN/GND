using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    
    private void Update()
    {
    }
    public void SettingsButton()
    {
        Time.timeScale = 0;
    }
    public void ReloadButton()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
}
