using System;
using TowerDefense.Managers;
using UnityEngine;

namespace _TowerDefense.Towers
{
    public class Tower : MonoBehaviour
    {
        public TowerData towerData;
        [SerializeField] private SphereCollider _sphereCollider;

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
