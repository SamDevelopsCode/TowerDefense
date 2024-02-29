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

        public Sprite icon;
        
        private bool _killedByTower;
        public bool KilledByTower
        {
            set
            {
                _killedByTower = value;
                StartDeathSequence();
            }
        }

        public event Action<GameObject> OnEnemyReachedPlayerBase; 
        
        [SerializeField] private Health _healthComponent;
        private Bank _bank;

        
        private void Start()
        {
            EnemyManager.Instance.AddEnemyToList(gameObject);
            _bank = FindObjectOfType<Bank>();
        }

        
        private void OnEnable()
        {
            _healthComponent.OnEnemyDied += StartDeathSequence;
        }
        
        
        private void OnDisable()
        {
            _healthComponent.OnEnemyDied -= StartDeathSequence;
        }


        private void DropResources()
        {
            _bank.AddToBalance(_resourceAmountDroppedOnDeath);
        }

        
        private void StartDeathSequence()
        {
            EnemyManager.Instance.DeleteEnemyFromList(gameObject);
            
            if (_killedByTower)
            {
                DropResources();
            }
            OnEnemyReachedPlayerBase?.Invoke(gameObject);
        }
    }
}
