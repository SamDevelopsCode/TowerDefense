using System;
using TowerDefense.Enemies;
using TowerDefense.Managers;
using TowerDefense.Tower;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private int _maxBaseHealth = 100;
    private int _currentBaseHealth;

    
    public int CurrentBaseHealth
    {
        get => _currentBaseHealth;
        set
        {
            _currentBaseHealth = Mathf.Max(0, value);

            if (_currentBaseHealth == 0)
            {
                GameManager.Instance.UpdateGameState(GameState.Lose);
            }
            
            CoreGameUI.Instance.UpdateBaseHealthUI(_currentBaseHealth, _maxBaseHealth);
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
    
    
    /* Using the setter KilledByTower to set a boolean
     that then calls StartDeathSequence to handle the
     event that enemy makes it to base without dropping
     resources for the player. */
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.transform.parent.GetComponent<Enemy>();
        enemy.KilledByTower = false;
        TakeDamage(enemy.DamageToBase);
    }
}
