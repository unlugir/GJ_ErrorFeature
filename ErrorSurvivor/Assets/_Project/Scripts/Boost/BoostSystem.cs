using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace ErrorSpace
{
    public class BoostSystem : MonoBehaviour
    {
        public static UnityEvent<Boost> OnBoostPickedUp { get; private set; } = new();
        public static UnityEvent<int> OnLevelUp { get; private set; } = new();
        
        [SerializeField] private WorldBoost experienceBoostPrefab;
        [SerializeField] private WorldBoost speedBoostPrefab;
        [SerializeField] private WorldBoost healthBoostPrefab;
        
        [SerializeField] private int experiencePoolStartingAmount;
        [SerializeField] private int otherBoostPoolStartingAmount;
        
        private List<WorldBoost> _experiencePool;
        private List<WorldBoost> _speedPool;
        private List<WorldBoost> _healthPool;

        
        private List<Boost> _boosts = new();

        private void Start()
        {
            _experiencePool = new List<WorldBoost>(experiencePoolStartingAmount * 2);
            _speedPool = new List<WorldBoost>(otherBoostPoolStartingAmount * 2);
            _healthPool = new List<WorldBoost>(otherBoostPoolStartingAmount * 2);
            
            
            //no time for DRY
            for (int i = 0; i < experiencePoolStartingAmount; i++)
            {
                var exp = Instantiate(experienceBoostPrefab, this.transform);
                exp.gameObject.SetActive(false);
                _experiencePool.Add(exp);
            }
            
            for (int i = 0; i < otherBoostPoolStartingAmount; i++)
            {
                var speed = Instantiate(speedBoostPrefab, this.transform);
                speed.gameObject.SetActive(false);
                _speedPool.Add(speed);
            }
            
            for (int i = 0; i < otherBoostPoolStartingAmount; i++)
            {
                var health = Instantiate(healthBoostPrefab, this.transform);
                health.gameObject.SetActive(false);
                _healthPool.Add(health);
            }
            
            OnBoostPickedUp.AddListener(AddBoost);
        }

        public void Initialize()
        {
            _boosts.Clear();
        }

        public void SpawnRandomBoost(Vector3 position)
        {
            //https://music.youtube.com/watch?v=3A2x-7HawDc&list=RDAMVMA0Kk0zcuAy8
            var exp = _experiencePool.FirstOrDefault(e => !e.gameObject.activeSelf);
            if (exp == null)
            {
                exp = Instantiate(experienceBoostPrefab, this.transform);
                _experiencePool.Add(exp);
            }
            exp.transform.position = position;
            exp.gameObject.SetActive(true);
        }
        
        public void AddBoost(Boost boost)
        {
            if (!Mathf.Approximately(boost.Duration, -1))
                _boosts.Add(boost);
            
            var oldLevel = PlayerSystem.PlayerStats.Level;
            foreach (var statValue in boost.Stats)
            {
                float value = statValue.value;
                
                if (statValue.stat == Stats.Experience)
                    value *= PlayerSystem.PlayerStats.Stats[Stats.ExperienceMultiplier];
                
                PlayerSystem.PlayerStats.Stats[statValue.stat] += value;
            }
            var newLevel = PlayerSystem.PlayerStats.Level;
            
            if (newLevel > oldLevel)
                OnLevelUp.Invoke(newLevel - oldLevel);
        }

        public void RemoveBoost(Boost boost)
        {
            _boosts.Remove(boost);
            foreach (var statValue in boost.Stats)
            {
                PlayerSystem.PlayerStats.Stats[statValue.stat] -= statValue.value;
            }
        }
        private void Update()
        {
            //i dont care, its gamejam
            List<Boost> boostsToDelete = new List<Boost>();
            
            foreach (var boost in _boosts)
            {
                boost.Duration -= Time.deltaTime;
                if (boost.Duration <= 0) boostsToDelete.Add(boost);
            }

            foreach (var boost in boostsToDelete)
                RemoveBoost(boost);
        }
    }
}
