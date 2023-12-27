using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private int _maxBaseHealth = 100;
    private int _currentBaseHealth;

    public int CurrentBaseHealth
    {
        get
        {
            return _currentBaseHealth;
        }
        set
        {
            _currentBaseHealth = value;
            UIManager.Instance.UpdateBaseHealthUI(_currentBaseHealth, _maxBaseHealth);
        }
    }
    
    
    private void Start()
    {
        CurrentBaseHealth = _maxBaseHealth;
    }


    private void TakeDamage(int damage)
    {
        CurrentBaseHealth -= damage;
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with enemy most likely");
        TakeDamage(other.transform.parent.GetComponent<Enemy>().DamageToBase);
        Destroy(other.transform.parent.gameObject);
    }
}
