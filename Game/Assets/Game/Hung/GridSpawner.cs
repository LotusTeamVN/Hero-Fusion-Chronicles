using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The object you want to spawn.
    public GameObject spawnPlane;    // The plane to spawn objects on.
    public float gridSizeX = 7;      // Width of the grid.
    public float gridSizeZ = 18;     // Length of the grid.

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 touchPosition = Input.GetTouch(0).position;
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == spawnPlane)
            {
                Vector3 hitPosition = hit.point;

                // Round the hit position to the nearest grid cell
                Vector3 spawnPosition = RoundToGridCell(hitPosition);

                SpawnObjectAtPosition(spawnPosition);
            }
        }
    }

    // Rounds a position to the nearest grid cell.
    Vector3 RoundToGridCell(Vector3 position)
    {
        float cellSizeX = gridSizeX / Mathf.Round(gridSizeX);
        float cellSizeZ = gridSizeZ / Mathf.Round(gridSizeZ);

        float roundedX = Mathf.Round(position.x / cellSizeX) * cellSizeX;
        float roundedZ = Mathf.Round(position.z / cellSizeZ) * cellSizeZ;

        return new Vector3(roundedX, 0f, roundedZ);
    }

    // Spawns the object at the specified position.
    public void SpawnObjectAtPosition(Vector3 position)
    {
        Instantiate(objectToSpawn, position, Quaternion.identity);
    }
}
