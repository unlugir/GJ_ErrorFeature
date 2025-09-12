using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ErrorSpace
{
    public class ProjectileSystem : MonoBehaviour
    {
        private List<Projectile> _projectiles;

        private void Start()
        {
            _projectiles = new List<Projectile>();
        }

        public void Spawn(Ability projectileAbility)
        {
            var prefab = projectileAbility.Config.Prefab;
            if (prefab == null)
            {
                Debug.LogWarning($"No projectile prefab found for {projectileAbility.Config.Name}");
                return;
            }
            var projectile = Instantiate(prefab, this.transform);
            
            projectile.Initialize(this);
            projectile.SetVFXColor(projectileAbility.Config.RelatedColor);
            _projectiles.Add(projectile);
        }

        public void Finalize(Projectile projectile)
        {
            _projectiles.Remove(projectile);
        }
        
        private void Update()
        {
            for (int i = 0; i < _projectiles.Count; i++)
            {
                _projectiles[i].UpdateProjectile(Time.deltaTime);
            }
        }
    }
}
