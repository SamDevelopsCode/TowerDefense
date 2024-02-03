using System;
using System.Collections;
using System.Collections.Generic;
using _TowerDefense.Towers;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectedTowerHighlighter : MonoBehaviour
{
    [SerializeField] private TowerManager _towerManager;
    [SerializeField] private Transform _selectionIndicators;

    private GameObject[] _selectors;
   

    private void Awake()
    {
        _towerManager.TowerSelected += OnTowerSelected;
    }

    
    private void Start()
    {
        _selectors = new GameObject[_selectionIndicators.childCount];

        for (int i = 0; i < _selectionIndicators.childCount; i++)
        {
            _selectors[i] = _selectionIndicators.GetChild(i).gameObject;
        }
    }

    
    private void OnTowerSelected(TowerData towerData)
    {
        foreach (GameObject selector in _selectors)
        {
            selector.SetActive(false);
        }
        
        if (towerData.towerType == TowerData.TowerType.Ballista)
        {
            _selectors[0].SetActive(true);
        }
        else if (towerData.towerType == TowerData.TowerType.Fire)
        {
            _selectors[1].SetActive(true);
        }
        else if (towerData.towerType == TowerData.TowerType.Lightning)
        {
            _selectors[2].SetActive(true);
        }
        else
        {
            Debug.Log("ERROR: SelectedTowerType not a valid selection.");
        }
    }
}
