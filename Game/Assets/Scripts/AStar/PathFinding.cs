using NN.PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Agent agent;
    public Transform endPos;

    List<Node> path;


    private void Update()
    {
        agent.FindPath(endPos.position);
    }

    //private void OnDrawGizmos()
    //{
    //    if (path != null && path.Count > 0)
    //    {
    //        Gizmos.color = Color.yellow;
    //        foreach (Node node in path)
    //        {
    //            Gizmos.DrawCube(node.WorldPosition + Vector3.up * NN.PathFinding.Grid.Instance.NodePosYOffset * 2,
    //                new Vector3(NN.PathFinding.Grid.Instance.NodeLength - NN.PathFinding.Grid.Instance.NodeDistanceOffset, 0,
    //                NN.PathFinding.Grid.Instance.NodeLength - NN.PathFinding.Grid.Instance.NodeDistanceOffset));
    //        }
    //    }
    //}
}