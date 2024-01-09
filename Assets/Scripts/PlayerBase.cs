using System;
using TowerDefense.Managers;
using TowerDefense.Tower;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private int _maxBaseHealth = 100;
    private int _currentBaseHealth;

    public event Action<GameState> OnPlayerBaseDestroyed;

    public int CurrentBaseHealth
    {
        get
        {
            return _currentBaseHealth;
        }
        set
        {
            _currentBaseHealth = Mathf.Max(0, value);

            if (_currentBaseHealth == 0)
            {
                OnPlayerBaseDestroyed?.Invoke(GameState.Lose);
            }
            
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
        var enemy = other.transform.parent;
        TakeDamage(enemy.GetComponent<Enemy>().DamageToBase);
        Destroy(enemy.gameObject);
    }
}
