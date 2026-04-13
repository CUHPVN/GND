
public class CanvasMainMenu : UICanvas
{
     
    public void PlayButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }
    public void SettingsButton()
    {
        Close(0);
    }
}
