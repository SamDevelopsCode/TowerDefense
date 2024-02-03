using _TowerDefense.Towers;
using TMPro;
using UnityEngine;

namespace TowerDefense.Tower
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
    
        [SerializeField] private TextMeshProUGUI _goldBalance;
        [SerializeField] private TextMeshProUGUI _baseHealthAmount;
        [SerializeField] private TextMeshProUGUI _waveNumber;

        [SerializeField] private TowerManager _towerManager;
        
        private void Awake()
        {
            Instance = this;
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
