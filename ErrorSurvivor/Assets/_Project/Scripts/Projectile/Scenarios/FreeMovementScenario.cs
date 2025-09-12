using UnityEngine;

namespace ErrorSpace
{
    [CreateAssetMenu(fileName = "FreeMovementScenario", menuName = "Game/FreeMovementScenario")]
    public class FreeMovementScenario : ProjectileScenario
    {
        public override void UpdateProjectile(Projectile projectile, float deltaTime)
        {
            projectile.transform.position += projectile.transform.right * projectile.moveSpeed * deltaTime;
        }
    }
}
