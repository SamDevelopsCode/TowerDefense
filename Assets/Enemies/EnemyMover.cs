using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _waitTimeUntilMoveAgain = 1.5f; 
    [SerializeField] private List<GridPoint> path = new List<GridPoint>();

    private void Start()
    {
        StartCoroutine(MoveAlongPath());
    }

    private IEnumerator MoveAlongPath()
    {
        foreach (var gridPoint in path)
        {
            var nextPosition = new Vector3(gridPoint.transform.position.x, 0, gridPoint.transform.position.z);
            print(gridPoint.name);
            print(nextPosition);
            transform.position = nextPosition;
            print("player's pos: " + transform.position);
            yield return new WaitForSeconds(_waitTimeUntilMoveAgain);
        }
    }
    
    
    
    
}

