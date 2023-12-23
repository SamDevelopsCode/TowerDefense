using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _resourceAmountDroppedOnDeath = 25;


    [SerializeField] private Health healthComponent;
    private ResourceBank _resourceBank;


    private void Start()
    {
        healthComponent.OnEntityDied += DropResources;
        _resourceBank = FindObjectOfType<ResourceBank>();
    }

    private void DropResources()
    {
        _resourceBank.AddToBalance(_resourceAmountDroppedOnDeath);
    }
}
