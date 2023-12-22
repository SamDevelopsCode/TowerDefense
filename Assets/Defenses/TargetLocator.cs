using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform _turretSupport;

    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _secondsTillNextShot = 3f;

    [SerializeField] private float _turretRange = 3f;
    
    private GameObject _target;

    private void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }
    

    private void FindClosestTarget()
    {
        // Physics.SphereCast();
        //get a list of targets
        //find all the targets in range
        //loop through each target in range and see which one is closer
        //set closest one to the target until they leave range
        //this should repeat
    }

    private void AimWeapon()
    {
        _turretSupport.LookAt(_target.transform.position);
    }

    private void Shoot()
    {
        if (_target != null)
        {
            Debug.Log("Shooting now");
            var healthComponent = _target.GetComponent<Health>();
            healthComponent.TakeDamage(_damage);
        }
    }

   
}


