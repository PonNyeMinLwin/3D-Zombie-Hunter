using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputManager inputManager;
    PlayerManager playerManager;

    public Rigidbody playerRigidbody;

    [Header("Camera Location")]
    public Transform cameraHolderTransform;

    [Header("Movement Speed")]
    public float rotationSpeed = 3.5f;
    public float turnSpeed = 8;

    [Header("Rotation Variables")]
    Quaternion targetRotation; // Place to rotate
    Quaternion playerRotation; // Current player's rotation 

    private void Awake() {
        playerRigidbody = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        playerManager = GetComponent<PlayerManager>();
    }

    public void ManageAllMovement() {
        ManageRotation();
        //ManageFalling();
    }

    private void ManageRotation() {
        targetRotation = Quaternion.Euler(0, cameraHolderTransform.eulerAngles.y, 0);
        playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Ensures the player only rotates when there is movement input present
        if (inputManager.verticalMovementInput != 0 || inputManager.horizontalMovementInput != 0) {
            transform.rotation = playerRotation;
        }

        if (playerManager.isPerformingTurn) {
            playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            transform.rotation = playerRotation;
        }
    }
}
