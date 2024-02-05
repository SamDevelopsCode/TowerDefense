using System;
using _TowerDefense.Towers;
using TMPro;
using TowerDefense.Managers;
using UnityEngine;

namespace TowerDefense.Tower
{
    public class CoreGameUI : MonoBehaviour
    {
        public static CoreGameUI Instance;
    
        [SerializeField] private TextMeshProUGUI _goldBalance;
        [SerializeField] private TextMeshProUGUI _baseHealthAmount;
        [SerializeField] private TextMeshProUGUI _waveNumber;

        [SerializeField] private TowerManager _towerManager;
        [SerializeField] private TowerStatsUI _towerStats;
        [SerializeField] private GameObject _towerStatsUI;
        [SerializeField] private GameObject _waveStatsUI;
    
        
        private void Awake()
        {
            Instance = this;
        }

        
        private void OnEnable()
        {
            _towerManager.TowerSelected += OnTowerSelected;
            _towerManager.TowerPlacementFailed += SetWaveSpawnsCurrentView;
            _towerManager.TowerPlacementSucceeded += SetWaveSpawnsCurrentView;
            GameManager.OnGameStateChanged += OnGameStateChanged;
        }


        private void OnDisable()
        {
            _towerManager.TowerSelected -= OnTowerSelected;
            _towerManager.TowerPlacementFailed -= SetWaveSpawnsCurrentView;
            _towerManager.TowerPlacementSucceeded -= SetWaveSpawnsCurrentView;
            GameManager.OnGameStateChanged -= OnGameStateChanged;
        }


        private void OnGameStateChanged(GameState state)
        {
            if (state == GameState.EnemyWave)
            {
                SetWaveSpawnsCurrentView();
            }
        }


        private void OnTowerSelected(TowerData towerData)
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
    }
}
