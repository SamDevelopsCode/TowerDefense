using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _maxEnemySpawnNumber = 10;
    [SerializeField] private float _timeBetweenSpawn = 1.0f;
    
    private int _enemiesSpawned = 0;
    private bool _enemiesCurrentlySpawning;


    public void SpawnEnemyWave()
    {
        if (!_enemiesCurrentlySpawning)
        {
            StartCoroutine(SpawnEnemies());
        }
    }
    
    private IEnumerator SpawnEnemies()
    {
        GameManager.Instance.UpdateGameState(GameState.EnemyWave);
        _enemiesCurrentlySpawning = true;
        
        while (_enemiesSpawned < _maxEnemySpawnNumber)
        {
            var enemyInstance = Instantiate(_enemyPrefab, transform);
            
            _enemiesSpawned += 1;
            yield return new WaitForSeconds(_timeBetweenSpawn);
        }
    }
    
    
    
}
