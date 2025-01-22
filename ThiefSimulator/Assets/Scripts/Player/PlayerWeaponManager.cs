using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    private PlayerManager playerManager;
    private PlayerEquipmentSpawner playerEquipmentSpawner;

    [Header("Current Equipment")]
    public WeaponItems weapon;
    public WeaponAnimatorManager weaponAnimator;
    private RightHandIKTarget rightHandIK;
    private LeftHandIKTarget leftHandIK;

    private void Awake() {
        playerManager = GetComponent<PlayerManager>();
        LoadWeaponSlots();
    }

    private void Start() {
        LoadCurrentWeapon();
    }

    private void LoadWeaponSlots() {
        playerEquipmentSpawner = GetComponentInChildren<PlayerEquipmentSpawner>();
    }

    private void LoadCurrentWeapon() {
        // Loading the weapon on the player's hand 
        playerEquipmentSpawner.LoadWeaponModel(weapon);

        // Loading the relevant constraints and animations 
        playerManager.animationController.animator.runtimeAnimatorController = weapon.weaponAnimator;
        weaponAnimator = playerEquipmentSpawner.currentWeaponModel.GetComponentInChildren<WeaponAnimatorManager>();
        rightHandIK = playerEquipmentSpawner.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();
        leftHandIK = playerEquipmentSpawner.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
        playerManager.animationController.AssignHandIK(rightHandIK, leftHandIK); 

        // Changes the current weapon's ammo on UI screen
        playerManager.playerUIManager.gunAmmoCountText.text = weapon.ammoLeftInWeapon.ToString();

        // Checks if player have ammo in player inventory 
        if (playerManager.inventoryManager.currentAmmoInReloadableInventory != null) {
            playerManager.playerUIManager.reloadAmmoCountText.text = playerManager.inventoryManager.currentAmmoInReloadableInventory.currentAmmoInBox.ToString();
        }
    }
}
