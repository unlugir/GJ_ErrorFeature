using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace ErrorSpace
{
    public class EnemySystem : MonoBehaviour
    {
        public static UnityEvent<Enemy> OnDeath { get; private set; } = new();
        [SerializeField] private List<Enemy> enemyPrefabs;
        [SerializeField] private Transform spawnRoot;
        //You can add more than 200 as there was 0 lag in WebGl build from yesterday tests.
        //I set 200 as max because it looks too much when you have more than 500 at one time.
        [Range(100, 1000)]
        [SerializeField] private int maxEnemyCount = 200;
        [SerializeField] private int maxEnemyInWaveCount = 50;
        [SerializeField] private float waveTimeDelay = 10f;
        [SerializeField] private Tilemap backgroundTilemap;

        private int currentEnemyCount = 0;
        private bool shouldSpawn = true;
        
        public void Start()
        {
            GenerateEnemies();
        }

        private async void GenerateEnemies()
        {
            while (shouldSpawn)
            {
                GenerateWave();
                await UniTask.Delay(TimeSpan.FromSeconds(waveTimeDelay), ignoreTimeScale: false);
            }
        }
        

        private void GenerateWave()
        {
            Debug.Log(currentEnemyCount);
            for (int index = 0; index < maxEnemyInWaveCount; index++)
            {
                if(maxEnemyCount <= currentEnemyCount) return;

                var randomPoint = TryGetRandomPointInBounds();
                if(randomPoint.Equals(Vector3.zero)) continue;
                
                Enemy spawnedEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], randomPoint, Quaternion.identity, spawnRoot);
                spawnedEnemy.Initialise();
                
                spawnedEnemy.OnDeath.AddListener(() => { currentEnemyCount--;});
                currentEnemyCount++;
            }
        }

        private Vector3 TryGetRandomPointInBounds()
        {
            for (int index = 0; index < 10; index++)
            {
                Bounds local = backgroundTilemap.localBounds;
                Vector3 worldMin = backgroundTilemap.transform.TransformPoint(local.min);
                Vector3 worldMax = backgroundTilemap.transform.TransformPoint(local.max);

                Bounds world = new Bounds();
                world.SetMinMax(Vector3.Min(worldMin, worldMax), Vector3.Max(worldMin, worldMax));

                var randomPoint = GetRandomPointOutsideTheCamera();
                if(world.Contains(randomPoint)) return randomPoint;
            }

            return Vector3.zero;
        }

        private Vector3 GetRandomPointOutsideTheCamera()
        {
            var randomVector = GetRandomPoint();
            randomVector.z = 10f; 
            return Camera.main.ViewportToWorldPoint(randomVector);
        }

        private Vector3 GetRandomPoint()
        {
            float x = Random.Range(-0.8f, 0.8f);
            float y = Random.Range(-0.8f, 0.8f);
            if (x >= 0) x += 1;
            if (y >= 0) y += 1;
            return new Vector3(x, y);
        }
    }
    
    
}
