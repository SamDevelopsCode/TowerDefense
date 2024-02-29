using System;
using TowerDefense.Managers;
using TowerDefense.Tower;
using UnityEngine;
using UnityEngine.Serialization;

namespace _TowerDefense.Towers
{
    public class Tower : MonoBehaviour
    {
        public TargetingSystem targetingSystem;
        
        public TowerStats towerStats;
        
        public event Action<TowerStats, GameObject> TowerSelected;

        
        private void OnMouseOver()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                TowerStats towerStats = GetComponent<Tower>().towerStats;
                TowerSelected?.Invoke(towerStats, gameObject);
                CoreGameUI.Instance.OnTowerTypeSelected(towerStats);
                CoreGameUI.Instance.UpdateTargetingDropDownValue(gameObject);
            }
            
        }
    }
}
