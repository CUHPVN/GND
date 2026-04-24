using System;
using UnityEngine;

public interface IInteractable
{
    public Vector2Int position {get;}
    public bool Interact(ItemData currentItem, out bool isItemChanged);
}