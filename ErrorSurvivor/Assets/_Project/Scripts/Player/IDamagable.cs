using UnityEngine;
using UnityEngine.Events;

namespace ErrorSpace
{
    public interface IDamageable
    {
        public int Health { get; }
        public bool IsDead { get; }
        public UnityEvent OnDamageTaken { get; }
        public UnityEvent OnDeath { get; }
        public void TakeDamage(int damage);
        public void Die();
    }
}
