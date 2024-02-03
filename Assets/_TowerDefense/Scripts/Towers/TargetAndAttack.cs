using System;
using TowerDefense.Enemies;
using UnityEngine;

namespace _TowerDefense.Towers
{
    public class TargetAndAttack : MonoBehaviour
    {
        [SerializeField] private Transform _towerPivot;
        
        private GameObject _target;
        
        private bool _canShoot = true;
        private float _shootTimer = 0f;

        private Tower _tower;
        private TowerData _towerData;
        
        
        private void Awake()
        {
            _tower = GetComponent<Tower>();
            _towerData = _tower.towerData;
        }


        private void Update()
        {
            FindClosestTarget();

            if (_target != null)
            {
                AimWeapon();
                AttemptToShoot();
            }
        }
    

        private void AttemptToShoot()
        {
            if (_canShoot)
            {
                Shoot();
                _canShoot = false;
            } 
            else if (!_canShoot)
            {
                _shootTimer += Time.deltaTime;
            
                if (_shootTimer >= _towerData.shotsPerSecond)
                {
                    _canShoot = true;
                    _shootTimer = 0f;
                }
            }
        }


        private void FindClosestTarget()
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            GameObject closetTarget = null;
            float maxAttackRange = _towerData.range;


            foreach (var enemy in enemies)
            {
                var distanceToTarget = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceToTarget < maxAttackRange)
                {
                    closetTarget = enemy.gameObject;
                    maxAttackRange = distanceToTarget;
                }
            }
            _target = closetTarget;
        }

    
        private void AimWeapon()
        {
            _towerPivot.LookAt(_target.transform.position);
        }

    
        private void Shoot()
        {
            var healthComponent = _target.GetComponent<Health>();
            healthComponent.TakeDamage(_towerData.damagePerShot);
        }   
    }
}


