using UnityEngine;

namespace _TowerDefense.Towers
{
	public class TowerSpawner : MonoBehaviour
	{
		public GameObject SpawnTower(GameObject towerPrefab, Transform towerParent)
		{
			return Instantiate(towerPrefab, towerParent.position, Quaternion.identity, towerParent);
		}
	}
}
