using System;
using UnityEngine;

namespace ErrorSpace
{
    public class WorldBoost : MonoBehaviour
    {
        [SerializeField] private Boost boost;
        private void OnTriggerEnter2D(Collider2D other)
        {
            BoostSystem.OnBoostPickedUp.Invoke(boost);
            Destroy(this.gameObject);
        }
    }
}
