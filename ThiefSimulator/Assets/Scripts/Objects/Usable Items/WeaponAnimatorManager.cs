using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimatorManager : MonoBehaviour
{
    private PlayerManager player;
    private Animator weaponAnimator;

    [Header("Weapon FXs")]
    public GameObject pistolBarrelSmokeFX; // Barrel smoke FX 
    public GameObject pistolEmptyBulletFX; // Empty bullet ejected from pistol when fired 

    [Header("Weapon FX Locations")]
    public Transform pistolBarrelSmokeTransform; // Location of the barrel smoke
    public Transform pistolEmptyBulletTransform; // Location of the empty bullet casing ejection 

    [Header("Weapon Hitbox Layers")]
    public LayerMask hitboxLayers;

    [Header("Weapon Bullet Stats")]
    public float bulletRange = 300f;

    [Header("Audio Source for Weapon Sounds")]
    public AudioSource weaponAudioSource; 

    private void Awake() {
        weaponAnimator = GetComponentInChildren<Animator>();
        player = GetComponentInParent<PlayerManager>();
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

        // Play weapon fire sound
        if (player.playerWeaponManager.weapon.weaponShotAudio != null && weaponAudioSource != null) {
            weaponAudioSource.PlayOneShot(player.playerWeaponManager.weapon.weaponShotAudio);
        } 

        // Actually shoots at intended target using Raycast
        RaycastHit hit;

        if (Physics.Raycast(cameraController.cameraObject.transform.position, cameraController.cameraObject.transform.forward, out hit, bulletRange, hitboxLayers)) {
            Debug.Log(hit.collider.gameObject.layer);

            ZombieHitboxManager zombie = hit.collider.gameObject.GetComponentInParent<ZombieHitboxManager>();

            if (zombie != null) {
                if (hit.collider.gameObject.layer == 9) {
                    zombie.DamageZombieHeadHitbox(player.playerWeaponManager.weapon.damage);
                }
                else if (hit.collider.gameObject.layer == 10) {
                    zombie.DamageZombieTorsoHitbox(player.playerWeaponManager.weapon.damage);
                } 
                else if (hit.collider.gameObject.layer == 11) {
                    zombie.DamageZombieNormalHitbox(player.playerWeaponManager.weapon.damage);
                }
            }
        }
    }
}
