using UnityEngine;

namespace ErrorSpace
{
    [CreateAssetMenu(fileName = "AbilityConfig", menuName = "Game/Ability Config")]
    public class AbilityConfig: ScriptableObject
    {
        public Sprite Icon;
        public string Name;
        public string Description;
        public float BaseCooldown;
        public GameObject Prefab;
    }
}
