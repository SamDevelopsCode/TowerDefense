using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField][Range(0f, 155f)] private float _moveSpeed = 2.0f;
    [SerializeField] private List<Tile> _path = new List<Tile>();
    
    private void Start()
    {
        FindPath();
        PlaceEnemyAtStart();
        StartCoroutine(MoveAlongPath());
    }
    

    private void PlaceEnemyAtStart()
    {
        GameObject startingTile = GameObject.FindGameObjectWithTag("StartingTile");
        transform.position = startingTile.transform.position;
    }


    private void FindPath()
    {
        _path.Clear();
        
        var gridPointParent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in gridPointParent.transform)
        {
            _path.Add(child.GetComponent<Tile>());
        }
    }
    
    
    private IEnumerator MoveAlongPath()
    {
        foreach (var tile in _path)
        {
            var startPosition = transform.position;
            var endPosition = tile.transform.position;
            var travelPercent = 0f;
            
            transform.LookAt(endPosition);
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * _moveSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        // Destroy(gameObject);
    }
}

