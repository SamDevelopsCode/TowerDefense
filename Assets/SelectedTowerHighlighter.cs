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
        GetChildrenSelectors();
    }

    
    private void GetChildrenSelectors()
    {
        _selectors = new GameObject[_selectionIndicators.childCount];

        for (int i = 0; i < _selectionIndicators.childCount; i++)
        {
            _selectors[i] = _selectionIndicators.GetChild(i).gameObject;
        }
    }

    
    private void DisableChildrenSelectors()
    {
        foreach (GameObject selector in _selectors)
        {
            selector.SetActive(false);
        }
    }

    
    private void OnTowerSelected(TowerData towerData)
    {
        DisableChildrenSelectors();

        switch (towerData.towerType)
        {
            case TowerData.TowerType.Ballista:
                _selectors[0].SetActive(true);
                break;
            case TowerData.TowerType.Fire:
                _selectors[1].SetActive(true);
                break;
            case TowerData.TowerType.Lightning:
                _selectors[2].SetActive(true);
                break;
            default:
                Debug.Log("ERROR: SelectedTowerType not a valid type of tower.");
                break;
        }
    }

}
