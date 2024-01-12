using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TowerDefense.Enemies;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace TowerDefense.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<Wave> enemyWaves = new List<Wave>();
    
        private bool _enemiesCurrentlySpawning;
        private int _currentWaveNumber = 0;

        private void Start()
        {
            _currentWaveNumber = 0;
        }

        
        private IEnumerator SpawnWave(int waveNumber)
        {
            var currentWave = enemyWaves[waveNumber];
            
            foreach(EnemyGroup group in currentWave.enemyGroups)
            {
                for (int i = 0; i < group.numberOfEnemies; i++)
                {
                    var enemyInstance = Instantiate(group.enemyPrefabs[i], transform);
                    yield return new WaitForSeconds(group.timeBetweenSpawns);
                }

                yield return new WaitForSeconds(3.0f);
            }
            _currentWaveNumber += 1;
        }
        
        
        public void SpawnNextWave(int waveNumber)
        {
            StartCoroutine(SpawnWave(_currentWaveNumber));
        }
        
    }
}
