using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Enemies
{
    [CreateAssetMenu(fileName = "EnemyWave_00", menuName = "Enemy / Create Enemy Wave") ]
    public class EnemyWaveComponent : ScriptableObject
    {
        [SerializeField] public GameObject enemyPrefab;
        [SerializeField] public int numberOfEnemies;
        [SerializeField] public float timeBetweenSpawns;
    }
}