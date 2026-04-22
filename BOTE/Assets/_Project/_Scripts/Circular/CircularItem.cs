using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CircularItem : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text countText;
    private ItemSO itemSO;
    private int count=1;
    

    public void SetItemSO(ItemSO itemSO)
    {
        this.itemSO = itemSO;
    }
    public ItemSO GetItemSO()
    {
        return itemSO;
    }
    public int GetCount()
    {
        return count;
    }
    public void SetCount(int count)
    {
        this.count = count;
        RefreshCountText();
    }
    public void RefreshCountText()
    {
        if (count > 1)
        {
            countText.text = count.ToString();
            countText.gameObject.SetActive(true);
        }
        else
        {
            countText.gameObject.SetActive(false);
        }
    }
    public void InitializeItem(ItemSO itemSO, int count = 1)
    {
        if (itemSO != null)
        {
            image.color = Color.white;
        }else image.color = new Color(1,1,1,0);
        this.itemSO = itemSO;
        this.count = count;
        image.sprite = itemSO.image;
        RefreshCountText();
    }
    
}
