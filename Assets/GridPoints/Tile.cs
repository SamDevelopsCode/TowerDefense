using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour
{
    public event Action OnTowerSpawnAttempted;
    
    [SerializeField] private bool _isDefensePlaceable = false;

    
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!_isDefensePlaceable) return;
            
            OnTowerSpawnAttempted?.Invoke();
            _isDefensePlaceable = false;
        }        
    }
}
