using CUHP;
using UnityEngine;

public class PathNode 
{
    private IsometricGrid<PathNode> grid;
    public int x;
    public int y;

    public int G;
    public int H;
    public int F;

    public PathNode cameFromNode;
    public bool canWalk = true;
    public PathNode(IsometricGrid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }
    public void CalculateF()
    {
        F= G + H;
    }
    public void CalcutateH()
    {
        H = 0;
    }
    public void SetCanWalk(bool value)
    {
        canWalk = value;
    }
    public override string ToString()
    {
        if (G == int.MaxValue) return "";
        return G+"|"+F+"|"+H;
    }
    public Vector3 ReturnPathPosition()
    {
        return grid.GetWorldPosition(x,y)+ grid.XYToIso(Vector3.one/2);
        //return grid.GetWorldPosition(x,y)+ Vector3.one*grid.GetCellSize()/2;
    }
}
