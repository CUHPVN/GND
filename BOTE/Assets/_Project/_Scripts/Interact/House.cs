using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour,IInteractable
{
    public Vector2Int position;

    Vector2Int IInteractable.position => position;

    public void Interact(ItemData currentItem)
    {
        Debug.Log("House");
    }
}
