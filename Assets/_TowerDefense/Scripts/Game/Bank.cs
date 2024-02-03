using TowerDefense.Tower;
using UnityEngine;

namespace TowerDefense.Managers
{
	public class Bank : MonoBehaviour
	{
		public static Bank Instance;
	
		[SerializeField] private int _startingBalance = 100;
		[SerializeField] private int _currentBalance;

	
		public int CurrentBalance
		{
			get => _currentBalance;
			set
			{
				_currentBalance = value;
				UIManager.Instance.UpdateGoldBalanceUI(_currentBalance);
			}
		}


		private void Awake()
		{
			Instance = this;
		}

	
		private void Start()
		{
			CurrentBalance = _startingBalance;
		}

	
		public void AddToBalance(int amount)
		{
			CurrentBalance += amount;
		}

		
		public void DetractFromBalance(int amount)
		{
			if (CurrentBalance >= amount)
			{
				CurrentBalance -= amount;
			}
		}

		
		public bool CanAffordTower(int towerCost)
		{
			if (towerCost <= CurrentBalance) return true;

			return false;
		}
	}
}
