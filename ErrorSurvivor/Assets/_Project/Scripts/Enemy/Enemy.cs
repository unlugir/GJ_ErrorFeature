using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ErrorSpace
{
    [RequireComponent(typeof(EnemyPathfinding))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        //https://music.youtube.com/watch?v=YoLLAGI7bqg&list=RDAMVMYoLLAGI7bqg
        [field: SerializeField] public int Health { get; private set; }
        public bool IsDead { get; private set;}
        public UnityEvent OnDamageTaken { get; private set; } = new();
        public UnityEvent OnDeath { get; private set; } = new();
        
        [SerializeField] private List<Sprite> enemyVariation;
        [SerializeField] private SpriteRenderer enemySprite;
        [SerializeField] private EnemyPathfinding enemyPathfinding;
        
        public void Initialise()
        {
            enemyPathfinding.OnDestinationUpdated.AddListener(SwapSprite);
            enemySprite.sprite = enemyVariation[Random.Range(0, enemyVariation.Count)];
        }

        private void SwapSprite()
        {
            enemySprite.flipX = gameObject.transform.position.x - PlayerSystem.Player.transform.position.x < 0;
        }

       
        public void TakeDamage(int damage)
        {
            if (IsDead) return;
            Health -= damage;
            OnDamageTaken.Invoke();
            if (Health <= 0)
                Die();
        }

        public void Die()
        {
            if (IsDead) return;
            IsDead = true;
            OnDeath.Invoke();
            EnemySystem.OnDeath.Invoke(this);
            Destroy(this.gameObject);
        }
    }
}
