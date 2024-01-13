using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TowerDefense.Enemies;
using TowerDefense.Tower;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace TowerDefense.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<Wave> enemyWaves = new List<Wave>();
    
        private bool _enemiesCurrentlySpawning;
        private bool _waveCanSpawn;
        private int _currentWaveNumber = 0;
        

        private void Start()
        {
            UIManager.Instance.UpdateWaveNumberUI(_currentWaveNumber, enemyWaves.Count);
        }

        
        private IEnumerator SpawnWave(int waveNumber)
        {
            var currentWave = enemyWaves[waveNumber];
            
            foreach(EnemyGroup group in currentWave.enemyGroups)
            {
                for (int i = 0; i < group.numberOfEnemies; i++)
                {
                    var enemyInstance = Instantiate(group.enemyPrefab, transform);
                    yield return new WaitForSeconds(group.timeBetweenSpawns);
                }

                yield return new WaitForSeconds(.1f);
            }
            _currentWaveNumber += 1;
        }
        
        
        public void SpawnNextWave(int waveNumber)
        {
            if (!_waveCanSpawn) return;
            
            GameManager.Instance.UpdateGameState(GameState.EnemyWave);
            UIManager.Instance.UpdateWaveNumberUI(_currentWaveNumber + 1, enemyWaves.Count);
            StartCoroutine(SpawnWave(_currentWaveNumber));
        }
        
    }
}
