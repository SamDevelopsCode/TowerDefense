using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWith : MonoBehaviour
{

    public event Action OnCollisionEvent;
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("I collided with " + name);
        OnCollisionEvent?.Invoke();
    }
}
