using CodeMonkey.Utils;
using UnityEngine;
using CUHP;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;

    private IsometricGrid<FarmLand> grid;
    void Start()
    {
        grid = new IsometricGrid<FarmLand>(width, height, cellSize, transform.position, (IsometricGrid<FarmLand> g, int x, int y) => new FarmLand(g, x, y));
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
                if (grid.GetGridObject(mouseWorldPosition) != null)
                {
                    //TODO
                    Debug.Log(grid.GetGridObject(mouseWorldPosition).x + " " + grid.GetGridObject(mouseWorldPosition).y);
                }
            }
    }
}
