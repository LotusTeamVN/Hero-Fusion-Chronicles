using NN.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NN.PathFinding
{
    public class Agent : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] protected float moveSpeed = 1f;
        [SerializeField] protected float angularSpeed = 2f;

        [Header("Path Finding")]
        [SerializeField] protected float timeUpdatePathPercent = 1f;

        [Header("Debuger")]
        [SerializeField] protected bool showMovePath = false;


        #region Event ===========================================================
        public NNEvent OnArrived = null;
        #endregion


        #region Properties ======================================================
        public NavMeshAgent NavmeshAgent
        {
            get
            {
                if (navmeshAgent == null)
                    navmeshAgent = GetComponent<NavMeshAgent>();
                return navmeshAgent;
            }
        }

        public float Radius => NavmeshAgent.radius;
        public int hashCode { get; set; } = -1;
        #endregion


        #region Private Field ===================================================
        private NavMeshAgent navmeshAgent = null;
        private NavMeshPath navmeshPath;
        private NavMeshHit hit;
        private int walkableArea;

        private Vector3 direction;
        private Vector3 navmeshPathPostion;

        private float countTime = 0;
        private List<Node> path;
        private Vector3 firtPath;
        private int currentPathIndex = 0;
        #endregion



        public void Initialized()
        {
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            navmeshPath = new NavMeshPath();
            walkableArea = NavMesh.GetAreaFromName("Walkable");
            hit = new NavMeshHit();
        }

        public void RotateToTarget(Vector3 target)
        {
            direction = target - transform.position;
            direction.y = transform.forward.y;
            Quaternion targetQuaternion = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, angularSpeed * Time.deltaTime);
        }

        public void FindPath(Vector3 target, float stoppingDistance = 999)
        {
            if (stoppingDistance == 999)
                stoppingDistance = Radius ;

            if (countTime == 0)
            {
                currentPathIndex = 0;
                path = PathfindingUtilities.FindPath(transform.position, target);
                if (path != null && path.Count >= 2)
                    path.RemoveAt(0);
            }

            if (path != null && currentPathIndex < path.Count)
                firtPath = path[currentPathIndex].WorldPosition;

            RotateToTarget(firtPath);
            NavmeshAgent.Move(transform.forward * moveSpeed * Time.deltaTime);

            if (DistanceToTarget(firtPath) <= Radius)
                currentPathIndex++;

            countTime += Time.deltaTime;
            if (countTime >= timeUpdatePathPercent)
                countTime = 0;

            if (DistanceToTarget(target) <= stoppingDistance && OnArrived != null)
                OnArrived.Act();
        }

        public void MoveOnNavigation(Vector3 target, float stoppingDistance = 999)
        {
            if (NavmeshAgent.CalculatePath(target, navmeshPath))
            {
                navmeshPathPostion = NavmeshAgent.SamplePathPosition(walkableArea, 1, out hit) ? hit.position : navmeshPath.corners[1];

                if (!NavmeshAgent.isOnNavMesh)
                {
                    LogTool.LogErrorEditorOnly(gameObject.name + " not is on Navmesh");
                    return;
                }

                RotateToTarget(navmeshPathPostion);
                NavmeshAgent.Move(transform.forward * moveSpeed * Time.deltaTime);

                if (stoppingDistance == 999)
                    stoppingDistance = Radius * 2;

                if (DistanceToTarget(target) <= stoppingDistance && OnArrived != null)
                    OnArrived.Act();

            }
            else
            {
                LogTool.LogErrorEditorOnly(gameObject.name + " calculatePath fail");
            }
        }

        public float DistanceToTarget(Vector3 target)
        {
            float distance = Vector3.Distance(transform.position, new Vector3(target.x, transform.position.y, target.z));
            return distance;
        }

        private void OnDrawGizmos()
        {
            if (showMovePath && path != null && path.Count > 0)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Gizmos.DrawLine(path[i].WorldPosition, path[i + 1].WorldPosition);
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(path[i].WorldPosition, Vector3.one * 0.2f);
                }
            }
        }
    }
}

