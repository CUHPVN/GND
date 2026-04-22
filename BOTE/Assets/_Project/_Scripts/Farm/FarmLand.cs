using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUHP;
using System;

public class FarmLand : MonoBehaviour
{
    public IsometricGrid<FarmLand> grid;
    public int x;
    public int y;

    internal void Interact(ItemData currentItem)
    {
        Debug.Log(x + " " + y);
    }
    // public FarmLand(IsometricGrid<FarmLand> grid, int x, int y)
    // {
    //     this.grid = grid;
    //     this.x = x;
    //     this.y = y;
    // }
}
