using System.Collections.Generic;
using UnityEngine;

namespace ErrorSpace
{
    public class LevelUpView : MonoBehaviour
    {
        [SerializeField] private List<LevelUpCard> levelUpCards;
        
        private List<UpgradeData> _upgradeData;
        private void Start()
        {
            for (int i = 0; i < levelUpCards.Count; i++)
            {
                int index = i;
                levelUpCards[i].OnClick.AddListener(()=> OnBoostActivate(index));
                levelUpCards[i].gameObject.SetActive(false);
            }
        }

        public void SetUpgrades(List<UpgradeData> upgrades)
        {
            _upgradeData = upgrades;
            levelUpCards.ForEach(card => card.gameObject.SetActive(false));
            if (upgrades.Count > levelUpCards.Count)
            {
                Debug.LogWarning($"There are {upgrades.Count} upgrades, while only {levelUpCards.Count} cards are present");
                return;
            }
            for (int i = 0; i < upgrades.Count; i++)
            {
                var upgrade = upgrades[i];
                levelUpCards[i].Set(upgrade.Icon, upgrade.Description, upgrade.Name);
                
            }
        }

        public void OnBoostActivate(int index)
        {
            _upgradeData[index].Activate();
            levelUpCards.ForEach(card => card.gameObject.SetActive(false));
        }

        public void Hide()
        {
            levelUpCards.ForEach(card => card.gameObject.SetActive(false));
        }
    }
}
