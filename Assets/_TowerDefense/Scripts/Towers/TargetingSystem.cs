using System;
using System.Collections.Generic;
using TowerDefense.Enemies;
using TowerDefense.Managers;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace _TowerDefense.Towers
{
    public class TargetingSystem : MonoBehaviour
    {
        [SerializeField] private Tower _tower;
        private TowerStats _towerStats;
        [SerializeField] private Transform _towerPivot;
        [SerializeField] private SphereCollider _sphereCollider;
        
        [SerializeField] private List<GameObject> _possibleTargets;
        private GameObject _currentTarget;

        public enum TargetingType
        {
            CLOSEST,
            HIGHESTHEALTH,
            LOWESTHEALTH,
        }

        public TargetingType currentTargetingType = TargetingType.CLOSEST;

        public event Action<GameObject> CurrentTargetSelected;
        
        
        private void Awake()
        {
            _towerStats = _tower.towerStats;
            _sphereCollider.radius = _towerStats.range;
        }
        
        
        private void OnEnable()
        {
            GameManager.GameStateChanged += OnGameStateChanged;
        }

        
        private void OnDisable()
        {
            GameManager.GameStateChanged -= OnGameStateChanged;
        }
        
        
        private void OnTriggerEnter(Collider collision)
        {
            _possibleTargets.Add(collision.gameObject);
        }

        
        private void OnTriggerExit(Collider collision)
        {
            _possibleTargets.Remove(collision.gameObject);
        }
        
        
        private void Update()
        {
            SelectCurrentTarget();
            
            if (_currentTarget != null)
            {
                AimWeapon();
            }
        }

        
        private void SelectCurrentTarget()
        {
            if (_possibleTargets.Count == 0)
            {
                _currentTarget = null;
                CurrentTargetSelected?.Invoke(_currentTarget);
                return;
            }
            
            if (currentTargetingType == TargetingType.CLOSEST)
            {
               _currentTarget = CalculateClosestTarget();
            }
            else if (currentTargetingType == TargetingType.HIGHESTHEALTH)
            { 
                _currentTarget = CalculateHighestHealthTarget();
            }
            else if (currentTargetingType == TargetingType.LOWESTHEALTH)
            {
                _currentTarget = CalculateLowestHealthTarget();
            }
            
            CurrentTargetSelected?.Invoke(_currentTarget);
        }
        
        
        private GameObject CalculateClosestTarget()
        {
            GameObject closestTarget = null;
            float smallestEnemyDistance = Mathf.Infinity;
            
            foreach (var enemy in _possibleTargets)
            {
                if (enemy == null) continue;
                
                float distanceToTarget = Vector3.Distance(transform.position, enemy.transform.position);
            
                if (distanceToTarget < smallestEnemyDistance)
                {
                    closestTarget = enemy;
                }
            }
            return closestTarget;
        }

        
        private GameObject CalculateHighestHealthTarget()
        {
            GameObject highestHealthTarget = null;
            float highestHealth = 0.0f;
            
            foreach (var enemy in _possibleTargets)
            {
                float currentEnemyHealth = enemy.GetComponent<Health>().CurrentHealth;
                    
                if (currentEnemyHealth > highestHealth)
                {
                    highestHealth = currentEnemyHealth;
                    highestHealthTarget = enemy;
                }
            }
            return highestHealthTarget;
        }
        
        
        private GameObject CalculateLowestHealthTarget()
        {
            GameObject lowestHealthTarget = null;
            float lowestHealth = Mathf.Infinity;
            
            foreach (var enemy in _possibleTargets)
            {
                float currentEnemyHealth = enemy.GetComponent<Health>().CurrentHealth;
                    
                if (currentEnemyHealth < lowestHealth)
                {
                    lowestHealth = currentEnemyHealth;
                    lowestHealthTarget = enemy;
                }
            }
            return lowestHealthTarget;
        }

        
        // Toggles the towers target detection colliders so they won't interfere with the placing of
        // other towers nearby with mouse detection
        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.TowerPlacement) SetSphereColliderActive(false);
            else if (gameState == GameState.EnemyWave) SetSphereColliderActive(true);
        }
        
        
        private void SetSphereColliderActive(bool shouldEnable)
        {
            _sphereCollider.enabled = shouldEnable;
        }
        
        
        private void AimWeapon()
        {
            _towerPivot.LookAt(_currentTarget.transform.position);
        }
    }
}