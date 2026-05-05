using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private Image currentImage;
    [SerializeField] private TMP_Text currentText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text dayText;
    private void OnEnable()
    {
        if(CircularManager.Instance != null) CircularManager.Instance.OnItemChange += OnItemChange;
        if(GamePlayManager.Instance != null) GamePlayManager.Instance.OnDayChange += OnDayChange;
        if(GamePlayManager.Instance != null) GamePlayManager.Instance.OnMoneyChange += OnMoneyChange;
    }
    private void OnDisable()
    {
        //if(CircularManager.Instance!=null) CircularManager.Instance.OnItemChange -= OnItemChange;
        //if(GamePlayManager.Instance!=null) GamePlayManager.Instance.OnDayChange -= OnDayChange;
        //if(GamePlayManager.Instance!=null) GamePlayManager.Instance.OnMoneyChange -= OnMoneyChange;
    }

    private void OnItemChange(ItemData data)
    {
        if(data.itemSO != null)
        {
            currentImage.color = Color.white;
            currentImage.sprite = data.itemSO.image;
            if (data.itemSO.stackable)
            {
                currentText.text = data.count.ToString();
            }
            else
            {
                currentText.text = "";
            }
        }
        else
        {
            currentImage.color = new Color(1,1,1,0);
            currentImage.sprite = null;
            currentText.text = "";
        }
    }
    private void OnDayChange(int day)
    {
        dayText.text = "Day " + day.ToString();
    }
    private void OnMoneyChange(int money)
    {
        moneyText.text = money.ToString();
    }


    public void SettingsButton()
    {
        Time.timeScale = 0;
    }
    public void ReloadButton()
    {
    }
}
