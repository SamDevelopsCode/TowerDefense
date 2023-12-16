using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingWeapon : MonoBehaviour
{
    [SerializeField] private Transform support;
    [SerializeField] private Transform target;

    private void Start()
    {
        target = FindObjectOfType<EnemyMovement>().transform;
    }

    private void Update()
    {
        AimWeapon();
    }

    private void AimWeapon()
    {
        support.transform.LookAt(target);
    }
}
