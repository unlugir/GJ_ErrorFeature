using System;
using UnityEngine;

namespace ErrorSpace
{
    public class EnemyAttacker : MonoBehaviour
    {
        [SerializeField] private float attackRange;

        [SerializeField] private int damage;

        [SerializeField] private float attackRate;


        private float _attackTimer = 0;
        
        void Update()
        {
            if (PlayerSystem.Player == null) return;
            Character player = PlayerSystem.Player;
            _attackTimer += Time.deltaTime;
            if (_attackTimer < attackRate) return;
            if (Vector3.Distance(player.transform.position, this.transform.position) > attackRange) return;
            _attackTimer = 0;
            player.HealthDamageable.TakeDamage(damage);
        }
    }
}
