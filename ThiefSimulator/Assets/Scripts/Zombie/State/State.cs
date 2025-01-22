using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    // This is the base class for all future states
    public virtual State StateSwitchCheck(ZombieController zombieController) {
        return this;
    }
}
