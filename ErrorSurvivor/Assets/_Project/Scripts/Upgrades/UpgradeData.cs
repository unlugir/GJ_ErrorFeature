using UnityEngine;

namespace ErrorSpace
{
    public abstract class UpgradeData
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract Sprite Icon { get; }
        public abstract void Activate();
    }

    public class AbilityUpgrade : UpgradeData
    {
        public override string Name => _ability.Config.Name;
        public override string Description => _description;
        public override Sprite Icon => _ability.Config.Icon;
        
        private Ability _ability;
        private string _description;
        public AbilityUpgrade(Ability ability, string description)
        {
            _ability = ability;
            _description = description;
        }

        public override void Activate()
        {
            _ability.LevelUp();
        }
    }

    public class StatUpgrade : UpgradeData
    {
        public override string Name => GameSettings.Settings.statConfigs[_stat].name;
        public override string Description => _description;
        public override Sprite Icon => GameSettings.Settings.statConfigs[_stat].icon;
        
        private string _description;
        private Stats _stat;
        private float _increase;
        private BoostSystem _boostSystem;
        
        public StatUpgrade(Stats stat, BoostSystem boostSystem, string description)
        {
            
            _stat = stat;
            _increase = Random.Range(0.05f, 0.25f);
            if (GameSettings.Settings.statConfigs[_stat].invertBoost)
                _increase *= -1;
            _description = string.Format(description, Mathf.RoundToInt(_increase * 100));
            _boostSystem = boostSystem;
        }
        
        public override void Activate()
        {
            var boost = new Boost();
            boost.Add(_stat, _increase, -1);
            _boostSystem.AddBoost(boost);
        }
    }
}
