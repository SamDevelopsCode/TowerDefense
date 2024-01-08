using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
	[SerializeField] private Transform _placeableTiles;
	[SerializeField] private TowerSpawner _towerSpawner;
	
	[SerializeField] private Tower _towerPrefab;
	
	private Tower _selectedTowerType;
	private bool _canPlaceTowers;

	private void Awake()
	{
		GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
	}

	private void Start() 
	{
		for (int i = 0; i < _placeableTiles.childCount; i++)
		{
			_placeableTiles.GetChild(i).GetComponent<Tile>().OnTileMouseOver += HandleOnMouseTileOver;
			_placeableTiles.GetChild(i).GetComponent<Tile>().OnTowerPlaceAttempted += HandleOnTowerPlaceAttempted;
		}
	}

	private void GameManagerOnGameStateChanged(GameState state)
	{
		_canPlaceTowers = state == GameState.TowerPlacement;
	}
		
	private void Update() 
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			_selectedTowerType = _towerPrefab;
			Debug.Log("Tower type 1 chosen.");
		}	
	}


	private void HandleOnMouseTileOver(string tileName)
	{
		//TODO spawn or move a transparent version of the selected tower type to the tile position
	}
	
	
	private void HandleOnTowerPlaceAttempted(Tile tile)
	{
		if (_canPlaceTowers)
		{
			print("Tried to place tower");
			if (_selectedTowerType == null)
			{
				Debug.Log("No tower has been selected.");			
				return;
			}
		
			if (_selectedTowerType.TowerCost <= CurrencyManager.Instance.CurrentBalance)
			{
				_towerSpawner.SpawnTower(_selectedTowerType, tile.transform);
				CurrencyManager.Instance.DetractFromBalance(_selectedTowerType.TowerCost);
			}
			else
			{
				Debug.Log("Not enough funds. Tower cost: " + _selectedTowerType.TowerCost.ToString() + ". Current money: " + CurrencyManager.Instance.CurrentBalance.ToString());
			}
		}
	}
	
}
