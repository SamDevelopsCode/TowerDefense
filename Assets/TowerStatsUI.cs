using System;
using System.Collections;
using System.Collections.Generic;
using _TowerDefense.Towers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerStatsUI : MonoBehaviour
{
    [SerializeField] private TowerManager _towerManager;
    [SerializeField] private Image _towerIconImage;
    [SerializeField] private TextMeshProUGUI _towerNameLabel;
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
        _towerIconImage.enabled = true;
        _towerIconImage.sprite = towerData.icon;
        _towerNameLabel.text = $"{towerData.name}";
        _towerCostLabel.text = $"{towerData.cost}";
        _towerRangeLabel.text = $"{towerData.range}";
        _towerDamageLabel.text = $"{towerData.damagePerShot}";
        _towerShotPerSecondLabel.text = $"{towerData.shotsPerSecond}";
    }
}
