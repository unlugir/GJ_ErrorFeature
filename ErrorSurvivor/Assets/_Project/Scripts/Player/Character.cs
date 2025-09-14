using System;
using UnityEngine;
using UnityEngine.AI;

namespace ErrorSpace
{
    public class Character : MonoBehaviour
    {
        public IDamageable HealthDamageable { get; private set; }
        public Vector3 LookDirection {get; private set;}
        
        private NavMeshAgent _navMeshAgent;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
        }

        public void Initialize(IDamageable health)
        {
            HealthDamageable = health ?? GetComponent<IDamageable>();
        }
        public void InputUpdate(PlayerInput playerInput, float deltaTime)
        {
            //_navMeshAgent.M
            //_navMeshAgent.Move(playerInput.Direction * deltaTime);
            LookDirection = (playerInput.MouseWorldPosition - transform.position).normalized;
            transform.Translate(playerInput.Direction * deltaTime);  
        }

        private void OnDestroy()
        {
            HealthDamageable?.OnDeath.RemoveAllListeners();
            HealthDamageable?.OnDamageTaken.RemoveAllListeners();
        }
    }
}
