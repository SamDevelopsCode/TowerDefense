using System;
using System.Collections;
using System.Collections.Generic;
using _TowerDefense.Towers;
using UnityEngine;


public class Attacker : MonoBehaviour
{
    [SerializeField] private TargetingSystem _targetingSystem;
    [SerializeField] private Tower _tower;
    private TowerStats _towerStats;
    
    private float _shootTimer;

    private GameObject _target;

    public event Action<Tower> shotFired;
    
    
    private void Awake()
    {
        _towerStats = _tower.towerStats;
        _targetingSystem.CurrentTargetSelected += UpdateCurrentTarget;
    }


    private void Update()
    {
        if (!CanShoot())
        {
            DecrementShootCooldown();
            return;
        }
        
        if (_target != null)
        {
            Shoot();
        }
    }

    
    private void DecrementShootCooldown()
    {
        _shootTimer -= Time.deltaTime;
    }


    private void UpdateCurrentTarget(GameObject currentTarget)
    {
        _target = currentTarget;
    }
    
    
    private bool CanShoot()
    {
        if (_shootTimer <= 0)
        {
            _shootTimer = _towerStats.fireCooldown;
           return true;
        }
        
        return false;
    }
    
    
    private void Shoot()
    {
        shotFired?.Invoke(_tower);
        var healthComponent = _target.GetComponent<Health>();
        healthComponent.TakeDamage(_towerStats.damagePerShot);
    }   
}
