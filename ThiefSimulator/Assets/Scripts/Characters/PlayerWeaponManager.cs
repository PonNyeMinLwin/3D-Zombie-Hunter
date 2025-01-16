using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    private AnimationController animationController;
    private PlayerEquipmentSpawner playerEquipmentSpawner;

    [Header("Current Equipment")]
    public WeaponItems weapon;
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
        playerEquipmentSpawner.LoadWeaponModel(weapon);

        // Loading the weapon on the player's hand and changing relevant animations
        animationController.animator.runtimeAnimatorController = weapon.weaponAnimator;
        rightHandIK = playerEquipmentSpawner.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();
        leftHandIK = playerEquipmentSpawner.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
        animationController.AssignHandIK(rightHandIK, leftHandIK); 
    }
}
