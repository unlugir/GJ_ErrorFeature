using DG.Tweening;
using UnityEngine;

namespace ErrorSpace
{
    [CreateAssetMenu(fileName = "RandomPointOnCircleScenario", menuName = "Game/RandomPointOnCircleScenario")]
    public class RandomPointOnCircleScenario: ProjectileScenario
    {
        [SerializeField] private float scale;
        [SerializeField] private Ease ease;
        [SerializeField] private float radius;
        public override Vector2 GetContextPoint(Projectile projectile)
        {
            Vector2 origin = projectile.transform.position;
            var randomDirection = (Random.insideUnitCircle).normalized;
            var point = origin + randomDirection * radius;
            return point;
        }
        public override void StartProjectile(Projectile projectile)
        {
            var seq = DOTween.Sequence();
            seq.Append(projectile.transform.DOMove(projectile.ContextPoint, projectile.moveSpeed).SetEase(ease));
            seq.Join(projectile.transform.DOPunchScale(Vector3.one * scale, projectile.moveSpeed, vibrato: 1));
        }
    }
}
