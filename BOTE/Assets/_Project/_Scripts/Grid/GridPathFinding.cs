using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridPathFinding : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile tile;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    PathFinding pathFinding;
    public GameObject pathPrefab;
    public GameObject wall;
    List<GameObject> pathPrefabs = new();
    List<PathNode> resultPath=new List<PathNode>();
    public PathNode start;
    public PathNode end;
    private void Start()
    {
        pathFinding = new PathFinding(width, height, cellSize);
       
    }
    void Execute()
    {
        foreach (GameObject go in pathPrefabs)
        {
            Destroy(go);
        }
        if (resultPath != null) resultPath.Clear();
        if (start !=null) resultPath = pathFinding.FindPath(start.x,start.y, end.x, end.y);
        if (resultPath == null) return;
        for (int i = 0; i < resultPath.Count; i++)
        {
            PathNode node = resultPath[i];
            tilemap.SetTile(new Vector3Int(node.y, -node.x-1, 0), tile);
            pathPrefabs.Add(Instantiate(pathPrefab, node.ReturnPathPosition(), Quaternion.identity));
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            PathNode gridObject = pathFinding.GetGrid().GetGridObject(mousePosition);
            if (gridObject != null)
            {
                start = gridObject;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            PathNode gridObject = pathFinding.GetGrid().GetGridObject(mousePosition);
            if (gridObject != null)
            {
                gridObject.SetCanWalk(false);
                Instantiate(wall, gridObject.ReturnPathPosition(), Quaternion.identity);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            PathNode gridObject = pathFinding.GetGrid().GetGridObject(mousePosition);
            if (gridObject != null)
            {
                end = gridObject;
                Execute();
            }
        }
    }
}
