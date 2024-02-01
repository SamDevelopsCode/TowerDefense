using System;
using System.Collections;
using System.Collections.Generic;
using _TowerDefense.Towers;
using TMPro;
using UnityEngine;

public class TowerStatsUI : MonoBehaviour
{
    [SerializeField] private TowerManager _towerManager;
    [SerializeField] private TextMeshProUGUI _towerCostLabel;
    [SerializeField] private TextMeshProUGUI _towerRangeLabel;
    [SerializeField] private TextMeshProUGUI _towerDamageLabel;
    [SerializeField] private TextMeshProUGUI _towerShotPerSecondLabel;
    
    private void Awake()
    {
        _towerManager.TowerSelected += OnTowerSelected;
    }

    private void OnTowerSelected(TowerData towerData)
    {
        _towerCostLabel.text = $"{towerData.towerCost}";
        _towerRangeLabel.text = $"{towerData.towerRange}";
        _towerDamageLabel.text = $"{towerData.damagePerShot}";
        _towerShotPerSecondLabel.text = $"{towerData.shotsPerSecond}";
    }
}
