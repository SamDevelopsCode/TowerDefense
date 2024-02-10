using System;
using System.Collections;
using System.Collections.Generic;
using _TowerDefense.Towers;
using UnityEngine;


public class Attacker : MonoBehaviour
{
    private TargetingSystem _targetingSystem;
    private Tower _tower;
    private TowerData _towerData;
    
    private float _shootTimer;

    private GameObject _target;
    
    
    private void Awake()
    {
        _tower = GetComponent<Tower>();
        _towerData = _tower.towerData;
        _targetingSystem = GetComponent<TargetingSystem>();
        _targetingSystem.CurrentTargetSelected += UpdateCurrentTarget;
    }


    private void Update()
    {
        DecrementShootCooldown();
        
        if (!CanShoot()) return;
        
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
            _shootTimer = _towerData.shotsPerSecond;
           return true;
        }
        
        return false;
    }
    
    private void Shoot()
    {
        var healthComponent = _target.GetComponent<Health>();
        healthComponent.TakeDamage(_towerData.damagePerShot);
    }   
}
