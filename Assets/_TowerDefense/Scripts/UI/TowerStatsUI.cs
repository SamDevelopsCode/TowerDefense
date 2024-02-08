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
    [SerializeField] private Color _iconBackgroundColor;
    [SerializeField] private TextMeshProUGUI _towerNameLabel;
    [SerializeField] private TextMeshProUGUI _towerCostLabel;
    [SerializeField] private TextMeshProUGUI _towerRangeLabel;
    [SerializeField] private TextMeshProUGUI _towerDamageLabel;
    [SerializeField] private TextMeshProUGUI _towerShotPerSecondLabel;
    
    
    public void SetTowerStatsUIData(TowerData towerData)
    {
        _towerIconImage.color = _iconBackgroundColor;
        _towerIconImage.sprite = towerData.icon;
        _towerNameLabel.text = $"{towerData.towerName}";
        _towerCostLabel.text = $"{towerData.cost}";
        _towerRangeLabel.text = $"{towerData.range}";
        _towerDamageLabel.text = $"{towerData.damagePerShot}";
        _towerShotPerSecondLabel.text = $"{towerData.shotsPerSecond}";
    }
}
