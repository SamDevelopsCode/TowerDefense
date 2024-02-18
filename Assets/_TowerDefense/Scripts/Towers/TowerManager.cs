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
		private Dictionary<int, List<GameObject>> allTowers = new();

		private Tower _selectedTowerType;
		private TowerData _currentlySelectedTowerData;
		private Tile _currentlySelectedTowerParentTile;
		private bool _canPlaceTowers;

		public event Action<TowerData> TowerSelected;
		public event Action TowerPlacementFailed;
		public event Action TowerPlacementSucceeded;

		
		private void Awake()
		{
			for (int i = 0; i < _towerCollections.Count; i++)
			{
				allTowers.Add(i, _towerCollections[i].towers);
			}
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
				tile.TowerSelected += OnTowerSelected;
			}
		}

		
		private void GameManagerGameStateChanged(GameState state)
		{
			_canPlaceTowers = state == GameState.TowerPlacement;
		}
	
		
		private void Update()
		{
			SelectTowerType();
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
				TowerSelected?.Invoke(_selectedTowerType.towerData);
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				_selectedTowerType = _towerBaseTypePrefabs[1].GetComponent<Tower>();
				TowerSelected?.Invoke(_selectedTowerType.towerData);
			}	
			else if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				_selectedTowerType = _towerBaseTypePrefabs[2].GetComponent<Tower>();
				TowerSelected?.Invoke(_selectedTowerType.towerData);
			}
		}

		
		private void OnTowerSelected(TowerData towerData, Tile tile)
		{
			_currentlySelectedTowerData = towerData;
			_currentlySelectedTowerParentTile = tile;
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

		
		public void UpgradeTower()
		{
			int currentTowerTypeIndex = ((int)_currentlySelectedTowerData.towerType);
			GameObject currentTowerGameObject = _currentlySelectedTowerParentTile.towerParent.GetChild(0).gameObject;
			int currentTowerIndex = currentTowerGameObject.GetComponent<Tower>().towerData.towerTier;
			
			if (!(currentTowerIndex <= _towerCollections[currentTowerTypeIndex].towers.Count - 1))
			{
				Debug.Log("Upgrade maxed out");
				return;
			}
			
			Tower upgradedTower =
				_towerCollections[currentTowerTypeIndex].towers[currentTowerIndex].GetComponent<Tower>();
			
			if (Bank.Instance.CanAffordTower(upgradedTower.towerData.cost))
			{
				Destroy(_currentlySelectedTowerParentTile.towerParent.GetChild(0).gameObject);
				_towerSpawner.SpawnTower(upgradedTower, _currentlySelectedTowerParentTile.towerParent);
				_currentlySelectedTowerData = upgradedTower.towerData;
				TowerSelected?.Invoke(_currentlySelectedTowerData);
			}
			else
			{
				Debug.Log("Can't afford tower upgrade.");
			}
		}
	}
}
