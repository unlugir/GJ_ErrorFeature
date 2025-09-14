using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ErrorSpace
{
    [System.Serializable]
    public class StatValuePair
    {
        public Stats stat;
        public float value;
    }
    [System.Serializable]
    public class StatSpritePair
    {
        public Stats stat;
        public StatConfig config;
    }

    [System.Serializable]
    public class StatConfig
    {
        public string name;
        public string description;
        public Sprite icon;
        public bool invertBoost;
    }
    
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public static GameSettings Settings;
        public List<StatValuePair> initialStats;
        public Dictionary<Stats, StatConfig> statConfigs;
        public float cooldownReductionPerLevel = 0.05f;
        public float damageIncreasePerLevel = 0.05f;
        [SerializeField] private List<StatSpritePair> _statConfigs;
        
        private void PackSprites()
        {
            statConfigs = _statConfigs.ToDictionary(s => s.stat, s => s.config);
        }

        private void OnValidate()
        {
            PackSprites();
        }

        private void OnEnable()
        {
            PackSprites();
            Settings = this;
        }
    }
}
