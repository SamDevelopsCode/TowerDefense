using System;
using TowerDefense.Managers;
using UnityEngine;

namespace TowerDefense.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int _resourceAmountDroppedOnDeath = 25;

        [SerializeField] private int _damageToBase = 5;
        public int DamageToBase
        {
            get => _damageToBase;
        }
        
        private bool _killedByTower;
        public bool KilledByTower
        {
            get => _killedByTower;
            set
            {
                _killedByTower = value;
                StartDeathSequence();
            }
        }

        public event Action<GameObject> OnEnemyReachedPlayerBase; 
        
        [SerializeField] private Health _healthComponent;
        private CurrencyManager _currencyManager;

        
        private void Start()
        {
            EnemyManager.Instance.AddEnemyToList(gameObject);
            _healthComponent.OnEnemyDied += StartDeathSequence;
            _currencyManager = FindObjectOfType<CurrencyManager>();
        }

    
        private void DropResources()
        {
            _currencyManager.AddToBalance(_resourceAmountDroppedOnDeath);
        }

        
        private void StartDeathSequence()
        {
            EnemyManager.Instance.DeleteEnemyFromList(gameObject);
            
            if (_killedByTower)
            {
                DropResources();
            }
            OnEnemyReachedPlayerBase?.Invoke(this.gameObject);
            // show vfx
            // possible sound?
            // Destroy(gameObject);
        }
    }
}
