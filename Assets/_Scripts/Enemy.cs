using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _resourceAmountDroppedOnDeath = 25;

    [SerializeField] private Health _healthComponent;
    private CurrencyManager _currencyManager;


    private void Start()
    {
        _healthComponent.OnEntityDied += DropResources;
        _currencyManager = FindObjectOfType<CurrencyManager>();
    }

    private void DropResources()
    {
        _currencyManager.AddToBalance(_resourceAmountDroppedOnDeath);
    }
}
