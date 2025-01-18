using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDelayDestroyer : MonoBehaviour
{
    public float secondsBeforeDestroy = 5;
    
    private void Awake() {
        Destroy(gameObject, secondsBeforeDestroy);
    }
}
