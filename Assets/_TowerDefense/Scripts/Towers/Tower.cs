using System;
using TowerDefense.Managers;
using TowerDefense.Tower;
using UnityEngine;
using UnityEngine.Serialization;

namespace _TowerDefense.Towers
{
    public class Tower : MonoBehaviour
    {
        public TowerStats towerStats;
        [SerializeField] private SphereCollider _sphereCollider;
        
        public event Action<TowerStats, GameObject> TowerSelected;

        
        private void Awake()
        {
            _sphereCollider.radius = towerStats.range;
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
                TowerStats towerStats = GetComponent<Tower>().towerStats;
                TowerSelected?.Invoke(towerStats, gameObject);
                CoreGameUI.Instance.OnTowerTypeSelected(towerStats);
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
