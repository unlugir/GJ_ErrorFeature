using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ErrorSpace
{
    public enum Stats
    {
        Health,
        Experience,
        MoveSpeed,
        ExplosionMultiplier,
        ImpactMultiplier,
        DamageMultiplier,
        MagnetMultiplier,
        ExperienceMultiplier,
        CooldownMultiplier,
        InvertControls
    }
    
    [System.Serializable]
    public class PlayerStats
    {
        public const int ExperiencePerLevel = 25;
        public Dictionary<Stats, float> Stats;
        public int Level => Mathf.FloorToInt(Stats[ErrorSpace.Stats.Experience] / ExperiencePerLevel); 
        public float LevelProgression => (Stats[ErrorSpace.Stats.Experience] - Level * ExperiencePerLevel) / ExperiencePerLevel;
        private PlayerStats()
        { 
        }

        public static PlayerStats Build()
        {
            PlayerStats stats = new();
            stats.Stats = GameSettings.Settings.initialStats.ToDictionary(e => e.stat, e => e.value);
            return stats;
        }
       
    }
}
