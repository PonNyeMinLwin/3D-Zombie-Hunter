using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private ChaseTargetState chaseTargetState;
    
    // Character (zombie) will detect objects with this layer
    [Header("Detection Layer")]
    [SerializeField] private LayerMask detectionLayer;
    
    // Added variable since raycast cannot detect properly when starting on the floor 
    [Header("Character Model Eye Level")]
    [SerializeField] private float characterModelHeight = 1.5f;
    [SerializeField] private LayerMask ignoreLayerForDetection;

    // How far away the character (zombie) must be to detect
    [Header("Detection Radius")]
    [SerializeField] private float detectionRadius = 30;

    // How much the character (zombie) can detect within its POV
    [Header("POV Stats")]
    [SerializeField] private float minDetectionRadiusAngle = -100f;
    [SerializeField] private float maxDetectionRadiusAngle = 180f;

    public void Awake() {
        chaseTargetState = GetComponent<ChaseTargetState>();
    }
    public override State StateSwitchCheck(ZombieController zombieController) {
        // Make the zombie idle until they find a potential target
        // If a target is found, go to ChaseTarget state - if no target is found, zombie stays idle 
        if (zombieController.currentTarget != null) {
            return chaseTargetState;
        } else {
            FindTargetViaPOV(zombieController);
            return this;
        }
    }

    private void FindTargetViaPOV(ZombieController zombieController) {
        // Only finds colliders that are in the detectionLayer 
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);

        Debug.Log("Checking for colliders...");
        
        // For every collider, this statement tries to get the PlayerManager script from that detected object
        for (int i = 0; i < colliders.Length; i++) {
            PlayerManager player = colliders[i].transform.GetComponent<PlayerManager>();

            // If the script is detected, check if this character (zombie) can "see" the target
            if (player != null) {
                Debug.Log("Found the player collider!");

                // Ensures that the character (zombie) can "see" the target
                Vector3 targetDirection = transform.position - player.transform.position;
                float targetAngle = Vector3.Angle(targetDirection, transform.forward);
                
                if (targetAngle > minDetectionRadiusAngle && targetAngle < maxDetectionRadiusAngle) {
                    Debug.Log("The player is in my POV! I can sense him.");

                    RaycastHit hit;
                    Vector3 playerStartLocation = new Vector3(player.transform.position.x, characterModelHeight, player.transform.position.z);
                    Vector3 zombieStartLocation = new Vector3(transform.position.x, characterModelHeight, transform.position.z);

                    Debug.DrawLine(playerStartLocation, zombieStartLocation, Color.yellow);

                    // Check one last time for objects between player and zombie locations that are blocking the POVs
                    if (Physics.Linecast(playerStartLocation, zombieStartLocation, out hit, ignoreLayerForDetection)) {
                        // Cannot find target - there is an object in between 
                        Debug.Log("There is an object in the way.");
                    } else {
                        // Finds target!
                        Debug.Log("Found target! Switching to ChaseTarget State.");
                        zombieController.currentTarget = player;
                    }
                }
            }
        }
    }
}
