using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBank : MonoBehaviour
{
    [SerializeField] private int _startingBalance = 100;

    [SerializeField] private int _currentBalance;
    public int CurrentBalance { get; private set; }


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
