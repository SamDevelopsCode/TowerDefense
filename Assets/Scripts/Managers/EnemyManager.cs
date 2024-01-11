using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TowerDefense.Enemies;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace TowerDefense.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<Wave> enemyWaves= new List<Wave>();
    
        private bool _enemiesCurrentlySpawning;
        private int _currentWaveNumber = 0;

        // private IEnumerator SpawnWave(int waveNumber)
        // {
        //     var currentWave = _enemyWaves[waveNumber];
        //     var enemiesSpawned = 0;
        //     
        //     while (enemiesSpawned < currentWave.numberOfEnemies)
        //     {
        //         var enemyInstance = Instantiate(currentWave.enemyPrefab, transform);
        //         enemiesSpawned += 1;
        //         yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        //     }
        //     _currentWaveNumber += 1;
        // }

        // public void SpawnNextWave(int waveNumber)
        // {
        //     StartCoroutine(SpawnWave(_currentWaveNumber));
        // }
        
    }
}
