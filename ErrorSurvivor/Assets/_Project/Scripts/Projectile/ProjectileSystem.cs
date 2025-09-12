using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
            Initialize();
        }

        public async UniTask Initialize()
        {
            while (transform.childCount > 0)
            {
                var obj = transform.GetChild(0);
                obj.parent = null;
                Destroy(obj.gameObject);
                await UniTask.WaitForEndOfFrame();
            }
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
