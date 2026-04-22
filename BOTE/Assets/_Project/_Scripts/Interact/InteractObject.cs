using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUHP;
using System;

public class InteractObject 
{
    public IsometricGrid<InteractObject> grid;
    public IInteractable interactable;
    public int x;
    public int y;

    protected virtual void Interact(ItemData currentItem)
    {
        Debug.Log(x + " " + y);
    }
    public InteractObject(IsometricGrid<InteractObject> grid, int x, int y,IInteractable interactable)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.interactable = interactable;
    }
}
