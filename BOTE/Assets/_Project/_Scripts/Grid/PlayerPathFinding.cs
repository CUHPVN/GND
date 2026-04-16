using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerPathFinding : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile tile;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    PathFinding pathFinding;
    public GameObject pathPrefab;
    public GameObject wall;
    public GameObject hightLight;
    List<GameObject> pathPrefabs = new();
    List<PathNode> resultPath=new List<PathNode>();
    public PathNode start;
    public PathNode end;
    [Tooltip("It's must be config after start")]
    public bool debugMode =false;

    [SerializeField] private PlayerController playerController;
    private void Start()
    {
        pathFinding = new PathFinding(width, height, cellSize,debugMode);
       
    }
    void Execute()
    {
        (int x,int y)= playerController.GetPosition();
        start = pathFinding.GetGrid().GetGridObject(x,y);
        foreach (GameObject go in pathPrefabs)
        {
            Destroy(go);
        }
        if (resultPath != null) resultPath.Clear();
        if (start !=null) resultPath = pathFinding.FindPath(start.x,start.y, end.x, end.y);
        if (resultPath == null) return;
        CallBackPlayer();
        // for (int i = 0; i < resultPath.Count; i++)
        // {
        //     PathNode node = resultPath[i];
        //     //tilemap.SetTile(new Vector3Int(node.y, -node.x-1, 0), tile);
        //     pathPrefabs.Add(Instantiate(pathPrefab, node.ReturnPathPosition(), Quaternion.identity));
        // }
    }
    private void CallBackPlayer()
    {
        Vector2Int[] pos = new Vector2Int[resultPath.Count];
        Vector2[] worldPos = new Vector2[resultPath.Count];
        for(int i = 0; i < resultPath.Count; i++)
        {
            pos[i]= new Vector2Int(resultPath[i].x,resultPath[i].y);
            worldPos[i]= resultPath[i].ReturnPathPosition();
        }
        playerController.MoveTo(worldPos,pos);
    }
    private void Update()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        PathNode gridObject = pathFinding.GetGrid().GetGridObject(mousePosition);
        if (gridObject!=null)
        {
            hightLight.SetActive(true);
            hightLight.transform.position = gridObject.ReturnPathPosition();
        }
        else
        {
            hightLight.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (gridObject != null)
            {
                gridObject.SetCanWalk(false);
                Instantiate(wall, gridObject.ReturnPathPosition(), Quaternion.identity);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (gridObject != null)
            {
                end = gridObject;
                Execute();
            }
        }
    }
}
