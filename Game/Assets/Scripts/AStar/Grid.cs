using UnityEngine;

namespace NN.PathFinding
{
    public class Grid : MonoBehaviour
    {
        [Header("Grid Configuration")]
        [SerializeField] private LayerMask notWalkableMask;
        [SerializeField] private LayerMask dynamicObstacleMask;
        [SerializeField] private Vector2 gridWorldSize;
        [SerializeField] private float nodeRadius;

        [Header("Debuger")]
        [SerializeField] private bool showGrid;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Color groundColor;
        [SerializeField] private Color walkableColor;
        [SerializeField] private Color notWalkableColor;
        [SerializeField] private float nodeDistanceOffset;
        [SerializeField] private float nodePosYOffset;

        public static Grid Instance = null;
        public float NodeDistanceOffset => nodeDistanceOffset;
        public float NodePosYOffset => nodePosYOffset;

        public LayerMask DynamicObstacleMask => dynamicObstacleMask;

        public float NodeLength => nodeRadius * 2;
        public int GridSizeX => Mathf.RoundToInt(gridWorldSize.x / NodeLength);
        public int GridSizeY => Mathf.RoundToInt(gridWorldSize.y / NodeLength);

        private bool hasInitialized = false;
        public bool HasInitialized => hasInitialized;

        private Vector3 GroundContact
        {
            get
            {
                if (Physics.Raycast(transform.position + Vector3.up * 5, Vector3.down, out RaycastHit hit, 100, groundMask))
                    return hit.point;
                return Vector3.zero;
            }
        }
        private Vector3 gridWorldBottomLeftPos;

        private Node[,] grid;


        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            CreateGrid();
            hasInitialized = true;
        }

        void CreateGrid()
        {
            grid = new Node[GridSizeX, GridSizeY];
            gridWorldBottomLeftPos = GroundContact - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

            for (int x = 0; x < GridSizeX; x++)
            {
                for (int y = 0; y < GridSizeY; y++)
                {
                    Vector3 worldPoint = gridWorldBottomLeftPos + Vector3.right * (x * NodeLength + nodeRadius) + Vector3.forward * (y * NodeLength + nodeRadius);
                    bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, notWalkableMask));
                    grid[x, y] = new Node(walkable, worldPoint, x, y);
                }
            }
        }

        public Node GetNode(Vector3 worldPosition)
        {
            int x = Mathf.FloorToInt(worldPosition.x - gridWorldBottomLeftPos.x);
            int y = Mathf.FloorToInt(worldPosition.z - gridWorldBottomLeftPos.z);

            if (x < 0 || y < 0)
                return null;

            return grid[x, y];
        }

        public Node GetNode(int gridLocalPosX, int gridLocalPosY)
        {
            return grid[gridLocalPosX, gridLocalPosY];
        }

        void OnDrawGizmos()
        {
            Gizmos.color = groundColor;
            Gizmos.DrawWireCube(GroundContact + Vector3.up * nodePosYOffset, new Vector3(gridWorldSize.x, 0, gridWorldSize.y));

            if (showGrid && grid != null)
            {
                foreach (Node n in grid)
                {
                    Gizmos.color = n.Walkable ? walkableColor : notWalkableColor;
                    Gizmos.DrawCube(n.WorldPosition + Vector3.up * nodePosYOffset, new Vector3(NodeLength - nodeDistanceOffset, 0, NodeLength - nodeDistanceOffset));
                }
            }
        }
    }
}
