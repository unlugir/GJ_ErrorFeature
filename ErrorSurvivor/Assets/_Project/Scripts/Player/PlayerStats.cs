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
        public Dictionary<Stats, float> Stats;
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
