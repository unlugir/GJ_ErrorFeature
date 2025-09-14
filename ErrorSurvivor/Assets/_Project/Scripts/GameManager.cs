using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ErrorSpace
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerSystem playerSystem;
        [SerializeField] private AbilitySystem abilitySystem;
        [SerializeField] private BoostSystem boostSystem;
        [SerializeField] private ProjectileSystem projectileSystem;
        [SerializeField] private UpgradesSystem upgradesSystem;
        [SerializeField] private EnemySystem enemySystem;
        //keep settings reference just in case its so is not loaded
        [SerializeField] private GameSettings gameSettings;

        //fast ui
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private TMP_Text scoreText;
        
        public static int Score { get; set; }

        private void Start()
        {
            MusicManager.Main.PlayFromLibrary("Main Track");
            PlayerSystem.OnPlayerDeath.AddListener(()=> menuPanel.SetActive(true));
            //StartGame();
        }

        public void StartGame()
        {
            Score = 0;
            menuPanel.SetActive(false);
            projectileSystem.Initialize();
            playerSystem.Initialize();
            boostSystem.Initialize();
            abilitySystem.Initialize();
            upgradesSystem.Initialize();
            enemySystem.Initialize();
        }
        
        private void Update()
        {
            scoreText.text = $"Score: {Score.ToString()}";
        }
    }
}
