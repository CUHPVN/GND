using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private Image currentImage;
    [SerializeField] private TMP_Text currentText;
    private void OnEnable()
    {
        CircularManager.Instance.OnItemChange += OnItemChange;
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


    public void SettingsButton()
    {
        Time.timeScale = 0;
    }
    public void ReloadButton()
    {
    }
}
