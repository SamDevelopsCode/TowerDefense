using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TargetingWeapon : MonoBehaviour
{
    [SerializeField] private Transform _turretSupport;
    [SerializeField] private Transform raycastStartPos;

    [SerializeField] private float damage = 5f;
    [SerializeField] private float secondsTillNextShot = 3f;
    
    private GameObject _target;

    private void Start()
    {
        _target = FindObjectOfType<Health>().gameObject;
        InvokeRepeating("Shoot", secondsTillNextShot, secondsTillNextShot);
    }

    private void Update()
    {
        AimWeapon();
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
            healthComponent.TakeDamage(damage);
        }
    }
    
    
    private void RaycastTowardsEnemy()
    {
        Debug.Log("Shooting now");
        var distanceToEnemy = Vector3.Distance(transform.position, _target.transform.position);
        var directionToEnemy = _target.transform.position - transform.position;
        if (Physics.Raycast(raycastStartPos.position, directionToEnemy + new Vector3(0, .5f,0), out RaycastHit raycastHit, distanceToEnemy))
        {
            var objectHit = raycastHit.collider.gameObject;
            Debug.Log("hit: " + objectHit.name);
            if (objectHit.transform.parent.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage);
            }
        }
    }
}
