using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthManager : MonoBehaviour
{
    // This class will go through a change when I implement different rounds 
    // The damage multipliers will become lower as the rounds increase (this is why they are public variables)
    // Might also make specific healths for arm & leg colliders - after their health is gone (not overall health) they will go to CrawlState (in the future)

    public GameLogicManager gameLogicManager;
    private ZombieController zombie;

    [Header("Zombie Total Health")]
    public int overallHealth = 100; // If this reaches 0, the zombie will die

    [Header("Damage Multipliers")]
    public int headHitboxDamageMultiplier = 50;
    public int torsoHitboxDamageMultiplier = 20; 
    public int normalHitboxDamageMultiplier = 10;

    private void Awake() {
        zombie = GetComponent<ZombieController>();
    }

    public void DealHeadHitboxDamage(int damage) {
        overallHealth = overallHealth - (damage * headHitboxDamageMultiplier); 
        DeathCheck();
    }

    public void DealTorsoHitboxDamage(int damage) {
        overallHealth = overallHealth - (damage * torsoHitboxDamageMultiplier);
        DeathCheck();
    }

    public void DealNormalHitboxDamage(int damage) {
        overallHealth = overallHealth - (damage * normalHitboxDamageMultiplier);
        DeathCheck();
    }

    private void DeathCheck() {
        if (overallHealth <= 0) {
            zombie.isDead = true;
            zombie.zombieAnimationManager.PlayDeathAnimation("ZombieDeath");
            // Starts a co-routine to manage zombie's death body
            StartCoroutine(ManageZombieDeath());
        }
    }
    
    private IEnumerator ManageZombieDeath() {
        // Gets the length of zombie's death animation to wait before destroying
        Animator animator = zombie.GetComponent<Animator>();
        if (animator != null) {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            float deathAnimationLength = stateInfo.length;
            
            // Disable zombie colliders
            DisableZombie();
            
            // Wait for death animation to finish and destroy
            yield return new WaitForSeconds(deathAnimationLength);

            // Notify GameLogicManager about zombie's death
            if (gameLogicManager != null) {
                gameLogicManager.ManageZombieKillCount();
            }
            // Delete the zombie
            Destroy(gameObject);
        } else {
            Debug.LogWarning("Animator not found on zombie. Destroying immediately.");
            Destroy(gameObject);
        }
    }
    
    private void DisableZombie() {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (var collider in colliders) {
            collider.enabled = false;
        }
    }
}
