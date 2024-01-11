using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Enemies
{
    [CreateAssetMenu(fileName = "EnemyWave", menuName = "Enemy / Create New Wave") ]
    public class Wave : ScriptableObject
    {
        [SerializeField] public List<EnemyWaveComponent> enemyWaves = new List<EnemyWaveComponent>();
    }
}