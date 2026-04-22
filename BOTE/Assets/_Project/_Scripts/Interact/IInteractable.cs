using UnityEngine;

public interface IInteractable
{
    public Vector2Int position {get;}
    public void Interact(ItemData currentItem);
}