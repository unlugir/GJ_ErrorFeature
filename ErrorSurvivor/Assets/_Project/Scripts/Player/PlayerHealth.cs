using UnityEngine;
using UnityEngine.Events;

namespace ErrorSpace
{
    public class PlayerHealth: IDamageable
    {
        public int Health
        {
            get => (sbyte)PlayerSystem.PlayerStats.Stats[Stats.Health];
            set => PlayerSystem.PlayerStats.Stats[Stats.Health] = (sbyte)value;
        }  
        public bool IsDead { get; private set; }
        public UnityEvent OnDamageTaken { get; private set; } = new();
        public UnityEvent OnDeath { get; private set; } = new();

        public void TakeDamage(int damage)
        {
            if (IsDead) return;
            OnDamageTaken.Invoke();
            Health -= damage;
            
            //CORE FEATURE 
            if (Health == 0)
                Die();
        }

        public void Die()
        {
            if (IsDead) return;
            IsDead = true;
            OnDeath.Invoke();
        }
    }
}
