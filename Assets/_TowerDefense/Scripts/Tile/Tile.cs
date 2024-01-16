using System;
using _TowerDefense.Towers;
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
			if (_isTowerPlaceable)
			{
				OnTowerPlaceAttempted?.Invoke(this);
			}
			else
			{
				Debug.Log(towerParent.GetChild(0).name + " at " + towerParent.parent.name);
				// show the TowerStats + Upgrade button + Sell button
			}
		}
	}
	
	
	private void OnMouseExit()
	{
		_mouseHasEnteredTile = false;
	}
	
}
