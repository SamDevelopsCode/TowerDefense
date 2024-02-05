using System;
using System.Collections.Generic;
using TowerDefense.Managers;
using TowerDefense.Tower;
using UnityEngine;

namespace _TowerDefense.Towers
{
	public class TowerManager : MonoBehaviour
	{
		[SerializeField] private Transform _validTowerTilesParent;
		[SerializeField] private TowerSpawner _towerSpawner;
		[SerializeField] private List<GameObject> _towerBaseTypePrefabs = new();

		private Tower _selectedTowerType;
		private bool _canPlaceTowers;

		public event Action<TowerData> TowerSelected;
		public event Action TowerPlacementFailed;
		public event Action TowerPlacementSucceeded;
		

		private void OnEnable()
		{
			GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
		}

		
		private void OnDisable()
		{
			GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
		}


		private void Start() 
		{
			for (int i = 0; i < _validTowerTilesParent.childCount; i++)
			{
				_validTowerTilesParent.GetChild(i).GetComponent<Tile>().OnTowerPlaceAttempted += OnTowerPlaceAttempted;
			}
		}

	
		private void GameManagerOnGameStateChanged(GameState state)
		{
			_canPlaceTowers = state == GameState.TowerPlacement;
		}
	
		
		private void Update()
		{
			SelectTowerType();
		}

		
		private void SelectTowerType()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				_selectedTowerType = _towerBaseTypePrefabs[0].GetComponent<Tower>();
				TowerSelected?.Invoke(_selectedTowerType.towerData);
				Debug.Log("Tower type Ballista chosen.");
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				_selectedTowerType = _towerBaseTypePrefabs[1].GetComponent<Tower>();
				TowerSelected?.Invoke(_selectedTowerType.towerData);
				Debug.Log("Tower type Fire chosen.");
			}	
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				_selectedTowerType = _towerBaseTypePrefabs[2].GetComponent<Tower>();
				TowerSelected?.Invoke(_selectedTowerType.towerData);
				Debug.Log("Tower type Lightning chosen.");
			}
		}


		private void OnTowerPlaceAttempted(Tile tile)
		{
			if (!_canPlaceTowers)
			{
				return;
			}
			
			if (_selectedTowerType == null)
			{
				Debug.Log("No tower has been selected.");
				return;
			}
			
			int towerCost = _selectedTowerType.towerData.cost;
			
			if (Bank.Instance.CanAffordTower(towerCost))
			{
				tile.CanPlaceTower = false;
				_towerSpawner.SpawnTower(_selectedTowerType, tile.towerParent);
				Bank.Instance.DetractFromBalance(towerCost);
				TowerPlacementSucceeded?.Invoke();
				_selectedTowerType = null;
			}
			else
			{
				TowerPlacementFailed?.Invoke();
				Debug.Log("Not enough funds. Tower cost: " + towerCost + ". Current money: " + Bank.Instance.CurrentBalance);
			}
			
		}
	}
}
