using UnityEngine;
public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private ItemSO[] itemSOArray;
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private InventoryItem inventoryItemPrefab;

    void Awake()
    {
        itemSOArray = Resources.LoadAll<ItemSO>("SO/Items");
    }
    public ItemSO GetRandomItem()
    {
        int randomIndex = Random.Range(0, itemSOArray.Length);
        return itemSOArray[randomIndex];
    }
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
        foreach (InventorySlot slot in inventorySlots)
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
                SpawnItem(item, slot, count);
                return true;
            }
        }
        remainCount = count;
        return false;
    }
    public bool AddNonStackableItem(ItemSO item, out int remainCount, int count)
    {
        remainCount = 0;
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.transform.childCount == 0)
            {
                SpawnItem(item, slot);
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
    public void SpawnItem(ItemSO item, InventorySlot slot, int count = 1)
    {
        InventoryItem newItem = Instantiate(inventoryItemPrefab, slot.transform);
        newItem.InitializeItem(item,slot,count);
    }
  
    public void InitInventorySlot(InventorySlot[] inventorySlots)
    {
        this.inventorySlots = inventorySlots;
    }
}