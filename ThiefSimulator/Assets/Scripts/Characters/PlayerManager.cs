using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    CameraController cameraController;
    PlayerController playerController;
    Animator animator;

    [Header("Player Actions")]
    public bool isPerformingInput;
    public bool isPerformingTurn;

    private void Awake() {
        inputManager = GetComponent<InputManager>();
        cameraController = FindObjectOfType<CameraController>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        inputManager.ManageAllInputs();

        isPerformingInput = animator.GetBool("isPerformingInput");
        isPerformingTurn = animator.GetBool("isTurning");
    }

    private void FixedUpdate() {
        playerController.ManageAllMovement();
    }

    private void LateUpdate() {
        cameraController.AllCameraMovements();
    }
}
