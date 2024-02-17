using System;
using _TowerDefense.Towers;
using TMPro;
using TowerDefense.Enemies;
using TowerDefense.Enemies.Data;
using TowerDefense.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace TowerDefense.Tower
{
    public class CoreGameUI : MonoBehaviour
    {
        public static CoreGameUI Instance;
    
        [SerializeField] private TextMeshProUGUI _goldBalance;
        [SerializeField] private TextMeshProUGUI _baseHealthAmount;
        [SerializeField] private TextMeshProUGUI _waveNumber;

        [SerializeField] private TowerManager _towerManager;
        [SerializeField] private EnemyWaveSpawner _enemyWaveSpawner;
        [SerializeField] private TowerStatsUI _towerStats;
        [SerializeField] private GameObject _towerStatsUI;
        [SerializeField] private GameObject _waveSpawnsUI;
        [SerializeField] private GameObject _waveStatsUI;
        [SerializeField] private TextMeshProUGUI _waveTitle;

        [SerializeField] private GameObject _waveSpawnPrefab;
        
        private const string CurrentWaveText = "Current Wave";
        private const string NextWaveText = "Next Wave";
    
        
        private void Awake()
        {
            Instance = this;
        }

        
        private void OnEnable()
        {
            _towerManager.TowerSelected += OnTowerSelected;
            _towerManager.TowerPlacementFailed += SetWaveSpawnsCurrentView;
            _towerManager.TowerPlacementSucceeded += SetWaveSpawnsCurrentView;
            _enemyWaveSpawner.OnNextWaveSpawned += SetWaveSpawnsData;
            GameManager.GameStateChanged += GameStateChanged;
        }
        

        private void OnDisable()
        {
            _towerManager.TowerSelected -= OnTowerSelected;
            _towerManager.TowerPlacementFailed -= SetWaveSpawnsCurrentView;
            _towerManager.TowerPlacementSucceeded -= SetWaveSpawnsCurrentView;
            _enemyWaveSpawner.OnNextWaveSpawned -= SetWaveSpawnsData;
            GameManager.GameStateChanged -= GameStateChanged;
        }

        
        private void SetWaveSpawnsData(int currentWaveNumber, Wave enemyWave)
        {
            if (_waveSpawnsUI.transform.childCount != 0)
            {
                for (int i = 0; i < _waveSpawnsUI.transform.childCount; i++)
                {
                    Destroy(_waveSpawnsUI.transform.GetChild(i).gameObject);
                }
            } 
            
            foreach (var group in enemyWave.enemyGroups) 
            {
                var spawnInfoInstance = Instantiate(_waveSpawnPrefab, _waveSpawnsUI.transform);
                SpawnInfo spawnInfo = spawnInfoInstance.GetComponent<SpawnInfo>();
                spawnInfo.SetSpawnInfoUI(group.enemyPrefab.GetComponent<Enemy>().icon, group.numberOfEnemies);
            }
        }

        
        private void GameStateChanged(GameState state)
        {
            if (state == GameState.EnemyWave)
            {
                SetWaveSpawnsCurrentView();
                SetWaveTitleToCurrentWave();
            }
            else 
            {
                SetWaveTitleToNextWave();
            }
        }

        
        public void OnTowerSelected(TowerData towerData)
        {
            SetTowerStatsToCurrentView();
            _towerStats.SetTowerStatsUIData(towerData);
        }


        private void SetTowerStatsToCurrentView()
        {
            _towerStatsUI.SetActive(true);
            _waveStatsUI.SetActive(false);
        }
        
        
        public void SetWaveSpawnsCurrentView()
        {
            _towerStatsUI.SetActive(false);
            _waveStatsUI.SetActive(true);
        }
        
        
        public void UpdateGoldBalanceUI(int currentGoldBalance)
        {
            _goldBalance.text = $"{currentGoldBalance}";
        }
    
    
        public void UpdateBaseHealthUI(int currentBaseHealth, int maxBaseHealth)
        {
            _baseHealthAmount.text = $"{currentBaseHealth} / {maxBaseHealth}";
        }
    
    
        public void UpdateWaveNumberUI(int currentWave, int maxWaves)
        {
            _waveNumber.text = $"{currentWave} / {maxWaves}";
        }


        private void SetWaveTitleToCurrentWave()
        {
            _waveTitle.text = CurrentWaveText;
        }


        private void SetWaveTitleToNextWave()
        {
            _waveTitle.text = NextWaveText;
        }
    }
}
