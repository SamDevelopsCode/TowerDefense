using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnEnemyDied;
    
    [SerializeField] private float _maxHealth = 10f;
    [SerializeField] private float _currentHealth;

    private void OnEnable()
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
