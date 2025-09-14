using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace ErrorSpace
{
    public class PlayerSystem : MonoBehaviour
    {
        public static UnityEvent OnPlayerDeath { get; private set; }= new();
        public static Character Player { get; private set; }
        public static PlayerStats PlayerStats { get; private set; }

        [SerializeField] private Character playerCharacterPrefab;
        [SerializeField] private CinemachineCamera playerCamera;
      
        private PlayerInput _playerInput;

        private bool _initialized = false;

        public void Initialize()
        {
            _initialized = false;
            _playerInput = new PlayerInput();
            PlayerStats = PlayerStats.Build();
            if (Player != null)
                Destroy(Player.gameObject);
            
            Player = Instantiate(playerCharacterPrefab, this.transform);
            Player.Initialize(new PlayerHealth());
            Player.HealthDamageable.OnDeath.AddListener(() => OnPlayerDeath.Invoke());
            playerCamera.Follow = Player.transform;
            _initialized = true;
        }
        
        void Update()
        {
            if (!_initialized) return;
            _playerInput.Update();

            _playerInput.Direction *= PlayerStats.Stats[Stats.MoveSpeed];
            _playerInput.Direction *= PlayerStats.Stats[Stats.InvertControls] > 0 ? -1 : 1;
            Player.InputUpdate(_playerInput, Time.deltaTime);
        }
    }
}
