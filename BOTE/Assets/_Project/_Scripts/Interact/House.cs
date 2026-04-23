using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour,IInteractable
{
    public Vector2Int position;

    Vector2Int IInteractable.position => position;

    public bool Interact(ItemData currentItem, out bool isItemChanged)
    {
        UIManager.Instance.OpenUI<CanvasHouse>();
        isItemChanged = false;
        return false;
    }

  
}
