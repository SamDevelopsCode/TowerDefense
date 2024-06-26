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
		[SerializeField] private List<TowerCollection> _towerCollections;

		[SerializeField] private List<GameObject> _towerVisualizations;
		private GameObject _selectedTowerVisualization;

		private GameObject _currentlySelectedTower;
		private GameObject _newlySpawnedTower;
		private Tower _selectedTowerType;
		private TowerStats _currentlySelectedTowerStats;
		private Tile _currentlySelectedTowerParentTile;
		private bool _canPlaceTowers;

		public event Action<TowerStats> TowerTypeSelected;
		public event Action TowerPlacementFailed;
		public event Action TowerPlacementSucceeded;
		public event Action TowerUpgraded;
		public event Action TowerSold;
		public event Action TowerUpgradeFailed;
		public event Action<Tower> TowerFiredShot;
		
		private enum TowerType
		{
			Ballista,
			Fire,
			Lightning,
		}


		private void OnEnable()
		{
			GameManager.GameStateChanged += GameManagerGameStateChanged;
		}

		
		private void OnDisable()
		{
			GameManager.GameStateChanged -= GameManagerGameStateChanged;
		}


		private void Start()
		{
			for (int i = 0; i < _validTowerTilesParent.childCount; i++)
			{
				Tile tile = _validTowerTilesParent.GetChild(i).GetComponent<Tile>();
				tile.TowerPlaceAttempted += OnTowerPlaceAttempted;
				tile.TileMouseHovered += OnTileMouseHovered;
			}
		}
		
		
		private void Update()
		{
			HandleTowerSelectionInput();
		}
		
		
		private void OnTileMouseHovered(Transform tileCenter)
		{
			if (_selectedTowerVisualization != null)
			{
				_selectedTowerVisualization.transform.position = tileCenter.position;
			}
		}


		private void GameManagerGameStateChanged(GameState state)
		{
			_canPlaceTowers = state == GameState.TowerPlacement;

			if (state == GameState.EnemyWave)
			{
				HideCurrentlySelectedTowersRangeVisualization();
				NullifyCurrentlySelectedTower();
				HideTowerSelectionVisualization();
				NullifySelectedTowerVisualization();
				NullifySelectedTowerType();
			}
		}


		private void HandleTowerSelectionInput()
		{
			if (!_canPlaceTowers)
			{
				return;
			}
			
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				SelectTowerType((int)TowerType.Ballista);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				SelectTowerType((int)TowerType.Fire);
			}	
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				SelectTowerType((int)TowerType.Lightning);
			}
		}


		private void SelectTowerType(int index)
		{
			if (_selectedTowerVisualization != null)
			{
				HideTowerSelectionVisualization();
			}
				
			if (_currentlySelectedTower != null)
			{
				HideCurrentlySelectedTowersRangeVisualization();
				NullifyCurrentlySelectedTower();
			}
				
			_selectedTowerType = _towerBaseTypePrefabs[index].GetComponent<Tower>();
			_selectedTowerVisualization = _towerVisualizations[index];
			
			TowerTypeSelected?.Invoke(_selectedTowerType.towerStats);
		}


		private void OnTowerSelected(TowerStats towerStats, GameObject currentlySelectedTower)
		{
			if (_currentlySelectedTower != null)
			{
				HideCurrentlySelectedTowersRangeVisualization();
			}
			
			if (_selectedTowerVisualization != null)
			{
				HideTowerSelectionVisualization();
				NullifySelectedTowerVisualization();
				NullifySelectedTowerType();
				TowerTypeSelected?.Invoke(null);
			}
			
			_currentlySelectedTowerStats = towerStats;
			_currentlySelectedTower = currentlySelectedTower;
			ShowCurrentlySelectedTowersRangeVisualization();
		}

		
		public void UpgradeTower()
		{
			if (GameManager.Instance.State != GameState.TowerPlacement) return;

			if (_currentlySelectedTower == null) return;
			
			int currentTowerTypeIndex = ((int)_currentlySelectedTowerStats.towerType);
			int currentTowerIndex = _currentlySelectedTower.GetComponent<Tower>().towerStats.towerTier;
			
			if (!(currentTowerIndex <= _towerCollections[currentTowerTypeIndex].towers.Count - 1))
			{
				TowerUpgradeFailed?.Invoke();
				Debug.Log("Upgrade maxed out");
				return;
			}
			
			GameObject upgradedTowerPrefab =
				_towerCollections[currentTowerTypeIndex].towers[currentTowerIndex];
			
			if (Bank.Instance.CanAffordTower(upgradedTowerPrefab.GetComponent<Tower>().towerStats.cost))
			{
				TowerUpgraded?.Invoke();
				int currentTowerTargetingType =
					(int)_currentlySelectedTower.GetComponent<Tower>().targetingSystem.currentTargetingType;
				
				Destroy(_currentlySelectedTower);
				
				_newlySpawnedTower = _towerSpawner.SpawnTower(upgradedTowerPrefab, _currentlySelectedTower.transform.parent);
				_newlySpawnedTower.GetComponent<Tower>().TowerSelected += OnTowerSelected;
				_newlySpawnedTower.GetComponent<Attacker>().shotFired += OnTowerShotFired;
				
				_newlySpawnedTower.GetComponent<RangeVisualizer>().EnableRangeVisualization();
				
				_currentlySelectedTowerStats = upgradedTowerPrefab.GetComponent<Tower>().towerStats;
				TowerTypeSelected?.Invoke(_currentlySelectedTowerStats);
				
				_currentlySelectedTower = _newlySpawnedTower;
				
				UpdateTowerTargetingBehaviour(currentTowerTargetingType);
				CoreGameUI.Instance.UpdateTargetingDropDownValue(_currentlySelectedTower);
			}
			else
			{
				TowerUpgradeFailed?.Invoke();
				Debug.Log("Can't afford tower upgrade.");
			}
		}


		public void SellTower()
		{
			if (GameManager.Instance.State != GameState.TowerPlacement) return;

			if (_currentlySelectedTower == null) return;

			int sellAmount = Mathf.RoundToInt(_currentlySelectedTower.GetComponent<Tower>().towerStats.cost * .5f);
			Bank.Instance.AddToBalance(sellAmount);

			Tile tile = _currentlySelectedTower.transform.parent.parent.GetComponent<Tile>();
			tile.CanPlaceTower = true;
			
			Destroy(_currentlySelectedTower);
			NullifyCurrentlySelectedTower();
			
			TowerSold?.Invoke();
		}
		
		
		private void OnTowerPlaceAttempted(Tile tile)
		{
			if (_selectedTowerType == null)
			{
				TowerTypeSelected?.Invoke(null);
				HideCurrentlySelectedTowersRangeVisualization();
				Debug.Log("No tower has been selected.");
				return;
			}
			
			if (!_canPlaceTowers)
			{
				return;
			}
			
			int towerCost = _selectedTowerType.towerStats.cost;
			
			if (Bank.Instance.CanAffordTower(towerCost))
			{
				TowerPlacementSucceeded?.Invoke();
				
				tile.CanPlaceTower = false;
				
				GameObject spawnedTower = _towerSpawner.SpawnTower(_selectedTowerType.gameObject, tile.towerParent);
				
				spawnedTower.GetComponent<Tower>().TowerSelected += OnTowerSelected;
				spawnedTower.GetComponent<Attacker>().shotFired += OnTowerShotFired;
				
				Bank.Instance.DetractFromBalance(towerCost);
				
				NullifySelectedTowerType();
				
				HideTowerSelectionVisualization();
				NullifySelectedTowerVisualization();
			}
			else
			{
				TowerPlacementFailed?.Invoke();
				Debug.Log("Not enough funds. Tower cost: " + towerCost + ". Current money: " + Bank.Instance.CurrentBalance);
			}
		}

		
		private void OnTowerShotFired(Tower tower)
		{
			TowerFiredShot?.Invoke(tower);
		}


		private void ShowCurrentlySelectedTowersRangeVisualization()
		{
			_currentlySelectedTower.GetComponent<RangeVisualizer>().EnableRangeVisualization();
		}
	
		
		private void HideCurrentlySelectedTowersRangeVisualization()
		{
			if (_currentlySelectedTower != null)
			{
				_currentlySelectedTower.GetComponent<RangeVisualizer>().DisableRangeVisualization();
			}
		}

		
		public void HideTowerSelectionVisualization()
		{
			if (_selectedTowerVisualization == null) return; 
			
			_selectedTowerVisualization.transform.localPosition = new Vector3(0,0,0);
		}
		
		
		private void NullifyCurrentlySelectedTower()
		{
			_currentlySelectedTower = null;
		}
		
		
		private void NullifySelectedTowerType()
		{
			_selectedTowerType = null;
		}


		private void NullifySelectedTowerVisualization()
		{
			_selectedTowerVisualization = null;
		}

		
		public void UpdateTowerTargetingBehaviour(int selectedOptionIndex)
		{
			TargetingSystem towerTargetingSystem = _currentlySelectedTower.GetComponent<Tower>().targetingSystem;
			towerTargetingSystem.currentTargetingType = (TargetingSystem.TargetingType)selectedOptionIndex;
		}
	}
}
