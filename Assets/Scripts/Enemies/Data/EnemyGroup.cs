using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Enemies
{
    [CreateAssetMenu(fileName = "EnemyGroup00", menuName = "Enemy / Create Enemy Group") ]
    public class EnemyGroup : ScriptableObject
    {
        [SerializeField] public List<GameObject> enemyPrefabs;
        [SerializeField] public int numberOfEnemies;
        [SerializeField] public float timeBetweenSpawns;
    }
}