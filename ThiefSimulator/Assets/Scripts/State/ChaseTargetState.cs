using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTargetState : State
{
    public override State StateSwitchCheck(ZombieController zombieController) {
        Debug.Log("Found target. Running ChaseTargetState.");
        return this;
    }
}
