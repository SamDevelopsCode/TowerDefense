using System;
using TowerDefense.Managers;
using TowerDefense.Tower;
using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField] private bool _isTowerPlaceable = false;

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
		_towerManager.onTowerPlacementSuccess += SetIsTowerPlaceable;
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
			
			// TowerManager handles assignment of _isDefensePlaceable after event invocation
		}
	}
	
	
	private void OnMouseExit()
	{
		_mouseHasEnteredTile = false;
	}

	
	private void SetIsTowerPlaceable(bool isTowerPlaceable)
	{
		_isTowerPlaceable = isTowerPlaceable;
	}
}
