using CUHP;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private IsometricGrid<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closeList;

    public PathFinding(int width , int height, float cellSize)
    {
        grid = new IsometricGrid<PathNode>(width, height, cellSize, Vector3.zero, (IsometricGrid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }
    public List<PathNode> FindPath(int startX, int startY, int endX,int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject (endX, endY);
        openList = new List<PathNode> {startNode};
        closeList = new List<PathNode>();
        for(int i = 0; i < grid.GetWidth(); i++)
        {
            for(int j = 0;j< grid.GetHeight(); j++)
            {
                PathNode pathNode = grid.GetGridObject(i, j);
                pathNode.G = int.MaxValue;
                pathNode.H = CalculateDistanceCost(pathNode, endNode);
                pathNode.CalculateF();
                pathNode.cameFromNode = null;
                grid.SetGridObject(i,j,pathNode);
            }
            
        }
        startNode.G = 0;
        startNode.H = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateF();
        grid.SetGridObject(startX,startY, startNode);
  
        while (openList.Count > 0)
        {
            PathNode curentNode = GetLowerFNode(openList);
            if(curentNode == endNode)
            {
                return CalculatePath(curentNode);
            }
            openList.Remove(curentNode);
            closeList.Add(curentNode);
            foreach (PathNode pathNode in GetNeighbourList(curentNode))
            {
                if (openList.Contains(pathNode)) continue;
                int tmpG = curentNode.G +CalculateDistanceCost(curentNode,pathNode);
                if(tmpG < pathNode.G)
                {
                    pathNode.cameFromNode = curentNode;
                    pathNode.G = tmpG;
                    pathNode.H = CalculateDistanceCost(pathNode, endNode);
                    pathNode.CalculateF();
                    grid.SetGridObject(pathNode.x, pathNode.y, pathNode);
                    if(!openList.Contains(pathNode)) openList.Add(pathNode);
                }
            }
        }
        //Not Found;
        return null;
    }
    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbour=new ();
        int x=currentNode.x, y=currentNode.y;
        if (x - 1 >= 0)
        {
            neighbour.Add(grid.GetGridObject(x-1,y));
            if(y-1 >= 0) neighbour.Add(grid.GetGridObject(x - 1, y-1));
            if (y + 1 < grid.GetHeight()) neighbour.Add(grid.GetGridObject(x - 1, y + 1));
        }
        if(y+1<grid.GetHeight()) neighbour.Add(grid.GetGridObject(x, y+1));
        if (y - 1 >=0) neighbour.Add(grid.GetGridObject(x, y - 1));

        if (x + 1 < grid.GetWidth())
        {
            neighbour.Add(grid.GetGridObject(x + 1, y));
            if (y - 1 >= 0) neighbour.Add(grid.GetGridObject(x + 1, y - 1));
            if (y + 1 < grid.GetHeight()) neighbour.Add(grid.GetGridObject(x + 1, y + 1));
        }
        List<PathNode> neighbour2 = new();
        foreach(PathNode node in neighbour)
        {
            if (node.canWalk)
            {
                neighbour2.Add(node);
            }
        }
        return neighbour2;
    }
    private int CalculateDistanceCost(PathNode a,PathNode b)
    {
        int x = Mathf.Abs(a.x - b.x);
        int y = Mathf.Abs(a.y - b.y);
        int rm = Mathf.Abs(x - y);
        return rm * MOVE_STRAIGHT_COST+Mathf.Min(x,y)*MOVE_DIAGONAL_COST;
    }
    private PathNode GetLowerFNode(List<PathNode> openList)
    {
        PathNode MinNode = openList[0];
        for(int i = 0; i < openList.Count; i++)
        {
            if (openList[i].F < MinNode.F)
            {
                MinNode = openList[i];
            }
        }
        return MinNode;
    }
    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> pathNodes=new List<PathNode>();
        PathNode tempNode = endNode;
        while(tempNode != null)
        {
            pathNodes.Add(tempNode);
            tempNode = tempNode.cameFromNode;
        }
        pathNodes.Reverse();
        return pathNodes;
    }
    public IsometricGrid<PathNode> GetGrid()
    {
        return grid;
    }
}
