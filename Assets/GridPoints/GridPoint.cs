using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class GridPoint : MonoBehaviour
{

    [SerializeField] private GameObject _defensePrefab;
    [SerializeField] private bool _isDefensePlaceable = false;
    
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!_isDefensePlaceable) return;
            
            var defenseInstance = Instantiate(_defensePrefab, transform.position, quaternion.identity);
            _isDefensePlaceable = false;
        }
        
        
    }
}
