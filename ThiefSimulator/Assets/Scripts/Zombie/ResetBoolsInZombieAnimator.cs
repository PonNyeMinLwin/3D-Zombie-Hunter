using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBoolsInZombieAnimator : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ZombieController zombieController = animator.GetComponent<ZombieController>();

        if (zombieController != null) {
            zombieController.isPerformingAction = false;
        }
    }
}
