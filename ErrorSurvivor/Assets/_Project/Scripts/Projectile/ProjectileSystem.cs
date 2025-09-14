using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace ErrorSpace
{
    public class ProjectileSystem : MonoBehaviour
    {
        [SerializeField] private PlayerSystem playerSystem;

        private List<Projectile> _projectiles = new();

        public void Initialize()
        {
            foreach (var projectile in _projectiles)
            {
                Destroy(projectile.gameObject);
            }
            _projectiles.Clear();
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
            projectile.transform.position = PlayerSystem.Player.transform.position;
            
            float angle = Mathf.Atan2(PlayerSystem.Player.LookDirection.y, PlayerSystem.Player.LookDirection.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
          
            projectile.Initialize(this);
            projectile.damage += (projectile.damage * (GameSettings.Settings.damageIncreasePerLevel * (projectileAbility.Level - 1)));
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
