using UnityEngine;

namespace _TowerDefense.Towers
{
	[CreateAssetMenu(fileName = "Tower", menuName = "Tower / Create New Tower Data") ]
	public class TowerData : ScriptableObject
	{
		[SerializeField] public int towerCost = 100;
		[SerializeField] public float towerRange = 6f;
		[SerializeField] public float damagePerShot = 5f;
		[SerializeField] public float shotsPerSecond = 2f;
	}
}
