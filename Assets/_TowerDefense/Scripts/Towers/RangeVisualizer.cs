using System;
using _TowerDefense.Towers;
using UnityEngine;

public class RangeVisualizer : MonoBehaviour
{
    private Tower _tower;
    private TowerData _towerData;
    [SerializeField] private MeshRenderer _rangeVisualizer;


    private void Awake()
    {
        _tower = GetComponent<Tower>();
        _towerData = _tower.towerData;
        _rangeVisualizer.transform.localScale = new Vector3(_towerData.range * 2, .5f, _towerData.range * 2);
    }
    
    // private void OnDrawGizmos()
    // {
    //     // Draw a yellow sphere at the transform's position
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(transform.position, _towerData.range);
    // }
}
