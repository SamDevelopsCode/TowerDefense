using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
	
	public static CurrencyManager Instance;
	
	[SerializeField] private int _startingBalance = 100;

	[SerializeField] private int _currentBalance;
	public int CurrentBalance { get { return _currentBalance; } }


	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		_currentBalance = _startingBalance;
	}

	public void AddToBalance(int amount)
	{
		_currentBalance += amount;
	}

	public void DetractFromBalance(int amount)
	{
		if (_currentBalance >= amount)
		{
			_currentBalance -= amount;
		}
	}

	
	
	
}
