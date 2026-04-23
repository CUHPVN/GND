using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class CircularManager : Singleton<CircularManager>
{
    [SerializeField] private ItemSO[] itemSOArray;
    [SerializeField] private CircularSlot[] inventorySlots;
    [SerializeField] private List<ItemData> itemDatas = new();

    private CanvasCircular canvas;
    public ItemData currentItem;
    [SerializeField] private ItemSO fullWateringCan;
    [SerializeField] private ItemSO emptyWateringCan;

    public event Action<ItemData> OnItemChange;

    void Awake()
    {
        itemSOArray = Resources.LoadAll<ItemSO>("SO/Items");
    }
    void OnEnable()
    {
        OnItemChange += SetUI;
    }
    private void SetUI(ItemData item)
    {
        canvas.SetItem(itemDatas.ToArray());
    }
    void Start()
    {
        canvas = UIManager.Instance.OpenUI<CanvasCircular>();
        canvas.Close(0);
    }
    void Update()
    {
        if (!GamePlayManager.Instance.BlockInput && Input.GetKeyDown(KeyCode.E))
        {
            canvas.SetItem(itemDatas.ToArray());
            canvas.Open();
        }
        if (!GamePlayManager.Instance.BlockInput && Input.GetKey(KeyCode.E))
        {
            canvas.Highlight(canvas.FindSlotMouseOver());
        }
        if (!GamePlayManager.Instance.BlockInput && Input.GetKeyUp(KeyCode.E))
        {
            SetCurrentItem(canvas.GetCurrentItem());
            canvas.Close(0);
        }
    }
    public ItemData UseItem()
    {
        ItemData itemData = currentItem;
        if (currentItem.itemSO != null)
        {
            if (currentItem.itemSO.stackable)
            {
                currentItem.count--;
                
                if (currentItem.count <= 0)
                {
                    if(currentItem.itemSO == fullWateringCan)
                    {
                        currentItem.itemSO = emptyWateringCan;
                        currentItem.count = 1;
                    }
                    else
                    {
                        currentItem.itemSO = null;
                    }
                }

            }
            else
            {
                
            }
            OnItemChange?.Invoke(currentItem);
        }
        return itemData;
    }
    public ItemData UseItem(ItemData item)
    {
        if (item.itemSO != null)
        {
            if (item.itemSO.stackable)
            {
                item.count--;
                
                if (currentItem.count <= 0)
                {
                    if(currentItem.itemSO == fullWateringCan)
                    {
                        currentItem.itemSO = emptyWateringCan;
                        currentItem.count = 1;
                    }
                    else
                    {
                        currentItem.itemSO = null;
                    }
                }

            }
            else
            {
                
            }
            OnItemChange?.Invoke(currentItem);
        }
        return item;
    }
    public void ChangeItem()
    {
        OnItemChange?.Invoke(currentItem);
    }
    private void SetCurrentItem(ItemData itemData)
    {
        currentItem = itemData;
        OnItemChange?.Invoke(itemData);
    }
    public ItemSO GetRandomItem()
    {
        int randomIndex = UnityEngine.Random.Range(0, itemSOArray.Length);
        return itemSOArray[randomIndex];
    }

    public ItemData FindItem(ItemSO itemSO)
    {
        for (int i = 0; i < itemDatas.Count; i++)
        {
            if (itemDatas[i].itemSO == itemSO)
            {
                return itemDatas[i];
            }
        }
        return null;
    }
    public void AddItem(ItemSO item,int count)
    {
        foreach(ItemData itemData in itemDatas)
        {
            if (itemData.itemSO == item)
            {
                itemData.count += count;
                return;
            }
        }
        foreach(ItemData itemData in itemDatas)
        {
            if (itemData.itemSO == null)
            {
                itemData.itemSO = item;
                itemData.count = count;
                return;
            }
        }
        itemDatas.Add(new ItemData
        {
            itemSO = item,
            count = count
        });
        ChangeItem();
    }
    /*
    public bool AddItem(ItemSO item,out int remainCount,int count = 1)
    {
        remainCount = 0;
        if(count <= 0)
        {
            return false;
        }
        if(item.stackable)
        {
            bool result = AddStackableItem(item, out remainCount, count);
            while (remainCount > 0 && AddStackableItem(item, out remainCount, remainCount))
            {
            }
            return result;
        }
        else
        {
            return AddNonStackableItem(item, out remainCount, count);
        }
    }
    public bool AddStackableItem(ItemSO item, out int remainCount, int count)
    {
        remainCount = 0;
        foreach (CircularSlot slot in inventorySlots)
        {
            if (slot.transform.childCount == 1)
            {
                InventoryItem existingItem = slot.transform.GetComponentInChildren<InventoryItem>();
                if (existingItem != null && existingItem.GetItemSO() == item && existingItem.GetCount() < item.maxStack)
                {                
                    int newCount = existingItem.GetCount() + count;
                    if (newCount > item.maxStack){
                        remainCount = newCount - item.maxStack;
                        newCount = item.maxStack;
                    }
                    existingItem.SetCount(newCount);
                    return true;
                }
            }else if(slot.transform.childCount == 0)
            {
                if (count > item.maxStack){
                    remainCount = count - item.maxStack;
                    count = item.maxStack;
                }
                //SpawnItem(item, slot, count);
                return true;
            }
        }
        remainCount = count;
        return false;
    }
    public bool AddNonStackableItem(ItemSO item, out int remainCount, int count)
    {
        remainCount = 0;
        foreach (CircularSlot slot in inventorySlots)
        {
            if (slot.transform.childCount == 0)
            {
                //SpawnItem(item, slot);
                count--;
                if(count <= 0)
                {
                    
                    return true;
                }
            }
        }
        remainCount = count;
        return false;
    }
    public void SpawnItem(ItemSO item, CircularSlot slot, int count = 1)
    {
        CircularItem newItem = Instantiate(inventoryItemPrefab, slot.transform);
        newItem.InitializeItem(item,slot,count);
    }
    */
  
    public void InitInventorySlot(CircularSlot[] inventorySlots)
    {
        this.inventorySlots = inventorySlots;
    }
}
