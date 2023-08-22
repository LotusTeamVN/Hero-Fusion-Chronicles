using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The object you want to spawn.

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 touchPosition = Input.GetTouch(0).position;
            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 hitPosition = hit.point;
                Vector3 offset = new Vector3(5, 0, 5);
                Vector3 matrixScale = new Vector3(10, 0, 10);

                // Calculate grid position based on the matrix
                int gridX = Mathf.RoundToInt((hitPosition.x - offset.x) / matrixScale.x);
                int gridY = Mathf.RoundToInt((hitPosition.z - offset.z) / matrixScale.z);

                SpawnObjectAtGridPosition(gridX, gridY);
            }
        }
    }

    // Spawns the object at the specified grid coordinates (x, y).
    public void SpawnObjectAtGridPosition(int x, int y)
    {
        Vector3 spawnPosition = new Vector3(5 + x * 10, 0f, 5 + y * 10);
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
