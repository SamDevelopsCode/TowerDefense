using System;
using TowerDefense.Managers;
using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField] public Transform towerParent;
	[SerializeField] private bool _canPlaceTower;

	public bool CanPlaceTower
	{
		get => _canPlaceTower;
		set => _canPlaceTower = value;
	}

	public event Action<Transform> TileMouseHovered;
	public event Action<Tile> TowerPlaceAttempted;
	
	private bool _mouseHasEnteredTile;


	private void Start()
	{
		_canPlaceTower = true;
	}

	
	private void OnMouseOver()
	{
		if (GameManager.Instance.State != GameState.TowerPlacement) return;  
		
		if (!_mouseHasEnteredTile)
		{
			TileMouseHovered?.Invoke(towerParent);
			_mouseHasEnteredTile = true;
		}		
		
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			if (_canPlaceTower)
			{
				TowerPlaceAttempted?.Invoke(this);
			}
		}
	}
	
	
	private void OnMouseExit()
	{
		_mouseHasEnteredTile = false;
	}
}
