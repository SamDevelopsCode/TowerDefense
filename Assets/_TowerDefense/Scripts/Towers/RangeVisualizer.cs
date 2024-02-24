using System;
using _TowerDefense.Towers;
using UnityEngine;

public class RangeVisualizer : MonoBehaviour
{
    private Tower _tower;
    private TowerStats _towerStats;
    [SerializeField] private MeshRenderer _rangeVisualizer;


    private void Awake()
    {
        _tower = GetComponent<Tower>();
        _towerStats = _tower.towerStats;
        _rangeVisualizer.transform.localScale = new Vector3(_towerStats.range * 2, .5f, _towerStats.range * 2);
    }
    
    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _towerStats.range);
    }

    
    public void EnableRangeVisualization()
    {
        _rangeVisualizer.enabled = true;
    }
    
    
    public void DisableRangeVisualization()
    {
        _rangeVisualizer.enabled = false;
    }
    
    
}
