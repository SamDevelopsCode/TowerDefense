using System;
using System.Collections.Generic;
using TMPro;
using TowerDefense.Managers;
using TowerDefense.Tower;
using UnityEngine;
using UnityEngine.UI;

namespace _TowerDefense.Towers
{
	public class TowerManager : MonoBehaviour
	{
		[SerializeField] private Transform _validTowerTilesParent;
		[SerializeField] private TowerSpawner _towerSpawner;
		
		[SerializeField] private List<GameObject> _towerBaseTypePrefabs = new();
		[SerializeField] private List<TowerCollection> _towerCollections;

		private GameObject _currentlySelectedTower;
		private GameObject _newlySpawnedTower;
		private Tower _selectedTowerType;
		private TowerData _currentlySelectedTowerData;
		private Tile _currentlySelectedTowerParentTile;
		private bool _canPlaceTowers;

		public event Action<TowerData> TowerTypeSelected;
		public event Action TowerPlacementFailed;
		public event Action TowerPlacementSucceeded;


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
			}
		}

		
		private void GameManagerGameStateChanged(GameState state)
		{
			_canPlaceTowers = state == GameState.TowerPlacement;
		}
	
		
		private void Update()
		{
			SelectTowerType();
			Debug.Log(_currentlySelectedTower);
		}

		
		private void SelectTowerType()
		{
			if (!_canPlaceTowers)
			{
				return;
			}
			
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				_selectedTowerType = _towerBaseTypePrefabs[0].GetComponent<Tower>();
				TowerTypeSelected?.Invoke(_selectedTowerType.towerData);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				_selectedTowerType = _towerBaseTypePrefabs[1].GetComponent<Tower>();
				TowerTypeSelected?.Invoke(_selectedTowerType.towerData);
			}	
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				_selectedTowerType = _towerBaseTypePrefabs[2].GetComponent<Tower>();
				TowerTypeSelected?.Invoke(_selectedTowerType.towerData);
			}
		}

		
		private void OnTowerSelected(TowerData towerData, GameObject currentlySelectedTower)
		{
			_currentlySelectedTowerData = towerData;
			_currentlySelectedTower = currentlySelectedTower;
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
				GameObject spawnedTower = _towerSpawner.SpawnTower(_selectedTowerType.gameObject, tile.towerParent);
				spawnedTower.GetComponent<Tower>().TowerSelected += OnTowerSelected;
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

		
		public void UpgradeTower()
		{
			int currentTowerTypeIndex = ((int)_currentlySelectedTowerData.towerType);
			int currentTowerIndex = _currentlySelectedTower.GetComponent<Tower>().towerData.towerTier;
			
			if (!(currentTowerIndex <= _towerCollections[currentTowerTypeIndex].towers.Count - 1))
			{
				Debug.Log("Upgrade maxed out");
				return;
			}
			
			GameObject upgradedTowerPrefab =
				_towerCollections[currentTowerTypeIndex].towers[currentTowerIndex];
			
			if (Bank.Instance.CanAffordTower(upgradedTowerPrefab.GetComponent<Tower>().towerData.cost))
			{
				int _currentTowerTargetingType =
					(int)_currentlySelectedTower.GetComponent<TargetingSystem>().currentTargetingType;
				Destroy(_currentlySelectedTower);
				_newlySpawnedTower = _towerSpawner.SpawnTower(upgradedTowerPrefab, _currentlySelectedTower.transform.parent);
				_newlySpawnedTower.GetComponent<Tower>().TowerSelected += OnTowerSelected;
				_currentlySelectedTowerData = upgradedTowerPrefab.GetComponent<Tower>().towerData;
				TowerTypeSelected?.Invoke(_currentlySelectedTowerData);
				_currentlySelectedTower = _newlySpawnedTower;
				UpdateTowerTargetingBehaviour(_currentTowerTargetingType);
				CoreGameUI.Instance.UpdateTargetingDropDownValue(_currentlySelectedTower);
			}
			else
			{
				Debug.Log("Can't afford tower upgrade.");
			}
		}


		public void UpdateTowerTargetingBehaviour(int selectedOptionIndex)
		{
			TargetingSystem towerTargetingSystem = _currentlySelectedTower.GetComponent<TargetingSystem>();
			towerTargetingSystem.currentTargetingType = (TargetingSystem.TargetingType)selectedOptionIndex;
		}
	}
}
