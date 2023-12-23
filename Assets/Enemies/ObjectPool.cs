using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _objectPoolSize = 5;
    [SerializeField] private int _maxEnemySpawnNumber = 10;
    [SerializeField] private float _timeBetweenSpawn = 1.0f;
    
    private GameObject[] _objectPool;
    
    private int _enemiesSpawned = 0;

    private void Awake()
    {
        PopulateObjectPool();
    }


    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    
    
    private void PopulateObjectPool()
    {
        _objectPool = new GameObject[_objectPoolSize];

        for (int i = 0; i < _objectPool.Length; i++)
        {
            _objectPool[i] = Instantiate(_enemyPrefab, transform);
            _objectPool[i].SetActive(false);
        }
    }

    
    private IEnumerator SpawnEnemies()
    {
        while (_enemiesSpawned < _maxEnemySpawnNumber)
        {
            EnableObjectInPool();
            
            _enemiesSpawned += 1;
            yield return new WaitForSeconds(_timeBetweenSpawn);
        }
    }
    

    private void EnableObjectInPool()
    {
        foreach (var poolObject in _objectPool)
        {
            if (!poolObject.activeInHierarchy)
            {
                poolObject.SetActive(true);
                return;
            }
        }
    }
}
