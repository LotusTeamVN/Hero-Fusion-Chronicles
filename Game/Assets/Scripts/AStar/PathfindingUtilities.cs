using System.Collections.Generic;
using UnityEngine;


namespace NN.PathFinding
{
    public static class PathfindingUtilities
    {
        public static List<Node> GetNeighbours(this Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int gridPosX = node.GridLocalPosX + x;
                    int gridPosY = node.GridLocalPosY + y;

                    if (gridPosX >= 0 && gridPosX < Grid.Instance.GridSizeX && gridPosY >= 0 && gridPosY < Grid.Instance.GridSizeY)
                    {
                        neighbours.Add(Grid.Instance.GetNode(gridPosX, gridPosY));
                    }
                }
            }

            return neighbours;
        }

        public static int DistanceTo(this Node from, Node to)
        {
            int dstX = Mathf.Abs(from.GridLocalPosX - to.GridLocalPosX);
            int dstY = Mathf.Abs(from.GridLocalPosY - to.GridLocalPosY);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }

        public static List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
        {
            if (!Grid.Instance.HasInitialized)
                return null;

            Node startNode = Grid.Instance.GetNode(startPos);
            Node targetNode = Grid.Instance.GetNode(targetPos);

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node node = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].FCost < node.FCost || openSet[i].FCost == node.FCost)
                    {
                        if (openSet[i].HCost < node.HCost)
                            node = openSet[i];
                    }
                }

                openSet.Remove(node);
                closedSet.Add(node);

                if (node == targetNode)
                {
                    List<Node> path = new List<Node>();
                    Node currentNode = targetNode;

                    while (currentNode != startNode)
                    {
                        path.Add(currentNode);
                        currentNode = currentNode.Parent;
                    }

                    path.Reverse();

                    return path;
                }

                foreach (Node neighbour in node.GetNeighbours())
                {
                    if (!neighbour.Walkable || neighbour.HaveObstacle || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newCostToNeighbour = node.GCost + node.DistanceTo(neighbour);
                    if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                    {
                        neighbour.GCost = newCostToNeighbour;
                        neighbour.HCost = neighbour.DistanceTo(targetNode);
                        neighbour.Parent = node;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }

            return null;
        }
    }
}

