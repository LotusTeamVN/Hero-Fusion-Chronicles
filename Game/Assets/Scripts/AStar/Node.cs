using UnityEngine;

namespace NN.PathFinding
{
    public class Node
    {
        private bool walkable;
        private Vector3 worldPosition;
        private int gridLocalPosX;
        private int gridLocalPosY;

        public bool Walkable => walkable;
        public bool HaveObstacle => Physics.CheckSphere(worldPosition, Grid.Instance.NodeLength / 2, Grid.Instance.DynamicObstacleMask);

        public Vector3 WorldPosition => worldPosition;

        public int GridLocalPosX => gridLocalPosX;
        public int GridLocalPosY => gridLocalPosY;

        public int GCost { get; set; }
        public int HCost { get; set; }
        public int FCost => GCost + HCost;
        public Node Parent { get; set; } 


        public Node(bool walkable, Vector3 worldPosition, int gridLocalPosX, int gridLocalPosY)
        {
            this.walkable = walkable;
            this.worldPosition = worldPosition;
            this.gridLocalPosX = gridLocalPosX;
            this.gridLocalPosY = gridLocalPosY;
        }
    }
}


