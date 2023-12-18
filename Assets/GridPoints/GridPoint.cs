using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridPoint : MonoBehaviour
{

    [SerializeField] private GameObject defensePrefab;
    [SerializeField] private bool isDefensePlaceable = false;
    
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!isDefensePlaceable) return;
            
            var defenseInstance = Instantiate(defensePrefab, transform.position, quaternion.identity);
            isDefensePlaceable = false;
        }
        
        
    }
}
