using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject cellPrefab;      // Prefab of the cell cube
    public GameObject objectToSpawn;   // Prefab of the object to spawn
    public Vector3 gridSize;           // Dimensions of the grid (rows, columns)
    public float cellSize = 1f;        // Size of each cell
    public float gapSize = 0.2f;       // Gap between cells

    void Start()
    {
        GenerateGrid();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 touchPosition = Input.GetTouch(0).position;
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 closestCellPosition = GetClosestCellPosition(hit.point);
                SpawnObjectOnGrid(closestCellPosition);
            }
        }
    }

    void GenerateGrid()
    {
        for (int row = 0; row < gridSize.x; row++)
        {
            for (int col = 0; col < gridSize.z; col++)
            {
                Vector3 position = new Vector3(
                    col * (cellSize + gapSize) + 0.5f,
                    0,
                    row * (cellSize + gapSize) + 0.5f
                );

                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);
                cell.transform.localScale = new Vector3(cellSize, cellSize, cellSize);
                cell.transform.parent = transform;
            }
        }
    }

    Vector3 GetClosestCellPosition(Vector3 position)
    {
        int colIndex = Mathf.RoundToInt(position.x / (cellSize + gapSize));
        int rowIndex = Mathf.RoundToInt(position.z / (cellSize + gapSize));

        float cellX = colIndex * (cellSize + gapSize) + 0.5f;
        float cellZ = rowIndex * (cellSize + gapSize) + 0.5f;

        return new Vector3(cellX, 0.85f, cellZ);
    }

    void SpawnObjectOnGrid(Vector3 position)
    {
        Instantiate(objectToSpawn, position, Quaternion.identity);
    }
}
