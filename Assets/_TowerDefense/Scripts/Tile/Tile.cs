using System;
using _TowerDefense.Towers;
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

	public event Action<string> OnTileMouseOver;
	public event Action<Tile> OnTowerPlaceAttempted;
	
	private bool _mouseHasEnteredTile;


	private void Start()
	{
		_canPlaceTower = true;
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
			if (_canPlaceTower)
			{
				OnTowerPlaceAttempted?.Invoke(this);
			}
			else
			{
				TowerData towerData = towerParent.GetChild(0).GetComponent<Tower>().towerData;
				Debug.Log(towerParent.GetChild(0).name + " at " + towerParent.parent.name);
				Debug.Log($"Tower cost: {towerData.towerCost}, Tower Range: {towerData.towerRange}");
				//TODO show the TowerStats + Upgrade button + Sell button
			}
		}
	}
	
	
	private void OnMouseExit()
	{
		_mouseHasEnteredTile = false;
	}
}
