using System;
using UnityEngine;

namespace ErrorSpace
{
    public enum AbilityType
    {
        Projectile,
        Boost
    }
    
    [CreateAssetMenu(fileName = "AbilityConfig", menuName = "Game/Ability Config")]
    public class AbilityConfig: ScriptableObject
    {
        public AbilityType Type;
        public Sprite Icon;
        public string Name;
        public string Description;
        public float BaseCooldown;
        public Projectile Prefab;
        public Color RelatedColor;
        private void OnValidate()
        {
            if (Type == AbilityType.Projectile && Prefab == null)
                Debug.LogError("Projectile prefab should be not null for projectile abilities");
        }
    }
}
