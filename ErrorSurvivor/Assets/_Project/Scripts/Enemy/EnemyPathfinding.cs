using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace ErrorSpace
{
    public class EnemyPathfinding : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        [SerializeField] private float enemySightRange = 20f;
        [SerializeField] private float enemyMoveRange = 200f;
        [SerializeField] private float destinationUpdateTime = 0.5f;
        
        private float _destinationUpdateTimer = 0;
        public UnityEvent OnDestinationUpdated { get; private set; } = new();
        
        private void Start()
        {            
            navMeshAgent = transform.GetComponent<NavMeshAgent>();
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
            _destinationUpdateTimer = destinationUpdateTime;
        }

        private void Update()
        {
            if(Vector3.Distance(PlayerSystem.Player.transform.position, gameObject.transform.position)
               > enemySightRange)
                return;

            _destinationUpdateTimer += Time.deltaTime;
            if (_destinationUpdateTimer < destinationUpdateTime) return;
            _destinationUpdateTimer = 0;
            navMeshAgent.SetDestination(PlayerSystem.Player.transform.position);
            OnDestinationUpdated?.Invoke();
        }
    }
}
