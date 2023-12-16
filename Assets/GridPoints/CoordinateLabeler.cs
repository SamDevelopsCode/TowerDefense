using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


[ExecuteInEditMode]
public class CoordinateLabeler : MonoBehaviour
{

    [SerializeField] private TextMeshPro textLabel;

    private Vector2Int _coordinates;

    
    private void Update()
    {
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
    }

    private void DisplayCoordinates()
    {
        var position = transform.position;
        _coordinates.x = Mathf.RoundToInt(position.x / 10);
        _coordinates.y = Mathf.RoundToInt(position.z / 10);
        textLabel.text = _coordinates.ToString();
    }

    private void UpdateObjectName()
    {
        name = _coordinates.ToString();
    }
    
    
    
    
}
