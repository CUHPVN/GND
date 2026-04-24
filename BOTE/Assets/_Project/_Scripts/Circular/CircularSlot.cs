using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CircularSlot : MonoBehaviour
{
    [Header("InventoryRoot")]
    [SerializeField] private Image slotImage;

    [SerializeField] private CircularItem item;

    public ItemData itemData;
    public Transform itemHandle;

    public void Selected()
    {
        slotImage.color = new Color(1f, 0.8333334f, 0.5f,0.5019608f);
    }
    public void Unselected()
    {
        slotImage.color = new Color(0.5f, 0.5f, 0.5f,0.5019608f);
    }
    public void SetItem(ItemData itemData)
    {
        this.itemData = itemData;
        item.InitializeItem(itemData.itemSO, itemData.count);
    }
    public ItemData GetItem()
    {
        return itemData;
    }
}
