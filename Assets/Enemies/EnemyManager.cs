using System;
using System.Collections.Generic;
using TowerDefense.Enemies;
using TowerDefense.Managers;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> _listOfEnemiesInWave = new();

    public event Action<GameState> EnemyWaveCompleted;
    
    
    public void AddEnemyToList(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().OnEnemyReachedPlayerBase += DeleteEnemyFromList;
        _listOfEnemiesInWave.Add(enemy);
    }


    private void DeleteEnemyFromList(GameObject enemy)
    {
        _listOfEnemiesInWave.Remove(enemy);

        if (_listOfEnemiesInWave.Count == 0)
        {
            EnemyWaveCompleted?.Invoke(GameState.TowerPlacement);
        }
    }
}
