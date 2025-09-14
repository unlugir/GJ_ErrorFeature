using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace ErrorSpace
{
    public class EnemySystem : MonoBehaviour
    {
        public static UnityEvent<Enemy> OnDeath { get; private set; } = new();
        [SerializeField] private List<Enemy> enemyPrefabs;
        [SerializeField] private List<Collider2D> spawnLocations;
        [SerializeField] private Transform spawnRoot;
        //You can add more than 200 as there was 0 lag in WebGl build from yesterday tests.
        //I set 200 as max because it looks too much when you have more than 500 at one time.
        [Range(100, 1000)]
        [SerializeField] private int maxEnemyCount = 200;
        [SerializeField] private int maxEnemyInWaveCount = 50;

        private int currentEnemyCount = 0;
        private bool shouldSpawn = true;
        
        public void Start()
        {
            GenerateEnemies();
        }

        public async void GenerateEnemies()
        {
            while (shouldSpawn)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
                GenerateWave();
            }
        }
        

        public void GenerateWave()
        {
            Debug.Log(currentEnemyCount);
            for (int index = 0; index < maxEnemyInWaveCount; index++)
            {
                if(maxEnemyCount <= currentEnemyCount) return;
                
                int randomSpawnerIndex = Random.Range(0, spawnLocations.Count);
            
                var spawnLocation = RandomPointInBounds(spawnLocations[randomSpawnerIndex].bounds);
            
                Enemy spawnedEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], spawnLocation, Quaternion.identity, spawnRoot);
                spawnedEnemy.Initialise();
                
                spawnedEnemy.OnDeath.AddListener(() => { currentEnemyCount--;});
                currentEnemyCount++;
            }
        }
        
        public static Vector3 RandomPointInBounds(Bounds bounds) {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
    
    
}
