using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Enemies.Data;
using TowerDefense.Managers;
using TowerDefense.Tower;
using UnityEngine;

namespace TowerDefense.Enemies
{
    public class EnemyWaveSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _enemyParent;
        [SerializeField] private List<Wave> _enemyWaves = new();

        public List<Wave> EnemyWaves
        {
            get => _enemyWaves;
        }
        
        private bool _enemiesCurrentlySpawning;
        private bool _waveCanSpawn = true;
        
        private int _currentWaveNumber = 0;
        public int CurrentWaveNumber
        {
            get => _currentWaveNumber;
        }

        public event Action<int, Wave> OnNextWaveSpawned;

        
        private void OnEnable()
        {
            GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        }
        
        
        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        }
        

        private void Start()
        {
            CoreGameUI.Instance.UpdateWaveNumberUI(_currentWaveNumber, _enemyWaves.Count);
        }

        
        private IEnumerator SpawnWave(int waveNumber)
        {
            var currentWave = _enemyWaves[waveNumber];
            
            foreach(EnemyGroup group in currentWave.enemyGroups)
            {
                for (int i = 0; i < group.numberOfEnemies; i++)
                {
                    var enemyInstance = Instantiate(group.enemyPrefab, _enemyParent.transform);
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
            CoreGameUI.Instance.UpdateWaveNumberUI(_currentWaveNumber + 1, _enemyWaves.Count);
            StartCoroutine(SpawnWave(_currentWaveNumber));
        }
        
        
        private void GameManagerOnGameStateChanged(GameState state)
        {
            _waveCanSpawn = state == GameState.TowerPlacement;
            
            if (state == GameState.TowerPlacement)
            {
                OnNextWaveSpawned?.Invoke(_currentWaveNumber, _enemyWaves[_currentWaveNumber]);
            }
        }
    }
}
