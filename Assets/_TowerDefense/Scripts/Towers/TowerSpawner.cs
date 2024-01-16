using UnityEngine;

namespace _TowerDefense.Towers
{
	public class TowerSpawner : MonoBehaviour
	{
		public void SpawnTower(Tower towerPrefab, Transform towerParent)
		{
			Debug.Log("Instantiating tower at " + towerParent.name);
			Instantiate(towerPrefab, towerParent.position, Quaternion.identity, towerParent);
		}
	}
}
