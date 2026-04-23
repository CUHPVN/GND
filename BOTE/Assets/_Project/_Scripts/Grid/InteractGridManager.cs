using CodeMonkey.Utils;
using UnityEngine;
using CUHP;
using Unity.VisualScripting;

public class InteractGridManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;

    [SerializeField] private InteractObject interactObjectPrefabs;
    [SerializeField] private GameObject[] interactObject;
    //

    public IsometricGrid<InteractObject> grid;
    void Start()
    {
        grid = new IsometricGrid<InteractObject>(width, height, cellSize,Vector3.zero, (IsometricGrid<InteractObject> g, int x, int y) => null);
        foreach(GameObject gameObject in interactObject)
        {
            IInteractable interactable = gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                int x = interactable.position.x;
                int y = interactable.position.y;
                grid.SetGridObject(x,y,new InteractObject(grid,x,y,interactable));
            }
        }
    }
}
