using CodeMonkey.Utils;
using UnityEngine;
using CUHP;

public class FarmGridManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;

    [SerializeField] private FarmLand landPrefab;
    [SerializeField] private FarmLand[,] lands;
    //

    public IsometricGrid<FarmLand> grid;
    void Start()
    {
        lands = new FarmLand[width, height];
        grid = new IsometricGrid<FarmLand>(width, height, cellSize, transform.position, (IsometricGrid<FarmLand> g, int x, int y) => {
            FarmLand farmLand = Instantiate(landPrefab, g.GetWorldPositionOffset(x, y), Quaternion.identity);
            farmLand.grid = g;
            farmLand.transform.parent = transform;
            farmLand.x = x;
            farmLand.y = y;
            lands[x, y] = farmLand;
            return farmLand;
        });
    }

    // Update is called once per frame
    
}
