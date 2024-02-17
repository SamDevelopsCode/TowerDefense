using UnityEngine;

namespace _TowerDefense.Towers
{
	public class TowerSpawner : MonoBehaviour
	{
		public void SpawnTower(Tower towerPrefab, Transform towerParent)
		{
			Instantiate(towerPrefab, towerParent.position, Quaternion.identity, towerParent);
		}
	}
}
