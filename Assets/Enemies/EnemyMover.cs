using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<GridPoint> path = new List<GridPoint>();

    private void Start()
    {
        PrintGridPoints();
    }

    private void PrintGridPoints()
    {
        foreach (var gridPoint in path)
        {
            Debug.Log(gridPoint);
        }
    }
    
    
}

