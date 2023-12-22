using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform _turretSupport;
    [SerializeField] private Transform _raycastStartPos;

    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _secondsTillNextShot = 3f;

    [SerializeField] private float _turretRange = 3f;
    
    private GameObject _target;

    private void Start()
    {
        _target = FindObjectOfType<Health>().gameObject;
        InvokeRepeating("Shoot", _secondsTillNextShot, _secondsTillNextShot);
    }

    private void Update()
    {
        AimWeapon();
        FindClosestTarget();
    }
    

    private void FindClosestTarget()
    {
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
    
    
    private void RaycastTowardsEnemy()
    {
        Debug.Log("Shooting now");
        var distanceToEnemy = Vector3.Distance(transform.position, _target.transform.position);
        var directionToEnemy = _target.transform.position - transform.position;
        if (Physics.Raycast(_raycastStartPos.position, directionToEnemy + new Vector3(0, .5f,0), out RaycastHit raycastHit, distanceToEnemy))
        {
            var objectHit = raycastHit.collider.gameObject;
            Debug.Log("hit: " + objectHit.name);
            if (objectHit.transform.parent.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }
        }
    }
}
