using UnityEngine;

namespace TowerDefense.Enemies.Data
{
    [CreateAssetMenu(fileName = "EnemyGroup", menuName = "Enemy / Create Enemy Group") ]
    public class EnemyGroup : ScriptableObject
    {
        [SerializeField] public GameObject enemyPrefab;
        [SerializeField] public int numberOfEnemies;
        [SerializeField] public float timeToStartSpawning;
        [SerializeField] public float timeBetweenSpawns;
    }
}