using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public ZombieAnimationManager zombieAnimationManager;

    // Zombie starts on IdleState 
    public IdleState startingState;

    [Header("Animation State Checks")]
    public bool isPerformingAction;

    [Header("Current State")]
    [SerializeField] private State currentState;

    [Header("Current Target")]
    public PlayerManager currentTarget;
    public float distanceFromCurrentTarget;
    public Vector3 targetDirection;
    public float directionFromCurrentTarget;

    [Header("Animator")]
    public Animator animator;

    [Header("NavMesh Agent")]
    public NavMeshAgent zombieNavMeshAgent;

    [Header("Rigidbody")]
    public Rigidbody zombieRigidbody;

    [Header("Locomotion Stats")]
    public float rotationSpeed = 5;

    [Header("Attack Stats")]
    public float minAttackDistance = 1; // Putting my attack type with the shortest distance as 1
    public float maxAttackDistance = 3.5f; // Putting my attack type with the longest distance as 3.5
    public float attackCoolDown;

    private void Awake() {
        currentState = startingState;
        animator = GetComponent<Animator>();
        zombieNavMeshAgent = GetComponentInChildren<NavMeshAgent>();
        zombieRigidbody = GetComponent<Rigidbody>();
        zombieAnimationManager = GetComponent<ZombieAnimationManager>();
    }

    // Temporary
    private void FixedUpdate() {
        ManageStateMachine();
    }

    private void Update() {
        zombieNavMeshAgent.transform.localPosition = Vector3.zero;

        if (attackCoolDown > 0) {
            attackCoolDown = attackCoolDown - Time.deltaTime;
        }

        // Checking the distance between character (zombie) and player and going to AttackState
        if (currentTarget != null) {
            // Getting the current direction from player as a float value
            targetDirection = currentTarget.transform.position - transform.position;
            directionFromCurrentTarget = Vector3.SignedAngle(targetDirection, transform.forward, Vector3.up);
            // Getting the current distance from player as a float value
            distanceFromCurrentTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        }
    }

    // Temporary
    private void ManageStateMachine() {
        State nextState;

        if (currentState != null) {
            // Run logic based on which state the zombie is currently in 
            // Also runs a check and if met - switch stages
            nextState = currentState.StateSwitchCheck(this);
            if (nextState != null) {
                currentState = nextState;
            }
        }
    }
}
