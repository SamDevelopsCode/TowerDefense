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

    
        private void Awake()
        {
            Instance = this;
        }

    
        public void UpdateGoldBalanceUI(int currentGoldBalance)
        {
            _goldBalance.text = currentGoldBalance.ToString();
        }
    
    
        public void UpdateBaseHealthUI(int currentBaseHealth, int maxBaseHealth)
        {
            _baseHealthAmount.text = currentBaseHealth.ToString() + "/" + maxBaseHealth.ToString();
        }
    
    
        public void UpdateWaveNumberUI()
        {
            // TODO create waves spawning and update the current wave number here
        }
    }
}
