using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ErrorSpace
{
    public class HealthBar : MonoBehaviour
    {
        [Range(sbyte.MinValue, sbyte.MaxValue)] [SerializeField] sbyte health;
        
        [SerializeField] private Image mainFill;
        [SerializeField] private Image secondaryFill;
        [SerializeField] private Gradient gradient;
        [SerializeField] private TMP_Text text;
        
        private void UpdateBar()
        {
            secondaryFill.fillAmount = health >= 0 ? 0 : health / (float)sbyte.MinValue ;
            mainFill.fillAmount = health >= 0 ? health / (float)sbyte.MaxValue : 0;
            var color = gradient.Evaluate(health / (float)sbyte.MaxValue);

            mainFill.color = color; 
            secondaryFill.color = color;
            text.text = health.ToString();
        }
        
        private void OnValidate()
        {
            UpdateBar();
        }

        private void Update()
        {
            if (PlayerSystem.Player == null)
                health += 1;
            else
                health = (sbyte)PlayerSystem.Player.HealthDamageable.Health;
            
            UpdateBar();
        }
    }
}
