using System;
using UnityEngine;

public class Health : MonoBehaviour
{    
    [SerializeField] private float _maxHealth = 10f;
    [SerializeField] private float _currentHealth;

    public float CurrentHealth
    {
        get => _currentHealth;
    }
    
    public event Action OnEnemyDied;
    
    
    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(0, _currentHealth);
        Debug.Log("Current Health: " + _currentHealth);

        if (_currentHealth == 0)
        {
            OnEnemyDied?.Invoke();
        }
    }
}
