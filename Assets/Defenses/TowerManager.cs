using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
	[SerializeField] private GameObject _towerPrefab;
	
	private ResourceBank _resourceBank;
	

	private void Start()
	{
		_resourceBank = FindObjectOfType<ResourceBank>();
	}

	private void PlaceTower(Transform towerPosition)
	{
		var towerInstance = Instantiate(_towerPrefab, towerPosition.position, Quaternion.identity);
	}
	
}
