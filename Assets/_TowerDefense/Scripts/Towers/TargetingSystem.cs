using System;
using System.Collections.Generic;
using TowerDefense.Enemies;
using UnityEditor;
using UnityEngine;

namespace _TowerDefense.Towers
{
    public class TargetingSystem : MonoBehaviour
    {
        [SerializeField] private Transform _towerPivot;

        [SerializeField] private List<GameObject> _possibleTargets;
        private GameObject _currentTarget;
        
        private Tower _tower;
        private TowerData _towerData;
        
        // private float _maxAttackRange;
        
        private bool _should_calculate_closest_enemy = true;
        private bool _should_calculate_highest_health_enemy;
        private bool _should_calculate_lowest_health_enemy;

        public event Action<GameObject> CurrentTargetSelected;
        
        
        private void Awake()
        {
            _tower = GetComponent<Tower>();
            _towerData = _tower.towerData;
            // _maxAttackRange = _towerData.range;
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
                Debug.Log("List of enemies is empty.");
                _currentTarget = null;
                return;
            }
            
            if (_should_calculate_closest_enemy)
            {
               _currentTarget = CalculateClosestTarget();
            }
            else if (_should_calculate_highest_health_enemy)
            { 
                _currentTarget = CalculateHighestHealthTarget();
            }
            else if (_should_calculate_lowest_health_enemy)
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

        
        private void AimWeapon()
        {
            _towerPivot.LookAt(_currentTarget.transform.position);
        }
    }
}


