using UnityEngine;
using UnityEngine.UI;

namespace _TowerDefense.Towers
{
	[CreateAssetMenu(fileName = "Tower", menuName = "Tower / Create New Tower Data") ]
	public class TowerData : ScriptableObject
	{
		[SerializeField] public Sprite icon;
		[SerializeField] public string name;
		[SerializeField] public int cost = 100;
		[SerializeField] public float range = 6f;
		[SerializeField] public float damagePerShot = 5f;
		[SerializeField] public float shotsPerSecond = 2f;
	}
}
