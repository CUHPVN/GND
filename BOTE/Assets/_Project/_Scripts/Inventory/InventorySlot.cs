using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour , IDropHandler
{
    [Header("InventoryRoot")]
    [SerializeField] private Transform InventoryRoot;
    
    public void SetRoot(Transform root)
    {
        InventoryRoot = root;
    }
    public Transform GetRoot()
    {
        return InventoryRoot;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {            
            if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Item"))
            {
                InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
                if (item != null)
                {
                    item.transform.SetParent(transform);
                }
            }
        }
    }    
}
