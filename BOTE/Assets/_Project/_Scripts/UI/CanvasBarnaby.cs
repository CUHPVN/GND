using System;
using System.Collections.Generic;
using CodeMonkey.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasBarnaby : UICanvas
{
    [Header("Barnaby")]

    [SerializeField] private Button closeButton;

    [SerializeField] private ShopItemSlot itemSlotPrefab;
    [SerializeField] private GameObject inventoryContentRoot;
    [SerializeField] private GameObject shopContentRoot;
    [SerializeField] private GameObject selectedObject;
    [SerializeField] private Button interactButton;

    [SerializeField] private TMP_Text coinText;

    [SerializeField] private ShopItemSlot selectedItem;
    [SerializeField] private List<ShopItemSlot> inventorySlots = new();
    [SerializeField] private List<ShopItemSlot> shopSlots = new();
    [SerializeField] private ItemSO[] sellItems;
    [SerializeField] private List<ItemData> shopItem = new();

    public override void Open()
    {
        base.Open();
        GamePlayManager.Instance.BlockInput = true;
        LoadInventory();
        LoadShop();
        HideSelected();
    }
    public void LoadInventory()
    {
        foreach (ShopItemSlot button in inventorySlots)
        {
            Destroy(button.gameObject);
        }
        inventorySlots.Clear();
        foreach (ItemData itemData in CircularManager.Instance.ItemDatas)
        {
            if(itemData.itemSO == null) continue;
            if(!itemData.itemSO.sellable) continue;
            ShopItemSlot itemSlot = Instantiate(itemSlotPrefab, inventoryContentRoot.transform);
            itemSlot.GetComponent<Button>().onClick.AddListener(() => SetSelectedItem(itemSlot));
            itemSlot.SetItemData(itemData,true);
            inventorySlots.Add(itemSlot);
        }
    }
    public void LoadShop()
    {
        foreach (ShopItemSlot button in shopSlots)
        {
            Destroy(button.gameObject);
        }
        shopSlots.Clear();
        foreach (ItemData itemData in shopItem)
        {
            if(itemData.count <= 0) continue;
            ShopItemSlot itemSlot = Instantiate(itemSlotPrefab, shopContentRoot.transform);
            itemSlot.GetComponent<Button>().onClick.AddListener(() => SetSelectedItem(itemSlot));
            itemSlot.SetItemData(itemData,false);
            shopSlots.Add(itemSlot);
        }
    }
    public void SetSelectedItem(ShopItemSlot shopItemSlot)
    {
        this.selectedItem = shopItemSlot;
        if(selectedItem != null)
        {
            ShowSelected();
        }
    }

    private void ShowSelected()
    {
        if(selectedItem == null) return;
        selectedObject.transform.GetChild(0).GetComponent<Image>().sprite = selectedItem.itemData.itemSO.image;
        selectedObject.GetComponentInChildren<TMP_Text>().text = selectedItem.itemData.itemSO.itemType.ToString();
        selectedObject.SetActive(true);
        interactButton.gameObject.SetActive(true);
        if (selectedItem.isSell)
        {
            interactButton.GetComponentInChildren<TMP_Text>().text = "Sell for " + selectedItem.itemData.itemSO.price;
        } 
        if (!selectedItem.isSell)
        {
            interactButton.GetComponentInChildren<TMP_Text>().text = "Buy for " + selectedItem.itemData.itemSO.price;  
        }
    }
    private void HideSelected()
    {
        selectedObject.SetActive(false);
        interactButton.gameObject.SetActive(false);
    }

    public void ResetShop()
    {
        foreach (ItemSO itemSO in sellItems)
        {
            shopItem.Add(new ItemData
            {
                itemSO = itemSO,
                count = UnityEngine.Random.Range(1, 5)
            });
        }
    }
    public bool SellItem()
    {
        if (selectedItem == null) return false;
        if(selectedItem.itemData.itemSO == null) return false;
        int price = selectedItem.itemData.itemSO.price;
        if (CircularManager.Instance.SellItem(selectedItem.itemData))
        {
            GamePlayManager.Instance.AddMoney(price);
        }
        LoadInventory();
        return true;
    }
    public bool BuyItem()
    {
        if (selectedItem == null) return false;
        if (selectedItem.itemData.itemSO == null) return false;
        int price = selectedItem.itemData.itemSO.price;
        if (GamePlayManager.Instance.Money < price) return false;
        GamePlayManager.Instance.AddMoney(-price);
        CircularManager.Instance.AddItem(selectedItem.itemData.itemSO,1);
        selectedItem.itemData.count--;
        LoadShop();
        return true;
    }
    public void TryInteract()
    {
        if (selectedItem == null) return;
        if (selectedItem.isSell)
        {
            if(SellItem()) 
            HideSelected();
        }else
        if (!selectedItem.isSell)
        {
            if(BuyItem())
            HideSelected();
                else
                {
                    //No money
                }
        }
    }
    public void Awake()
    {
        ResetShop();
    }
    public void Start()
    {
        InitEvent();
    }
    public void SetCoinText(int money)
    {
        coinText.text = money.ToString();
    }
    public void InitEvent()
    {
        GamePlayManager.Instance.OnMoneyChange+=SetCoinText;
        interactButton.onClick.AddListener(() => TryInteract());
        closeButton.onClick.AddListener(() => CloseButton());
    }
    public void OnDestroy()
    {
        GamePlayManager.Instance.OnMoneyChange-=SetCoinText;
        interactButton.onClick.RemoveListener(TryInteract);
        closeButton.onClick.RemoveListener(CloseButton);
    }
    public override void CloseDirectly()
    {
        GamePlayManager.Instance.BlockInput = false;
        base.CloseDirectly();
    }

    public void CloseButton()
    {
        Close(0);
    }

}

