using System.Collections.Generic;
using UnityEngine;

namespace ErrorSpace
{
    [RequireComponent(typeof(EnemyPathfinding))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private List<Sprite> enemyVariation;
        [SerializeField] private SpriteRenderer enemySprite;
        [SerializeField] private EnemyPathfinding enemyPathfinding;
        
        public void Initialise()
        {
            //enemyPathfinding.OnDestinationUpdated
            enemySprite.sprite = enemyVariation[Random.Range(0, enemyVariation.Count)];
        }

        private void SwapSprite()
        {
            
        }
    }
}
