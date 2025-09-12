using System;
using UnityEngine;
using UnityEngine.Events;

namespace ErrorSpace
{
    public class AbilitySystem : MonoBehaviour
    {
        public static readonly UnityEvent<Ability> AbilityExecuted = new();
        
        [SerializeField] private ProjectileSystem projectileSystem;
        [SerializeField] private AbilityConfig[] abilityConfigs;
        [SerializeField] private AbilityView abilityView;
        
        private AbilityViewController _abilityController;
        private AbilityData _abilityData;
        private void Start()
        {
            AbilityExecuted.AddListener(OnAbilityExecuted);
            Initialize();
        }

        public void Initialize()
        {
            _abilityData = new AbilityData();
            foreach (var abilityConfig in abilityConfigs)
            {
                _abilityData.Abilities.Add(Ability.Build(abilityConfig));
            }
            _abilityController = new AbilityViewController(abilityView, _abilityData);
        }
        
        private void OnAbilityExecuted(Ability ability)
        {
            switch (ability.Config.Type)
            {
                case AbilityType.Projectile:
                    projectileSystem.Spawn(ability);
                    break;
                case AbilityType.Boost:
                    break;
            }
        }
        
        private void Update()
        {
            _abilityController.Update(Time.deltaTime);
            foreach (var ability in _abilityData.Abilities)
            {
                if (ability.CanExecute())
                    ability.Execute();
            }
        }
        
    }
}
