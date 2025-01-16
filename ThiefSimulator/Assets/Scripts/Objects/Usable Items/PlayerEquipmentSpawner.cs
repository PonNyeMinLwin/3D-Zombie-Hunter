using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentSpawner : MonoBehaviour
{
    public GameObject currentWeaponModel;

    private void DestroyPreviousWeapon() {
        // If a player has a weapon model equipped, destroy it 
        if (currentWeaponModel != null) {
            Destroy(currentWeaponModel);
        }
    }

    public void LoadWeaponModel(WeaponItems weapon) {
        DestroyPreviousWeapon();

        // If the player has no weapons, do nothing - if they do: 
        if (weapon == null)
        {
            return;
        }

        // Instantiaties a weapon model on the player's right hand 
        GameObject weaponModel = Instantiate(weapon.itemModel, transform);
        weaponModel.transform.localPosition = Vector3.zero;
        weaponModel.transform.localRotation = Quaternion.identity;
        weaponModel.transform.localScale = Vector3.one;
        currentWeaponModel = weaponModel;
    }
}
