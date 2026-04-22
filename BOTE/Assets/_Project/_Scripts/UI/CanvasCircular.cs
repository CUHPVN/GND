using System;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.UI;

public class CanvasCircular : UICanvas
{
    [Header("Inventory")]
    [SerializeField] private int ItemCount = 10;
    [SerializeField] private float slotFill => 1/(float)ItemCount;
    [SerializeField] private int preIndex=-1;
    [SerializeField] private Transform cameraHandle;
    [SerializeField] private CircularSlot prefabs;
    [SerializeField] private Transform toolBar;
    [SerializeField] private List<CircularSlot> inventorySlots=new List<CircularSlot>();
    [SerializeField] private Button addItemButton;
    [SerializeField] private Button addItemRandomCountButton;
    [SerializeField] private ItemData currentItem;
    
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
        SpawnInventorySlot(ItemCount);
    }

    private void SpawnInventorySlot(int ItemCount)
    {
        float offset = 360/ItemCount/2;
        for (int i = 0; i < ItemCount; i++)
        {
            CircularSlot slot = Instantiate(prefabs,toolBar);
            slot.GetComponent<Image>().fillAmount = slotFill-slotFill*0.1f;
            float angle = (float)(i+1)*360/ItemCount-0.1f*0.5f*360/ItemCount;
            float exactAngle = (float)(i+1)*360/ItemCount+0.1f;
            slot.transform.Rotate(0,0,angle);
            slot.itemHandle.transform.Rotate(0,0,-angle);
            float x = Mathf.Sin(Mathf.Deg2Rad*(exactAngle-offset))*300;
            float y = Mathf.Cos(Mathf.Deg2Rad*(exactAngle-offset))*-300;
            slot.itemHandle.GetChild(0).localPosition=new Vector3(x,y,0);
            inventorySlots.Add(slot);   
        }
    }
    private void Update()
    {
    }
    public int FindSlotMouseOver()
    {
        Vector3 mousePosition = UtilsClass.GetMousePosition();
        Vector3 dir = mousePosition-transform.position;
        int index=-1;
        if(dir.magnitude > 1.73f){
            float offset = 360/ItemCount;
            dir.Normalize();
            float angle = Mathf.Asin(dir.x)*Mathf.Rad2Deg;
            if (dir.y >= 0 && dir.x <= 0)
            {
                angle=180-angle;
            }else
            if (dir.y >= 0 && dir.x >= 0)
            {
                angle=180-angle;
            }
            else if(dir.y<=0&&dir.x <= 0)
            {
                angle+=360;
            }
            index = (int)(angle / offset);
        }
        currentItem = index==-1? null : inventorySlots[index].GetItem();
        return  index;
    }

    public void Highlight(int index)
    {
        
        if(preIndex >=0 && preIndex < ItemCount && preIndex != index)
        {
            inventorySlots[preIndex].transform.localScale = UnityEngine.Vector3.one;
            inventorySlots[preIndex].Unselected();
        }
        if (index >= 0 && index < ItemCount && preIndex!=index)
        {
            inventorySlots[index].transform.localScale= UnityEngine.Vector3.one*1.25f;
            inventorySlots[index].Selected();
        }
        preIndex=index;
    }
    private void OnDisable()
    {
        ReleaseButtonEvent();
    }

    private void InitInventorySlot()
    {
        CircularManager.Instance.InitInventorySlot(inventorySlots.ToArray());
    } 
    private void InitButtonEvent()
    {
        //addItemButton.onClick.AddListener(() => AddItem() );
        //addItemRandomCountButton.onClick.AddListener(() => AddItemRandomCount() );
    }
    private void ReleaseButtonEvent()
    {
        //addItemButton.onClick.RemoveAllListeners();
        //addItemRandomCountButton.onClick.RemoveAllListeners();
    }

    public void SetItem(ItemData[] items)
    {
        if (ItemCount < items.Length)
        {
            ItemCount = items.Length;
            SpawnInventorySlot(ItemCount);
        }
        for (int i = 0; i < items.Length; i++)
        {
            inventorySlots[i].SetItem(items[i]);
        }
    }
    public ItemData GetCurrentItem()
    {
        return currentItem;
    }
}

