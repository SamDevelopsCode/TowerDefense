using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [SerializeField] private TextMeshProUGUI _goldBalance;
    [SerializeField] private TextMeshProUGUI _baseHealthAmount;
    [SerializeField] private TextMeshProUGUI _waveNumber;

    
    private void Awake()
    {
        Instance = this;
    }


    public void UpdateGoldBalanceUI(int currentGoldBalance)
    {
        _goldBalance.text = currentGoldBalance.ToString();
    }
    
    
    public void UpdateBaseHealthUI()
    {
        // TODO create a base and update its  health here
    }
    
    
    public void UpdateWaveNumberUI()
    {
        // TODO create waves spawning and update the current wave number here
    }
}
