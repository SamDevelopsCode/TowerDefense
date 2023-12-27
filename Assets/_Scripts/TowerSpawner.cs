using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
	public void SpawnTower(Tower towerPrefab, Transform towerPosition)
	{
		Debug.Log("Instantiating tower at " + towerPosition);
		Instantiate(towerPrefab, towerPosition.position, Quaternion.identity);
	}
}
