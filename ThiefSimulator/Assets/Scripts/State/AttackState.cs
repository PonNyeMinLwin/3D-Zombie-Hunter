using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private ChaseTargetState chaseTargetState;

    [Header("All Zombie Attack Types")]
    // Keeps an array of all the different zombie attack types
    public ZombieAttackTypeManager[] zombieAttackTypes;

    [Header("Currently Playable Zombie Attacks")]
    // After I add more types of attack (in the future?), zombies will choose a potential attack
    // This attack will have passed through all direction and distance checks and have deemed possible 
    // A random attack type will be chosen from this pool
    public List<ZombieAttackTypeManager> currentlyPlayableAttacks;

    [Header("Attack Performing Now")]
    public ZombieAttackTypeManager currentAttack;

    [Header("Attack Action Checks")]
    public bool hasPerformedAttack;

    private void Awake() {
        chaseTargetState = GetComponent<ChaseTargetState>();
    }

    public override State StateSwitchCheck(ZombieController zombieController) {
        // Stops the walking animation and starts attack animations 
        zombieController.animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);

        // If the zombie attacks or is staggered, pause state and reset animator
        if (zombieController.isPerformingAction) {
            zombieController.animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
            return this;
        }

        if (!hasPerformedAttack && zombieController.attackCoolDown <= 0) {
            if (currentAttack == null) {
                // If no attack is found, check all attacks and find viable attack in currenlyPlayableAttacks list
                CheckNewAttack(zombieController);
            } else {
                AttackCurrentTarget(zombieController);
            }
        }

        if (hasPerformedAttack) {
            ResetBools();
            return chaseTargetState;
        } else {
            return this;
        }
    }

    private void CheckNewAttack(ZombieController zombieController) {
        for (int i = 0; i < zombieAttackTypes.Length; i++) {
            // Puts a variable zombieAttack on all attack types in the array list
            ZombieAttackTypeManager zombieAttack = zombieAttackTypes[i];

            // Check for min and max attack distances in order to see if this attack can be performed 
            if (zombieController.distanceFromCurrentTarget <= zombieAttack.maxAttackDistance && zombieController.distanceFromCurrentTarget >= zombieAttack.minAttackDistance) {
                // Check for min and max attack directions in order to see if this attack can be performed 
                if (zombieController.directionFromCurrentTarget <= zombieAttack.maxAttackDirection && zombieController.directionFromCurrentTarget >= zombieAttack.minAttackDirection) {
                    // This attack completed all checks - add it to currently playable attacks 
                    currentlyPlayableAttacks.Add(zombieAttack);
                }
            }
        }
        // For future purposes - randomly choose an attack from the playable attacks list and use it
        int randomValue = Random.Range(0, currentlyPlayableAttacks.Count);

        if (currentlyPlayableAttacks.Count >= 0) {
            currentAttack = currentlyPlayableAttacks[randomValue];
            currentlyPlayableAttacks.Clear();
        }
    }

    private void AttackCurrentTarget(ZombieController zombieController) {
        if (currentAttack != null) {
            hasPerformedAttack = true;
            zombieController.attackCoolDown = currentAttack.attackCoolDownTimer;
            zombieController.zombieAnimationManager.PlayAttackAnimation(currentAttack.attackAnimation);
        } else {
            Debug.LogWarning("Zombie can attack but no playable attacks available!");
        }
    }

    private void ResetBools() {
        hasPerformedAttack = false;
    }
}
