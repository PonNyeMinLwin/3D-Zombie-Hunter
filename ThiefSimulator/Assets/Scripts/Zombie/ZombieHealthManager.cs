using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthManager : MonoBehaviour
{
    // This class will go through a change when I implement different rounds 
    // The damage multipliers will become lower as the rounds increase (this is why they are public variables)
    // Might also make specific healths for arm & leg colliders - after their health is gone (not overall health) they will go to CrawlState (in the future)

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
        }
    }
}
