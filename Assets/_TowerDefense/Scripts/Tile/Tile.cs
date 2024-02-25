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
		if (_canPlaceTower && GameManager.Instance.State == GameState.TowerPlacement)
		{
			TileMouseHovered?.Invoke(towerParent);
		}
		
		if (Input.GetKeyDown(KeyCode.Mouse0) && _canPlaceTower)
		{
			TowerPlaceAttempted?.Invoke(this);
		}
	}
}
