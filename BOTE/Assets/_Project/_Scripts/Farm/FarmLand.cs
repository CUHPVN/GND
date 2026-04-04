using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CUHP;

public class FarmLand 
{
    private IsometricGrid<FarmLand> grid;
    public int x;
    public int y;
    public FarmLand(IsometricGrid<FarmLand> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }
}
