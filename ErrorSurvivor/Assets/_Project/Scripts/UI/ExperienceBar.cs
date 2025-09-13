using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ErrorSpace
{
    public class ExperienceBar : MonoBehaviour
    {
        [SerializeField] Image fillImage;
        [SerializeField] TMP_Text levelText;
        
        private void Update()
        {
            if (PlayerSystem.Player == null)
            {
                fillImage.fillAmount = 0;
                levelText.text = "";
            }
            else
            {
                levelText.text = PlayerSystem.PlayerStats.Level.ToString();
                fillImage.fillAmount = PlayerSystem.PlayerStats.LevelProgression;
            }
        }
    }
}
