using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    InputControls inputControls;
    AnimationController animationController;
    Animator animator;
    PlayerManager playerManager;

    [Header("Player Movement")]
    public float verticalMovementInput;
    public float horizontalMovementInput;
    private Vector2 movementInput;

    [Header("Camera Rotation")]
    public float verticalCameraInput;
    public float horizontalCameraInput;
    private Vector2 cameraInput;

    [Header("Extra Movement Settings")]
    public bool isRunning;
    public bool turnInput;

    private void Awake() {
        animationController = GetComponent<AnimationController>();
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
    }

    private void OnEnable() {
        if (inputControls == null) {
            inputControls = new InputControls();

            //Gets player input (WASD) and assigns it to a Vector2 variable
            inputControls.Player.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            inputControls.Player.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            // Sees if Shift Key is pressed
            inputControls.Player.Sprint.performed += i => isRunning = true;
            inputControls.Player.Sprint.canceled += i => isRunning = false;

            // Sees if Q Key is pressed
            inputControls.Player.Turn.performed += i => turnInput = true;
        }

        inputControls.Enable();
    }

    private void OnDisable() {
        inputControls.Disable();
    }

    public void ManageAllInputs() {
        ManageMovementInput();
        ManageCameraInput();
        ManageTurnInput();
    }

    private void ManageMovementInput() {
        horizontalMovementInput = movementInput.x;
        verticalMovementInput = movementInput.y;
        animationController.ManageAnimatorFloatValues(horizontalMovementInput, verticalMovementInput, isRunning);

        // Temporary 
        if (verticalMovementInput != 0 || horizontalMovementInput != 0) {
            animationController.rightHandIK.weight = 0;
            animationController.leftHandIK.weight = 0;
        } else {
            animationController.rightHandIK.weight = 1;
            animationController.leftHandIK.weight = 1;
        }
    }

    private void ManageCameraInput() {
        horizontalCameraInput = cameraInput.x;
        verticalCameraInput = cameraInput.y;
    }

    private void ManageTurnInput() {
        if (playerManager.isPerformingInput) {
            return;
        }

        if (turnInput) {
            animator.SetBool("isTurning", true);
            animationController.PlayAnimationWithoutRootMotions("Turn", true);
        }
    }
}
