using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerCollection", menuName = "TowerCollections / Create New Collection")]
public class TowerCollection : ScriptableObject
{
    public List<GameObject> towers = new();
}
