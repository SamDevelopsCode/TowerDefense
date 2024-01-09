using TowerDefense.Managers;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _resourceAmountDroppedOnDeath = 25;
    [SerializeField] private int _damageToBase = 5;
    
    [SerializeField] private Health _healthComponent;
    private CurrencyManager _currencyManager;
    private EnemyManager _enemyManager;

    public int DamageToBase
    {
        get
        {
            return _damageToBase;
        }
    }
    
    
    private void Awake()
    {
        _enemyManager = transform.parent.GetComponent<EnemyManager>();
    }

    
    private void Start()
    {
        _healthComponent.OnEntityDied += DropResources;
        _currencyManager = FindObjectOfType<CurrencyManager>();
    }

    
    private void DropResources()
    {
        _currencyManager.AddToBalance(_resourceAmountDroppedOnDeath);
        Destroy(gameObject);
    }
}
