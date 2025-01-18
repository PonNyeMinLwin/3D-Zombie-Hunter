using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager inputManager;
    private CameraController cameraController;
    private PlayerController playerController;
    private Animator animator;
    private PlayerWeaponManager playerWeaponManager;
    private AnimationController animationController;

    [Header("Player Actions")]
    public bool disableRootMotion;
    public bool isPerformingInput;
    public bool isPerformingTurn;
    public bool isAimingGun;

    private void Awake() {
        inputManager = GetComponent<InputManager>();
        cameraController = FindObjectOfType<CameraController>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        animationController = GetComponent<AnimationController>();
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
    }

    private void Update() {
        inputManager.ManageAllInputs();

        disableRootMotion = animator.GetBool("disableRootMotion");
        isPerformingInput = animator.GetBool("isPerformingInput");
        isPerformingTurn = animator.GetBool("isTurning");
        isAimingGun = animator.GetBool("isAimingGun");
    }

    private void FixedUpdate() {
        playerController.ManageAllMovement();
    }

    private void LateUpdate() {
        cameraController.AllCameraMovements();
    }

    public void UseCurrentWeapon() {
        // For future development, this function can be updated to use different kinds of weapons (like knives or grenades)
        //if (isPerformingInput) {
            //return;
        //}

        animationController.PlayAnimationWithoutRootMotions("Shoot", true);
        playerWeaponManager.weaponAnimator.ShootWeapon(cameraController);
    }
}
