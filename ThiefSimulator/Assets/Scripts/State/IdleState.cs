using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private ChaseTargetState chaseTargetState;
    
    [SerializeField] private bool findsTarget;

    public void Awake() {
        chaseTargetState = GetComponent<ChaseTargetState>();
    }
    public override State StateSwitchCheck() {
        // Make the zombie idle until they find a potential target
        // If a target is found, go to ChaseTarget state - if no target is found, zombie stays idle 
        if (findsTarget) {
            // Found target!
            return chaseTargetState;
        } else {
            return this;
        }
    }
}
