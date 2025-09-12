using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace ErrorSpace
{
    public class Projectile : MonoBehaviour
    {
        public float LifeSpan => Mathf.Clamp01(_life / lifeTime);
        
        public float moveSpeed = 10f;
        public float explosionRadius = 2f;

        [SerializeField] private float lifeTime;
        [SerializeField] private bool damageOnImpact;
        [SerializeField] private bool explodeOnDeath;
        [SerializeField] private ProjectileScenario scenario;
        [SerializeField] private ParticleSystem explodeParticles;
        [SerializeField] private SpriteRenderer spriteRenderer;
        private ProjectileSystem _projectileSystem;
        private float _life;
        private bool _dead;
        public Vector2 ContextPoint { get; private set; }
        public void Initialize(ProjectileSystem projectileSystem)
        {
            _dead = false;
            _projectileSystem = projectileSystem;
            ContextPoint = scenario.GetContextPoint(this);
            scenario.StartProjectile(this);
            
            if (!damageOnImpact)
                GetComponent<Collider2D>().enabled = false;
        }

        public void SetVFXColor(Color color)
        {
            explodeParticles.startColor = color;
        }
        public void UpdateProjectile(float deltaTime)
        {
            _life += deltaTime;
            if (_dead)
            {
                if (_life > explodeParticles.main.duration)
                {
                    _projectileSystem.Finalize(this);
                    Destroy(this.gameObject);
                }
                return;
            }
            
            scenario.UpdateProjectile(this, deltaTime);
            if (lifeTime > 0 && _life > lifeTime)
            {
                Die();
            }
        }
        private void Die()
        {
            _dead = true;
            _life = 0;
            explodeParticles.Play();
            spriteRenderer.enabled = false;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
        }
    }
}
