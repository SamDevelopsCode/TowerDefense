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
        Debug.Log(transform.name);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log(name);
            if (!isDefensePlaceable) return;
            
            var defenseInstance = Instantiate(defensePrefab, transform.position, quaternion.identity);
            isDefensePlaceable = false;
        }
        
        
    }
}
