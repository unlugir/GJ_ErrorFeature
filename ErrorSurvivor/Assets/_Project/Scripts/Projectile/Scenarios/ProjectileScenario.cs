using UnityEngine;

namespace ErrorSpace
{
    public abstract class ProjectileScenario : ScriptableObject
    {
        public virtual void StartProjectile(Projectile projectile) { }
        public virtual void UpdateProjectile(Projectile projectile, float deltaTime) {}
        public virtual Vector2 GetContextPoint(Projectile projectile) => Vector2.zero;
    }
}
