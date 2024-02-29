using UnityEngine;
using UnityEngine.Serialization;

namespace _TowerDefense.Towers
{
	[CreateAssetMenu(fileName = "Tower", menuName = "Tower / Create New Tower Data") ]
	public class TowerStats : ScriptableObject
	{
		public Sprite icon;
		public string towerName;
		public int cost = 100;
		public float range = 6f;
		public float damagePerShot = 5f;
		public float fireCooldown = 2f;
		public TowerType towerType;
		public int towerTier;
		
		public enum TowerType
		{
			Ballista,
			Fire,
			Lightning,
		}
	}
}
