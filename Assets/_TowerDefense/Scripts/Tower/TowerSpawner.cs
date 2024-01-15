using UnityEngine;

namespace TowerDefense.Tower
{
	public class TowerSpawner : MonoBehaviour
	{
		public void SpawnTower(Tower towerPrefab, Transform towerParent)
		{
			Debug.Log("Instantiating tower at " + towerParent.position);
			Instantiate(towerPrefab, towerParent.position, Quaternion.identity, towerParent);
		}
	}
}
