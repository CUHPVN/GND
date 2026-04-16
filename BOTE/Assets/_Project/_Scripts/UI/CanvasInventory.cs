using UnityEngine;
using UnityEngine.UI;

public class CanvasInventory : UICanvas
{
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private Button addItemButton;
    [SerializeField] private Button addItemRandomCountButton;
    
    public void ReloadButton()
    {
    }
    private void Awake()
    {
        InitInventorySlot();
    }
    private void OnEnable()
    {
        InitButtonEvent();
    }
    private void Start()
    {
        SetRoot();
    }
    private void OnDisable()
    {
        ReleaseButtonEvent();
    }
    private void SetRoot()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.SetRoot(transform);
        }
    }
    private void AddItem()
    {
        int remainCount=0;
        bool added = InventoryManager.Instance.AddItem(InventoryManager.Instance.GetRandomItem(), out remainCount);
        if (!added)
        {
            Debug.Log("Inventory is full!");
        }else if (remainCount > 0)
        {
            Debug.Log("Added with " + remainCount + " remain!");
        }
    }
    private void AddItemRandomCount()
    {
        int remainCount=0;
        ItemSO item = InventoryManager.Instance.GetRandomItem(); 
        bool added = InventoryManager.Instance.AddItem(item, out remainCount,item.stackable?Random.Range(1, 5):1);
        if (!added)
        {
            Debug.Log("Inventory is full!");
        }else if (remainCount > 0)
        {
            Debug.Log("Added with " + remainCount + " remain!");
        }
    }
    private void InitInventorySlot()
    {
        InventoryManager.Instance.InitInventorySlot(inventorySlots);
    } 
    private void InitButtonEvent()
    {
        addItemButton.onClick.AddListener(() => AddItem() );
        addItemRandomCountButton.onClick.AddListener(() => AddItemRandomCount() );
    }
    private void ReleaseButtonEvent()
    {
        addItemButton.onClick.RemoveAllListeners();
        addItemRandomCountButton.onClick.RemoveAllListeners();
    }
}
