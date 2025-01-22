using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamageCollider : MonoBehaviour
{
    // Putting this class on zombie's hand colliders
    private ZombieController zombie;
    public Collider zombieArmCollider;

    private void Awake() {
        zombie = GetComponentInParent<ZombieController>();
        zombieArmCollider = GetComponentInChildren<Collider>();
    }

    private void OnTriggerEnter(Collider other) {
        // Checks for any zombie arm collider hitting any colliders on Player layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            // Searches for PlayerManager script on the collider recently hit by zombie
            PlayerManager player = other.GetComponent<PlayerManager>();

            if (player != null) {
                if (!player.isPerformingInput) {
                    // Play the Player's "Get Hit" animation 
                    player.animationController.PlayAnimationWithRootMotions("Hit", true);
                    // Take away 1 health point from player
                    player.playerHealthManager.TakesDamageFromSwipe();
                    // Hit SFX plays
                }
            }
        }
    }
}
