using System;
using System.Collections.Generic;
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
        public float damage = 10f;
        
        [SerializeField] private LayerMask explosionLayerMask;
        [SerializeField] private float lifeTime;
        [SerializeField] private bool damageOnImpact;
        [SerializeField] private bool explodeOnDeath;
        [SerializeField] private ProjectileScenario scenario;
        [SerializeField] private ParticleSystem explodeParticles;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private SFXObject deathSFX;
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
            if (_dead) return;
            _dead = true;
            _life = 0;
            explodeParticles.Play();
            spriteRenderer.enabled = false;
            SFXManager.Main.Play(deathSFX);
            if (explodeOnDeath)
                Explode();
        }

        private void Explode()
        {
            var overlaps = Physics2D.OverlapCircleAll(this.transform.position, explosionRadius, explosionLayerMask);
            foreach (var overlap in overlaps)
            {
                var damageable = overlap.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
                if (damageable == null) continue;
                int calculatedDamage = Mathf.RoundToInt(this.damage
                                              * PlayerSystem.PlayerStats.Stats[Stats.DamageMultiplier]
                                              * PlayerSystem.PlayerStats.Stats[Stats.ExplosionMultiplier]);
                damageable.TakeDamage(calculatedDamage);
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_dead) return;
            Die();
            var damageable = other.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
            if (damageable == null) return;
            int calculatedDamage = Mathf.RoundToInt(this.damage
                                                    * PlayerSystem.PlayerStats.Stats[Stats.DamageMultiplier]
                                                    * PlayerSystem.PlayerStats.Stats[Stats.ImpactMultiplier]);
            damageable.TakeDamage(calculatedDamage);
        }
    }
}
