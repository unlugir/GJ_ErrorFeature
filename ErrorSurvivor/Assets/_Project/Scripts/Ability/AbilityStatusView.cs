using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ErrorSpace
{
    public class AbilityStatusView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Image cooldown;
        [SerializeField] private TMP_Text level;
        
        private Ability _ability;
        public void SetAbility(Ability ability)
        {
            _ability = ability;
            if (ability == null)
            {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
            level.text = ability.Level.ToString();
            icon.sprite = ability.Config.Icon;
            cooldown.fillAmount = ability.Cooldown01;

            var color = ability.Config.RelatedColor;
            color.a = cooldown.color.a;
            cooldown.color = color;
        }

        public void UpdateAbilityStatus()
        {
            if (_ability == null)
            {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(_ability.Level > 0);
            level.text = _ability.Level.ToString();
            cooldown.fillAmount = _ability.Cooldown01;
        }
    }
}
