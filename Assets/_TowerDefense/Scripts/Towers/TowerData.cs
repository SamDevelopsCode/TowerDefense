using UnityEngine;

namespace _TowerDefense.Towers
{
	[CreateAssetMenu(fileName = "Tower", menuName = "Tower / Create New Tower Data") ]
	public class TowerData : ScriptableObject
	{
		public Sprite icon;
		public string towerName;
		public int cost = 100;
		public float range = 6f;
		public float damagePerShot = 5f;
		public float shotsPerSecond = 2f;
		public TowerType towerType;
		
		public enum TowerType
		{
			Ballista,
			Fire,
			Lightning,
		}
	}
}
