using DG.Tweening;
using UnityEngine;

namespace ErrorSpace
{
    public class DancingComponent : MonoBehaviour
    {
        [SerializeField] private float time;
        [SerializeField] private float rotation;
        [SerializeField] private float scale;
        [SerializeField] private Ease ease;
        void Start()
        {
            var seq = DOTween.Sequence();
            seq.Append(transform.DORotate(Vector3.forward * rotation, time).SetEase(ease));
            seq.Append(transform.DORotate(-Vector3.forward * rotation, time).SetEase(ease));
            seq.Append(transform.DORotate(Vector3.forward * rotation, time).SetEase(ease));
            if (scale != 0)
                seq.Join(transform.DOShakeScale(scale, time).SetEase(ease));
            seq.SetLoops(-1);
        }
    }
}
