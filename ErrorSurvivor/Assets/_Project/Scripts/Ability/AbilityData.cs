using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace ErrorSpace
{
    public class AbilityData
    {
        public List<Ability> Abilities = new List<Ability>();
        public int Level => Abilities.Sum(a => a.Level);
    }

    public class Ability
    {
        public AbilityConfig Config { get; private set; }
        public DateTime LastUseTime { get; private set; }
        public int Level { get; private set; }
        public float Cooldown => Config.BaseCooldown * PlayerSystem.PlayerStats.Stats[Stats.CooldownMultiplier];
        public float Cooldown01 => 1 - Mathf.Clamp01((float)(DateTime.UtcNow - LastUseTime).TotalSeconds / (Cooldown));
        
        private Ability() { }

        public bool CanExecute()
        {
            return Cooldown01 <= 0;
        }

        public void Execute()
        {
            LastUseTime = DateTime.UtcNow;
            AbilitySystem.AbilityExecuted.Invoke(this);
        }
        public static Ability Build(AbilityConfig config)
        {
            var ability = new Ability();
            ability.Config = config;
            ability.LastUseTime = DateTime.UtcNow.AddSeconds(-ability.Cooldown);
            ability.Level = 0;
            return ability;
        }
    }
}
