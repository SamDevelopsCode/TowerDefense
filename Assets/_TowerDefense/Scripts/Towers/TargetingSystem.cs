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
        
        private float _maxAttackRange;

        public event Action<GameObject> CurrentTargetSelected;
        
        
        private void Awake()
        {
            _tower = GetComponent<Tower>();
            _towerData = _tower.towerData;
            _maxAttackRange = _towerData.range;
        }

        
        private void OnTriggerEnter(Collider collision)
        {
            Debug.Log(collision);
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
            
            GameObject closestTarget = null;
            
        
            foreach (var enemy in _possibleTargets)
            {
                var distanceToTarget = Vector3.Distance(transform.position, enemy.transform.position);
        
                if (distanceToTarget <= _maxAttackRange)
                {
                    closestTarget = enemy;
                }
            }
            _currentTarget = closestTarget;
            CurrentTargetSelected?.Invoke(_currentTarget);
        }

    
        private void AimWeapon()
        {
            _towerPivot.LookAt(_currentTarget.transform.position);
        }
    }
}


