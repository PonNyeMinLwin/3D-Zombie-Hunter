using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTargetState : State
{
    private AttackState attackState;

    private void Awake() {
        attackState = GetComponent<AttackState>();
    }

    public override State StateSwitchCheck(ZombieController zombieController) {
        // If the zombie attacks or is staggered, pause state and reset animator
        if (zombieController.isPerformingAction) {
            zombieController.animator.SetFloat("Vertical", 0, 0.2f, Time.deltaTime);
            return this;
        }

        Debug.Log("Found target. Running ChaseTargetState.");

        ChaseCurrentTarget(zombieController);
        RotateTowardsCurrentTarget(zombieController);

        if (zombieController.distanceFromCurrentTarget <= zombieController.minAttackDistance) {
            zombieController.zombieNavMeshAgent.enabled = false;
            return attackState;
        } else {
            return this;
        }
    }

    private void ChaseCurrentTarget(ZombieController zombieController) {
        // Enable movement of character (zombie) by changing animation blend tree values
        zombieController.animator.SetFloat("Vertical", 1, 0.2f, Time.deltaTime);
    }

    private void RotateTowardsCurrentTarget(ZombieController zombieController) {
        zombieController.zombieNavMeshAgent.enabled = true;
        zombieController.zombieNavMeshAgent.SetDestination(zombieController.currentTarget.transform.position);
        zombieController.transform.rotation = Quaternion.Slerp(zombieController.transform.rotation, zombieController.zombieNavMeshAgent.transform.rotation, zombieController.rotationSpeed / Time.deltaTime);

    }
}
