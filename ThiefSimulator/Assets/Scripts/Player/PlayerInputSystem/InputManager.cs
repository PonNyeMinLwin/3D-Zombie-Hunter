using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    InputControls inputControls;
    AnimationController animationController;
    Animator animator;
    PlayerManager playerManager;
    PlayerUIManager playerUIManager;

    [Header("Player Movement")]
    public float verticalMovementInput;
    public float horizontalMovementInput;
    private Vector2 movementInput;

    [Header("Camera Rotation")]
    public float verticalCameraInput;
    public float horizontalCameraInput;
    private Vector2 cameraInput;

    [Header("Inputted Actions")]
    public bool runInput;
    public bool turnInput;
    public bool aimInput;
    public bool shootInput;

    private void Awake() {
        animationController = GetComponent<AnimationController>();
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
        playerUIManager = FindObjectOfType<PlayerUIManager>();
    }

    private void OnEnable() {
        if (inputControls == null) {
            inputControls = new InputControls();

            //Gets player input (WASD) and assigns it to a Vector2 variable
            inputControls.Player.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            inputControls.Player.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            // Sees if Shift Key is pressed
            inputControls.Player.Sprint.performed += i => runInput = true;
            inputControls.Player.Sprint.canceled += i => runInput = false;

            // Sees if Q Key is pressed
            inputControls.Player.Turn.performed += i => turnInput = true;

            // Sees if right mouse button is pressed
            inputControls.PlayerCombat.Aim.performed += i => aimInput = true;
            inputControls.PlayerCombat.Aim.canceled += i => aimInput = false;

            // Sees if left mouse button is pressed
            inputControls.PlayerCombat.Shoot.performed += i => shootInput = true;
            inputControls.PlayerCombat.Shoot.canceled += i => shootInput = false;
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
        ManageAimInput();
        ManageShootInput();
    }

    private void ManageMovementInput() {
        horizontalMovementInput = movementInput.x;
        verticalMovementInput = movementInput.y;
        animationController.ManageAnimatorFloatValues(horizontalMovementInput, verticalMovementInput, runInput);

        // Temporary 
        //if (verticalMovementInput != 0 || horizontalMovementInput != 0) {
            //animationController.rightHandIK.weight = 0;
            //animationController.leftHandIK.weight = 0;
        //} else {
            //animationController.rightHandIK.weight = 1;
            //animationController.leftHandIK.weight = 1;
        //}
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

    private void ManageAimInput() {
        // Player cannot aim while moving
        if (verticalMovementInput != 0 || horizontalMovementInput != 0) {
            aimInput = false;
            animator.SetBool("isAimingGun", false);
            playerUIManager.crosshair.SetActive(false);
            return;
        }

        if (aimInput) {
            animator.SetBool("isAimingGun", true);
            playerUIManager.crosshair.SetActive(true);
        } else {
            animator.SetBool("isAimingGun", false);
            playerUIManager.crosshair.SetActive(false);
        }

        animationController.UpdateAimConstraints();
    }

    private void ManageShootInput() {
        // In the future, this function will also check what kind of weapon (gun, knife, bomb)
        // Player can only shoot when in aiming mode
        if (shootInput && aimInput) {
            shootInput = false;
            Debug.Log("Gun shot!");
            playerManager.UseCurrentWeapon();
        }
    }
}
