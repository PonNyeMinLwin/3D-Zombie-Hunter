using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    // Zombie starts on IdleState 
    public IdleState startingState;
    // Current state of the zombie
    private State currentState;

    private void Awake() {
        currentState = startingState;
    }

    // Temporary
    private void FixedUpdate() {
        ManageStateMachine();
    }

    // Temporary
    private void ManageStateMachine() {
        State nextState;

        if (currentState != null) {
            // Run logic based on which state the zombie is currently in 
            // Also runs a check and if met - switch stages
            nextState = currentState.StateSwitchCheck();
            if (nextState != null) {
                currentState = nextState;
            }
        }
    }
}
