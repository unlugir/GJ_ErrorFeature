using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace ErrorSpace
{
    public class EnemyPathfinding : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private float enemySightRange = 20f;
        private float enemyMoveRange = 200f;
        
        public UnityEvent OnDestinationUpdated { get; private set; } = new();
        
        private void Start()
        {            
            navMeshAgent = transform.GetComponent<NavMeshAgent>();
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
        }

        private void Update()
        {
            if(Vector3.Distance(PlayerSystem.Player.transform.position, gameObject.transform.position)
               > enemySightRange)
                return;
            
            navMeshAgent.SetDestination(PlayerSystem.Player.transform.position);
            OnDestinationUpdated?.Invoke();
        }
    }
}
