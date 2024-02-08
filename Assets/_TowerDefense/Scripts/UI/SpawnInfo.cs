using System.Collections;
using System.Collections.Generic;
using _TowerDefense.Towers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SpawnInfo : MonoBehaviour
{
    public Image spawnImage;
    public TextMeshProUGUI spawnCount;

    public void SetSpawnInfoUI(Sprite sprite, int count)
    {
        spawnImage.sprite = sprite;
        spawnCount.text = count.ToString();
    }
}
