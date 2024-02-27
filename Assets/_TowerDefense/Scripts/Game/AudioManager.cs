using System;
using System.Collections;
using System.Collections.Generic;
using _TowerDefense.Towers;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private TowerManager _towerManager;
    [SerializeField] private SoundReferences soundReferences;
    
    
    private void OnEnable()
    {
        _towerManager.TowerFiredShot += OnTowerFiredShot;
        _towerManager.TowerPlacementSucceeded += OnTowerPlacementSucceeded;
        _towerManager.TowerPlacementFailed += OnTowerPlacementFailed;
        _towerManager.TowerUpgraded += OnTowerUpgraded;
        _towerManager.TowerUpgradeFailed += OnTowerUpgradeFailed;
    }

    
    private void OnTowerUpgradeFailed()
    {
        PlaySound(soundReferences.errors, Camera.main.transform.position);
    }


    private void OnTowerUpgraded()
    {
        PlaySound(soundReferences.towerUpgrades, Camera.main.transform.position, .1f);
    }


    private void OnTowerPlacementFailed()
    {
        PlaySound(soundReferences.errors, Camera.main.transform.position);
    }

    
    private void OnTowerPlacementSucceeded()
    {
        PlaySound(soundReferences.towerPlacements, Camera.main.transform.position);
    }


    private void OnTowerFiredShot(Tower tower)
    {
        if (tower.towerStats.towerType == TowerStats.TowerType.Ballista)
        {
            PlaySound(soundReferences.arrowAttacks, Camera.main.transform.position);
        }
        else if (tower.towerStats.towerType == TowerStats.TowerType.Fire)
        {
            PlaySound(soundReferences.fireAttacks, Camera.main.transform.position);
        }
        else if (tower.towerStats.towerType == TowerStats.TowerType.Lightning)
        {
            PlaySound(soundReferences.lightningAttacks, Camera.main.transform.position);
        }
    }


    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);    
    }
    
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
