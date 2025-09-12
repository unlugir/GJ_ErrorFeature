using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace ErrorSpace
{
    public class AbilityView: MonoBehaviour
    {
        [SerializeField] AbilityStatusView abilityStatusViewPrefab;
        [SerializeField] RectTransform abilityStatusViewContainer;
        
        private List<AbilityStatusView> _abilityStatusViews;
        private AbilityData _abilityData;
        public async UniTask Initialize(AbilityData abilityData)
        {
            _abilityData = abilityData;
            _abilityStatusViews = new List<AbilityStatusView>();
            
            while (abilityStatusViewContainer.transform.childCount > 0)
            { 
                var obj = abilityStatusViewContainer.transform.GetChild(0);
                obj.parent = null;
                Destroy(obj.gameObject);
                await UniTask.WaitForEndOfFrame();
            }
            
            foreach (var ability in abilityData.Abilities)
            {
                var abilityView = Instantiate(abilityStatusViewPrefab, abilityStatusViewContainer);
                abilityView.SetAbility(ability);
                _abilityStatusViews.Add(abilityView);
            }
        }

        private void Update()
        {
            if (_abilityData == null) return;
            _abilityStatusViews.ForEach(ab => ab.UpdateAbilityStatus());
        }
    }
}
