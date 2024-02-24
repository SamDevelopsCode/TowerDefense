using System;
using TowerDefense.Managers;
using TowerDefense.Tower;
using UnityEngine;

namespace _TowerDefense.Towers
{
    public class Tower : MonoBehaviour
    {
        public TowerData towerData;
        [SerializeField] private SphereCollider _sphereCollider;
        
        public event Action<TowerData, GameObject> TowerSelected;

        
        private void Awake()
        {
            _sphereCollider.radius = towerData.range;
        }

        
        private void OnEnable()
        {
            GameManager.GameStateChanged += OnGameStateChanged;
        }

        
        private void OnDisable()
        {
            GameManager.GameStateChanged -= OnGameStateChanged;
        }


        private void OnMouseOver()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                TowerData towerData = GetComponent<Tower>().towerData;
                TowerSelected?.Invoke(towerData, gameObject);
                CoreGameUI.Instance.OnTowerTypeSelected(towerData);
                CoreGameUI.Instance.UpdateTargetingDropDownValue(gameObject);
            }
        }


        // Toggles the towers target detection colliders so they won't interfere with the placing of
        // other towers nearby with mouse detection
        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.TowerPlacement) SetSphereColliderActive(false);
            else if (gameState == GameState.EnemyWave) SetSphereColliderActive(true);
        }

        
        private void SetSphereColliderActive(bool shouldEnable)
        {
            _sphereCollider.enabled = shouldEnable;
        }
    }
}
