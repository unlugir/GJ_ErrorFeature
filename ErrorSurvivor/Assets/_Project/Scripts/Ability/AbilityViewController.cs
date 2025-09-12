using Unity.VisualScripting;
using UnityEngine;

namespace ErrorSpace
{
    public class AbilityViewController
    {
        private AbilityView _abilityView;
        private AbilityData _abilityData;
        public AbilityViewController(AbilityView abilityView, AbilityData abilityData)
        {
            _abilityView = abilityView;
            _abilityData = abilityData;
            abilityView.Initialize(_abilityData);
        }
        
        public void Update(float deltaTime)
        {

        }
    }
}
