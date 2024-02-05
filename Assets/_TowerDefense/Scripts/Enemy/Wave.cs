using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Enemies.Data
{
    [CreateAssetMenu(fileName = "EnemyWave", menuName = "Enemy / Create New Wave") ]
    public class Wave : ScriptableObject
    {
        [SerializeField] public List<EnemyGroup> enemyGroups = new();
    }
}