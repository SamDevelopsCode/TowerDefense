using UnityEngine;

namespace TowerDefense.Enemies.Data
{
    [CreateAssetMenu(fileName = "EnemyGroup", menuName = "Enemy / Create Enemy Group") ]
    public class EnemyGroup : ScriptableObject
    {
        public GameObject enemyPrefab;
        public int numberOfEnemies;
        public float timeToStartSpawning;
        public float timeBetweenSpawns;
    }
}