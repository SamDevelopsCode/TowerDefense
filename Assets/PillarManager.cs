using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarManager : MonoBehaviour
{
    private void Awake()
    {
        var childThatContainsEvent = transform.GetChild(0).GetComponent<CollideWith>();
        childThatContainsEvent.OnCollisionEvent += LocalFunctionIWantToRun;
    }

    private void LocalFunctionIWantToRun()
    {
        Debug.Log("I have recieve the event and this print is now run.");
    }
}
