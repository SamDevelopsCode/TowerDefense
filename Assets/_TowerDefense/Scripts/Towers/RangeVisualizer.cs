using _TowerDefense.Towers;
using UnityEngine;

public class RangeVisualizer : MonoBehaviour
{
    private Tower _tower;
    private TowerData _towerData;
    
    private void Awake()
    {
        _tower = GetComponent<Tower>();
        _towerData = _tower.towerData;
    }
    
    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _towerData.range);
    }
}
