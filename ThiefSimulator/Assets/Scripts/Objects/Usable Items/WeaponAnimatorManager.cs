using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimatorManager : MonoBehaviour
{
    private Animator weaponAnimator;

    [Header("Weapon FXs")]
    public GameObject pistolBarrelSmokeFX; // Barrel smoke FX 
    public GameObject pistolEmptyBulletFX; // Empty bullet ejected from pistol when fired 

    [Header("Weapon FX Locations")]
    public Transform pistolBarrelSmokeTransform; // Location of the barrel smoke
    public Transform pistolEmptyBulletTransform; // Location of the empty bullet casing ejection 

    private void Awake() {
        weaponAnimator = GetComponentInChildren<Animator>();
    }

    public void ShootWeapon(CameraController cameraController) {
        // Plays the pistol animation 
        weaponAnimator.Play("PistolShoot");

        // Instantiates smoke FX at pistol barrel and empty bullet along pistol and aimed target
        // After a while, erase it from the game scene 
        GameObject barrelSmoke = Instantiate(pistolBarrelSmokeFX, pistolBarrelSmokeTransform.position, pistolBarrelSmokeTransform.rotation);
        GameObject bullet = Instantiate(pistolEmptyBulletFX, pistolEmptyBulletTransform.position, pistolEmptyBulletTransform.rotation);
        
        barrelSmoke.transform.parent = null;
        bullet.transform.parent = null;
        // Actually shoots at intended target (using Raycast?) - need to find documentation again
        RaycastHit hit;
        if (Physics.Raycast(cameraController.cameraObject.transform.position, cameraController.cameraObject.transform.forward, out hit)) {
            Debug.Log(hit.transform.gameObject.name);
        }
    }
}
