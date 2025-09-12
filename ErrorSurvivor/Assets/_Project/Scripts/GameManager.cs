using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ErrorSpace
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerSystem playerSystem;
        [SerializeField] private AbilitySystem abilitySystem;
        [SerializeField] private BoostSystem boostSystem;
        [SerializeField] private ProjectileSystem projectileSystem;
        
        //keep settings reference just in case its so is not loaded
        [SerializeField] private GameSettings gameSettings;

        
        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            projectileSystem.Initialize();
            playerSystem.Initialize();
            boostSystem.Initialize();
            abilitySystem.Initialize();   
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                StartGame();
        }
    }
}
