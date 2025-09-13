using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ErrorSpace
{
    public class BoostSystem : MonoBehaviour
    {
        public static UnityEvent<Boost> OnBoostPickedUp { get; private set; } = new();
        public static UnityEvent<int> OnLevelUp { get; private set; } = new();
        
        private List<Boost> _boosts = new();

        private void Start()
        {
            OnBoostPickedUp.AddListener(AddBoost);
        }

        public void Initialize()
        {
            _boosts.Clear();
        } 
        public void AddBoost(Boost boost)
        {
            if (!Mathf.Approximately(boost.Duration, -1))
                _boosts.Add(boost);
            
            var oldLevel = PlayerSystem.PlayerStats.Level;
            foreach (var statValue in boost.Stats)
            {
                PlayerSystem.PlayerStats.Stats[statValue.stat] += statValue.value;
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
                if (Mathf.Approximately(boost.Duration, -1)) continue;
                boost.Duration -= Time.deltaTime;
                if (boost.Duration <= 0) boostsToDelete.Add(boost);
            }

            foreach (var boost in boostsToDelete)
                RemoveBoost(boost);
        }
    }
}
