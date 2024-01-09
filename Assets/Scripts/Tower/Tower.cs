using UnityEngine;

namespace TowerDefense.Tower
{
	public class Tower : MonoBehaviour
	{
		[SerializeField] private int _towerCost = 100;
		public int TowerCost { get { return _towerCost;} }
	}
}
