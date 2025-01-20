using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Animator animator;
    private InputManager inputManager;

    public AnimationController animationController;
    public CameraController cameraController;
    public PlayerController playerController;
    public PlayerUIManager playerUIManager;
    public PlayerWeaponManager playerWeaponManager;
    public InventoryManager inventoryManager;
    public PlayerHealthManager playerHealthManager;
    

    [Header("Player Actions")]
    public bool disableRootMotion;
    public bool isPerformingInput;
    public bool isPerformingTurn;
    public bool isAimingGun;
    public bool isDead;

    private void Awake() {
        inputManager = GetComponent<InputManager>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        animationController = GetComponent<AnimationController>();
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
        cameraController = FindObjectOfType<CameraController>();
        playerUIManager = FindObjectOfType<PlayerUIManager>();
        inventoryManager = GetComponent<InventoryManager>();
        playerHealthManager = GetComponent<PlayerHealthManager>();
    }

    private void Update() {
        inputManager.ManageAllInputs();

        disableRootMotion = animator.GetBool("disableRootMotion");
        isPerformingInput = animator.GetBool("isPerformingInput");
        isPerformingTurn = animator.GetBool("isTurning");
        isAimingGun = animator.GetBool("isAimingGun");
        animator.SetBool("isDead", isDead);
    }

    private void FixedUpdate() {
        playerController.ManageAllMovement();
    }

    private void LateUpdate() {
        cameraController.AllCameraMovements();
    }

    public void UseCurrentWeapon() {
        // For future development, this function can be updated to use different kinds of weapons (like knives or grenades)
        // If there is ammo in gun, shoot weapon and minus ammo count (also from UI)
        if (playerWeaponManager.weapon.ammoLeftInWeapon > 0) {
            playerWeaponManager.weapon.ammoLeftInWeapon = playerWeaponManager.weapon.ammoLeftInWeapon - 1;
            playerUIManager.gunAmmoCountText.text = playerWeaponManager.weapon.ammoLeftInWeapon.ToString();
            animationController.PlayAnimationWithoutRootMotions("Shoot", true);
            playerWeaponManager.weaponAnimator.ShootWeapon(cameraController);
        } else {
            Debug.Log("Out of ammo! R to reload.");
        }
    }
}
