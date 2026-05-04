using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    public Image image;
    public TMP_Text countText;
    public ItemData itemData;

    public bool isSell;
    public void OnClick()
    {
        
    }
    public void SetItemData(ItemData itemData,bool isSell = false)
    {
        this.isSell = isSell;
        this.itemData = itemData;
        image.sprite = itemData.itemSO.image;
        countText.text = itemData.count.ToString();
    }
}
