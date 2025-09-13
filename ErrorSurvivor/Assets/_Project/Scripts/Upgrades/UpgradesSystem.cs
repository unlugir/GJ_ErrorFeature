using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

namespace ErrorSpace
{
    public class UpgradesSystem : MonoBehaviour
    {
        [SerializeField] private BoostSystem boostSystem;
        [SerializeField] private AbilitySystem abilitySystem;
        [SerializeField] private LevelUpView levelUpView;
        [SerializeField] private List<Stats> upgradeableStats;
        [SerializeField] private string newAbilityText;
        private System.Random _random = new();
        private void Start()
        {
            BoostSystem.OnLevelUp.AddListener(OnLevelUp);
        }

        public void Initialize()
        {
            
        }
        
        private void OnLevelUp(int levels)
        {
            int choicesCount = Random.Range(3, 6);
            int abilitiesCount = Random.Range(1, choicesCount - 1);
            int statsCount = choicesCount - abilitiesCount;
            
            List<UpgradeData> upgrades = new();
            var abilities = abilitySystem.GetAbilities();
            
            for (int i = 0; i < abilitiesCount; i++)
            {
                
                var ability = abilities[Random.Range(0, abilities.Count)];
                string description = ability.Level == 0 ? newAbilityText : "";
                description += "Increase damage and reduce cooldown";
                var upgrade = new AbilityUpgrade(ability, description);
                
                upgrades.Add(upgrade);
            }

            for (int i = 0; i < statsCount; i++)
            {
                Stats randomStat =upgradeableStats[Random.Range(0, upgradeableStats.Count)];
                string description = "{0}% boost";
                var upgrade = new StatUpgrade(randomStat, boostSystem, description);
                upgrades.Add(upgrade);
            }
            
            levelUpView.SetUpgrades(upgrades);
        }
    }
}
