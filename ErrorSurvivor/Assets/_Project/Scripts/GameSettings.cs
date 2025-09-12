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
        public Sprite sprite;
    }
    
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public static GameSettings Settings;
        public List<StatValuePair> initialStats;
        public Dictionary<Stats, Sprite> statSprites;
        
        [SerializeField] private List<StatSpritePair> _statSprites;
        
        private void PackSprites()
        {
            statSprites = _statSprites.ToDictionary(s => s.stat, s => s.sprite);
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
