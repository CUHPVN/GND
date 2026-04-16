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
        if(eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(InventoryRoot);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("Slot"))
        {
            if (eventData.pointerEnter.transform.childCount == 0)
            {   
                transform.SetParent(eventData.pointerEnter.transform);
            }else
            {
                InventoryItem droppedItem = eventData.pointerEnter.GetComponentInChildren<InventoryItem>();
                if (TryStackItem(droppedItem))
                {
                    Destroy(gameObject);
                }
                else
                {
                    transform.SetParent(parentAfterDrag);
                }
            }
        }
        else
        if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("ItemUI"))
        {
            InventoryItem droppedItem = eventData.pointerEnter.GetComponent<InventoryItem>();
            if (TryStackItem(droppedItem))
            {
                Destroy(gameObject);
            }
            else
            {
                transform.SetParent(parentAfterDrag);
            }
        }
        else
        {

            transform.SetParent(parentAfterDrag);
        }
        image.raycastTarget = true;
    }    
    private bool TryStackItem(InventoryItem droppedItem)
    {
        if(droppedItem.GetItemSO() == this.itemSO && itemSO.stackable)
            {
                int totalCount = droppedItem.GetCount() + this.count;
                if (totalCount <= itemSO.maxStack)
                {
                    droppedItem.SetCount(totalCount);
                    return true;
                }
                else
                {
                    droppedItem.SetCount(itemSO.maxStack);
                    this.SetCount(totalCount - itemSO.maxStack);
                    return false;
                }
            }
        return false;
    }
}
