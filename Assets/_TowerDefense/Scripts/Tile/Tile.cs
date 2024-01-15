using System;
using TowerDefense.Managers;
using TowerDefense.Tower;
using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField] private bool _isTowerPlaceable = false;
	[SerializeField] public Transform towerParent;

	public bool IsTowerPlaceable
	{
		get => _isTowerPlaceable;
		set => _isTowerPlaceable = value;
	}
	

	private TowerManager _towerManager;
	
	public event Action<string> OnTileMouseOver;
	public event Action<Tile> OnTowerPlaceAttempted;
	
	private bool _mouseHasEnteredTile;


	private void Awake()
	{
		_towerManager = FindObjectOfType<TowerManager>();
	}


	private void Start()
	{
		_isTowerPlaceable = true;
	}


	private void OnMouseOver()
	{
		if (!_mouseHasEnteredTile)
		{
			OnTileMouseOver?.Invoke(name);
			_mouseHasEnteredTile = true;
		}		
		
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{			
			if (!_isTowerPlaceable) return;
			
			OnTowerPlaceAttempted?.Invoke(this);
		}
	}
	
	
	private void OnMouseExit()
	{
		_mouseHasEnteredTile = false;
	}
	
	//TODO if we click on a tile, check if it has a tower as a child of the TowerParent GameObject
	//If it does, show the TowerStats + Upgrade button + Sell button
}
