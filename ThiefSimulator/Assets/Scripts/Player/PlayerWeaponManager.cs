using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    private AnimationController animationController;
    private PlayerEquipmentSpawner playerEquipmentSpawner;

    [Header("Current Equipment")]
    public WeaponItems weapon;
    public WeaponAnimatorManager weaponAnimator;
    private RightHandIKTarget rightHandIK;
    private LeftHandIKTarget leftHandIK;

    private void Awake() {
        animationController = GetComponent<AnimationController>();
        LoadWeaponSlots();
    }

    private void Start() {
        LoadCurrentWeapon();
    }

    private void LoadWeaponSlots() {
        // Back Slots
        // Hip Slots

        playerEquipmentSpawner = GetComponentInChildren<PlayerEquipmentSpawner>();
    }

    private void LoadCurrentWeapon() {
        // Loading the weapon on the player's hand 
        playerEquipmentSpawner.LoadWeaponModel(weapon);
        // Loading the relevant constraints and animations 
        animationController.animator.runtimeAnimatorController = weapon.weaponAnimator;
        weaponAnimator = playerEquipmentSpawner.currentWeaponModel.GetComponentInChildren<WeaponAnimatorManager>();
        rightHandIK = playerEquipmentSpawner.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();
        leftHandIK = playerEquipmentSpawner.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
        animationController.AssignHandIK(rightHandIK, leftHandIK); 
    }
}
