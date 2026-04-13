using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [Header("UI")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text countText;
    private ItemSO itemSO;
    private int count=1;
    
    private Transform InventoryRoot;
    private Transform parentAfterDrag;

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
    public void InitializeItem(ItemSO itemSO, InventorySlot slot, int count = 1)
    {
        this.itemSO = itemSO;
        this.count = count;
        image.sprite = itemSO.image;
        InventoryRoot = slot.GetRoot();
        RefreshCountText();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(InventoryRoot);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("Slot"))
        {
            transform.SetParent(eventData.pointerEnter.transform);
        }
        else
        {
            transform.SetParent(parentAfterDrag);

        }
    }    
}
