using System;
using System.Collections.Generic;
using TowerDefense.Enemies;
using TowerDefense.Managers;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> _listOfEnemiesInWave = new();

    public static EnemyManager Instance;
    
    private EnemyWaveSpawner _enemyWaveSpawner;
    
    
    private void Awake()
    {
        Instance = this;
        _enemyWaveSpawner = transform.parent.GetComponent<EnemyWaveSpawner>();
    }


    public void AddEnemyToList(GameObject enemy)
    {
        _listOfEnemiesInWave.Add(enemy);
    }


    public void DeleteEnemyFromList(GameObject enemy)
    {
        _listOfEnemiesInWave.Remove(enemy);

        if (_listOfEnemiesInWave.Count == 0)
        {
            if (_enemyWaveSpawner.CurrentWaveNumber == _enemyWaveSpawner.EnemyWaves.Count)
            {
                GameManager.Instance.UpdateGameState(GameState.Victory);
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.TowerPlacement);
            }
        }
    }
}
