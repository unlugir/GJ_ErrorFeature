using System;
using UnityEngine;

namespace ErrorSpace
{
    public class Character : MonoBehaviour
    {
        public IDamageable HealthDamageable { get; private set; }
        public Vector3 LookDirection {get; private set;}

        public void Initialize(IDamageable health)
        {
            HealthDamageable = health ?? GetComponent<IDamageable>();
        }
        public void InputUpdate(PlayerInput playerInput, float deltaTime)
        {
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
