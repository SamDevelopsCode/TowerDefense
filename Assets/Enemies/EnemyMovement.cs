using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField][Range(0f, 5f)] private float _moveSpeed = 2.0f;
    [SerializeField] private List<GridPoint> path = new List<GridPoint>();
    
    private void Start()
    {
        StartCoroutine(MoveAlongPath());
    }

    private IEnumerator MoveAlongPath()
    {
        foreach (var gridPoint in path)
        {
            var startPosition = transform.position;
            var endPosition = gridPoint.transform.position;
            var travelPercent = 0f;
            
            transform.LookAt(endPosition);
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * _moveSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    
    
    
    
}

