using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ErrorSpace
{
    public class EnemySystem : MonoBehaviour
    {
        [SerializeField] private List<Enemy> enemyPrefabs;
        [SerializeField] private List<Collider2D> spawnLocations;
        [SerializeField] private Transform spawnRoot;
        
        public void Start()
        {
            for (int index = 0; index < 200; index++)
                GenerateWave(); 
        }

        public void GenerateWave()
        {
            int randomSpawnerIndex = Random.Range(0, spawnLocations.Count);
            
            var spawnLocation = RandomPointInBounds(spawnLocations[randomSpawnerIndex].bounds);
            
            Enemy spawnedEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], spawnLocation, Quaternion.identity, spawnRoot);
            spawnedEnemy.Initialise();
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
