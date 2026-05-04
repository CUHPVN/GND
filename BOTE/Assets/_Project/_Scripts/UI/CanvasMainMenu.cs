
public class CanvasMainMenu : UICanvas
{
     
    public void PlayButton()
    {
        Close(0);
        if(UIManager.Instance != null) UIManager.Instance.OpenUI<CanvasGamePlay>();
    }
    public void SettingsButton()
    {
        Close(0);
    }
}
