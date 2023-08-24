using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NN.PathFinding;
using NN.Utilities;

public class Hero_Action : MonoBehaviour
{
    #region PROPERTISE
    [SerializeField]
    private Agent _agent;
    [SerializeField]
    private int _AttackRange ;


    #endregion



    #region UNITYCORE
    private void Start()
    {
        
    }


    #endregion



    #region MAIN



    #endregion



    #region SUPPPORT

    private void OnDrawGizmos()
    {
        if (_agent != null && _agent.enabled)
        {

            Vector3 agentGridPos = NN.PathFinding.Grid.Instance.GetNode(_agent.transform.position).WorldPosition;
            Gizmos.color = Color.red;
            for (int x = -_AttackRange; x <= _AttackRange; x++)
            {
                for (int z = -_AttackRange; z <= _AttackRange; z++)
                {

                    if (Mathf.Abs(x) + Mathf.Abs(z) <= _AttackRange)
                    {
                        Vector3 gridPosition = agentGridPos + new Vector3(x * NN.PathFinding.Grid.Instance.NodeLength, 0, z * NN.PathFinding.Grid.Instance.NodeLength);
                    
                        if (IsPositionWithinGridBounds(gridPosition))
                        {
                            Gizmos.DrawWireCube(gridPosition, new Vector3(NN.PathFinding.Grid.Instance.NodeLength, 0.1f, NN.PathFinding.Grid.Instance.NodeLength));
                        }
                    }
                }
            }
        }
    }

    private bool IsPositionWithinGridBounds(Vector3 position)
    {
        Vector3 gridSize = new Vector3(NN.PathFinding.Grid.Instance.GridSizeX * NN.PathFinding.Grid.Instance.NodeLength, 0, NN.PathFinding.Grid.Instance.GridSizeY * NN.PathFinding.Grid.Instance.NodeLength);
        return position.x >= 0 && position.x <= gridSize.x && position.z >= 0 && position.z <= gridSize.z;
    }



    #endregion


    #region EVENT


    #endregion
}