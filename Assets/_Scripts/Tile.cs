using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour
{
	
	[SerializeField] private bool _isDefensePlaceable = false;

	private TowerManager _towerManager;
	
	public event Action<string> OnTileMouseOver;
	public event Action<Tile> OnTowerPlaceAttempted;
	
	private bool hasMouseEntered = false;
		

	private void OnMouseOver()
	{		
		if (!hasMouseEntered)
		{
			OnTileMouseOver?.Invoke(name);
			hasMouseEntered = true;
		}		
		
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{			
			if (!_isDefensePlaceable) return;
			
			OnTowerPlaceAttempted?.Invoke(this);
			
			_isDefensePlaceable = false;
		}        
	}
	
	
	private void OnMouseExit() 
	{
		hasMouseEntered = false;
	}
	
}
