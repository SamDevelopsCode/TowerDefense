using TowerDefense.Managers;
using UnityEngine;

namespace TowerDefense.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int _resourceAmountDroppedOnDeath = 25;
        [SerializeField] private int _damageToBase = 5;
    
        [SerializeField] private Health _healthComponent;
        private CurrencyManager _currencyManager;

        public int DamageToBase
        {
            get
            {
                return _damageToBase;
            }
        }
    
    
        private void Start()
        {
            _healthComponent.OnEntityDied += StartDeathSequence;
            _currencyManager = FindObjectOfType<CurrencyManager>();
        }

    
        private void DropResources()
        {
            _currencyManager.AddToBalance(_resourceAmountDroppedOnDeath);
        }


        private void StartDeathSequence()
        {
            DropResources();
            // show vfx
            // possible sound?
            Destroy(gameObject);
        }
    }
}
