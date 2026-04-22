using Unity.VisualScripting;
using UnityEngine;
public class CircularManager : Singleton<CircularManager>
{
    [SerializeField] private ItemSO[] itemSOArray;
    [SerializeField] private CircularSlot[] inventorySlots;
    [SerializeField] private ItemData[] itemData;

    private CanvasCircular canvas;
    public ItemData currentItem;

    void Awake()
    {
        itemSOArray = Resources.LoadAll<ItemSO>("SO/Items");
    }
    void Start()
    {
        canvas = UIManager.Instance.OpenUI<CanvasCircular>();
        canvas.Close(0);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            canvas.SetItem(itemData);
            canvas.Open();
        }
        if (Input.GetKey(KeyCode.E))
        {
            canvas.Highlight(canvas.FindSlotMouseOver());
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            currentItem = canvas.GetCurrentItem();
            canvas.Close(0);
        }
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
    // public void SpawnItem(ItemSO item, CircularSlot slot, int count = 1)
    // {
    //     CircularItem newItem = Instantiate(inventoryItemPrefab, slot.transform);
    //     newItem.InitializeItem(item,slot,count);
    // }
  
    public void InitInventorySlot(CircularSlot[] inventorySlots)
    {
        this.inventorySlots = inventorySlots;
    }
}
