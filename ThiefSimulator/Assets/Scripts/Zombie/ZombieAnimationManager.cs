using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationManager : MonoBehaviour
{
    // This class is used to actually play the animations 
    private ZombieController zombieController;

    private void Awake() {
        zombieController = GetComponent<ZombieController>();
    }

    public void PlayAttackAnimation(string attackAnimation) {
        zombieController.animator.applyRootMotion = true;
        zombieController.isPerformingAction = true;
        zombieController.animator.CrossFade(attackAnimation, 0.2f);
    }

    public void PlayDeathAnimation(string deathAnimation) {
        zombieController.animator.applyRootMotion = true;
        zombieController.isPerformingAction = true;
        zombieController.animator.CrossFade(deathAnimation, 0.2f);
    }
}
