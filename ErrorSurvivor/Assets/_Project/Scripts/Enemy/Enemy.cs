using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

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
        [SerializeField] private int score;
        public void Initialise()
        {
            enemyPathfinding.OnDestinationUpdated.AddListener(SwapSprite);
            enemySprite.sprite = enemyVariation[Random.Range(0, enemyVariation.Count)];
            Health = HealthFormula(Health, PlayerSystem.PlayerStats.Level);
        }

        private int HealthFormula(int defaultHealth, int level)
        {
            defaultHealth += Mathf.RoundToInt(level * 1.5f);
            return defaultHealth;
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
            GameManager.Score += score;
            Destroy(this.gameObject);
        }

        private void OnDestroy()
        {
            OnDeath.RemoveAllListeners();
            OnDamageTaken.RemoveAllListeners();
        }
    }
}
