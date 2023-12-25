using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Health : MonoBehaviour
{

    public event Action OnEntityDied;
    
    [SerializeField] private float _maxHealth = 10f;
    [SerializeField] private float _currentHealth;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.Log("Current Health: " + _currentHealth);

        if (_currentHealth <= 0)
        {
            OnEntityDied?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
