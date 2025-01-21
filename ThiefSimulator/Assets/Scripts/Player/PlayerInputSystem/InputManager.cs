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

    [Header("Input Bool Flags")]
    public bool runInput;
    public bool turnInput;
    public bool aimInput;
    public bool shootInput;
    public bool reloadInput;

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

            // Sees if R Key is pressed
            inputControls.PlayerCombat.Reload.performed += i => reloadInput = true;
            inputControls.PlayerCombat.Reload.canceled += i => reloadInput = false;
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
        ManageReloadInput();
    }

    private void ManageMovementInput() {
        horizontalMovementInput = movementInput.x;
        verticalMovementInput = movementInput.y;
        animationController.ManageAnimatorFloatValues(horizontalMovementInput, verticalMovementInput, runInput);
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
            turnInput = false;
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

    private void ManageReloadInput() {
        // Reload can't interrupt other animations
        //if (playerManager.isPerformingInput) 
            //return;
        

        if (reloadInput) {
            reloadInput = false;

            if (playerManager.playerWeaponManager.weapon.ammoLeftInWeapon == playerManager.playerWeaponManager.weapon.maxAmmoCapacity) {
                Debug.Log("Ammo already full. Cannot reload!");
                return;
            }

            // Check if there is ammo in our player reloadable inventory 
            if (playerManager.inventoryManager.currentAmmoInReloadableInventory != null) {
                int ammoNeeded = playerManager.playerWeaponManager.weapon.maxAmmoCapacity - playerManager.playerWeaponManager.weapon.ammoLeftInWeapon;
                int spareAmmo = playerManager.inventoryManager.currentAmmoInReloadableInventory.currentAmmoInBox;
                
                // If there are no reserve ammo, don't play reload animation
                if (playerManager.inventoryManager.currentAmmoInReloadableInventory.currentAmmoInBox == 0)
                    return;

                int ammoAmountTakenFromInventory;
                ammoAmountTakenFromInventory = playerManager.playerWeaponManager.weapon.maxAmmoCapacity - playerManager.playerWeaponManager.weapon.ammoLeftInWeapon;
                // e.g. 10 bullets are left in pistol and player reloads - ammo in reloadable inventory (-2), ammo in gun (+2)

                // If player have reloadable ammo in inventory, we take it from there
                if (spareAmmo >= ammoNeeded) {
                    playerManager.playerWeaponManager.weapon.ammoLeftInWeapon += ammoNeeded;
                    playerManager.inventoryManager.currentAmmoInReloadableInventory.currentAmmoInBox -= ammoNeeded;
                } 
                // If player don't have enough reloadable ammo, take everything and put it in current weapon's magazine 
                else { 
                    playerManager.playerWeaponManager.weapon.ammoLeftInWeapon += spareAmmo;
                    playerManager.inventoryManager.currentAmmoInReloadableInventory.currentAmmoInBox = 0;
                }

                // Plays reload animation 
                playerManager.animationController.PlayAnimationWithRootMotions("Reload", true);

                // Takes the ammo from player's reserves to currently holding weapon's ammo count (in UI)
                playerManager.playerUIManager.gunAmmoCountText.text = playerManager.playerWeaponManager.weapon.ammoLeftInWeapon.ToString();
                playerManager.inventoryManager.UpdateSpareAmmoUI();
            } else {
                Debug.Log("No spare inventory to reload!");
            }
        }
    }
}
