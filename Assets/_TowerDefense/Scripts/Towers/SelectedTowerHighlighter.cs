using _TowerDefense.Towers;
using UnityEngine;

public class SelectedTowerHighlighter : MonoBehaviour
{
    [SerializeField] private TowerManager _towerManager;
    [SerializeField] private Transform _selectionIndicators;

    private GameObject[] _selectors;

    private void OnEnable()
    {
        _towerManager.TowerTypeSelected += OnTowerTypeSelected;
        _towerManager.TowerPlacementFailed += DisableChildrenSelectors;
        _towerManager.TowerPlacementSucceeded += DisableChildrenSelectors;
    }

    
    private void OnDisable()
    {
        _towerManager.TowerTypeSelected -= OnTowerTypeSelected;
        _towerManager.TowerPlacementFailed -= DisableChildrenSelectors;
        _towerManager.TowerPlacementSucceeded -= DisableChildrenSelectors;
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

    
    public void DisableChildrenSelectors()
    {
        foreach (GameObject selector in _selectors)
        {
            selector.SetActive(false);
        }
    }

    
    private void OnTowerTypeSelected(TowerStats towerStats)
    {
        if (towerStats == null)
        {
            DisableChildrenSelectors();
            return;
        }
        
        DisableChildrenSelectors();
        
        switch (towerStats.towerType)
        {
            case TowerStats.TowerType.Ballista:
                _selectors[0].SetActive(true);
                break;
            case TowerStats.TowerType.Fire:
                _selectors[1].SetActive(true);
                break;
            case TowerStats.TowerType.Lightning:
                _selectors[2].SetActive(true);
                break;
            default:
                Debug.Log("ERROR: SelectedTowerType not a valid type of tower.");
                break;
        }
    }

}
