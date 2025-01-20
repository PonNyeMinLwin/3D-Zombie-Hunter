using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitboxManager : MonoBehaviour
{
    private ZombieController zombieController;

    private void Awake() {
        zombieController = GetComponent<ZombieController>();
    }

    public void DamageZombieHeadHitbox(int damage) {
        // Hit to the head is a critical hit and staggers 
        // 2 critical hits = death
        zombieController.isPerformingAction = true;
        zombieController.animator.CrossFade("FaceHit", 0.2f);
        zombieController.zombieHealthManager.DealHeadHitboxDamage(damage);

        // (Future) play blood FX at bullet contact point
        // (Future) play audio source within contact
    }

    public void DamageZombieTorsoHitbox(int damage) {
        // Hit to torso (deal 25 dmg?) 5 = death
        zombieController.isPerformingAction = true;
        zombieController.animator.CrossFade("BodyHit", 0.2f);
        zombieController.zombieHealthManager.DealTorsoHitboxDamage(damage);

        // (Future) play blood FX at bullet contact point
        // (Future) play audio source within contact
    }

    public void DamageZombieNormalHitbox(int damage) {
        Debug.Log("Hit normal hitbox!");
        zombieController.zombieHealthManager.DealNormalHitboxDamage(damage);
    }
}
