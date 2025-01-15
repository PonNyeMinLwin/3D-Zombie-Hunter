using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    PlayerController playerController;

    float horizontalDirection;
    float verticalDirection;

    private void Awake() {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    public void PlayAnimationWithoutRootMotions(string targetAnimation, bool isPerformingInput) {
        animator.SetBool("isPerformingInput", isPerformingInput);
        animator.applyRootMotion = false;
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void ManageAnimatorFloatValues(float horizontalMovement, float verticalMovement, bool isRunning) {
        if (horizontalMovement > 0) { 
            horizontalDirection = 1; 
        }
        else if (horizontalMovement < 0) { 
            horizontalDirection = -1; 
        }
        else { 
            horizontalDirection = 0; 
        }

        if (verticalMovement > 0) { 
            verticalDirection = 1; 
        }
        else if (verticalMovement < 0) { 
            verticalDirection = -1; 
        }
        else { 
            verticalDirection = 0; 
        }

        if (isRunning && verticalDirection > 0) {
            verticalDirection = 2;
        }

        animator.SetFloat("Horizontal", horizontalDirection, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", verticalDirection, 0.1f, Time.deltaTime);
    }

    private void OnAnimatorMove() {
        Vector3 animatorDeltaPosition = animator.deltaPosition;
        animatorDeltaPosition.y = 0;

        Vector3 velocity = animatorDeltaPosition / Time.deltaTime;
        playerController.playerRigidbody.drag = 0;
        playerController.playerRigidbody.velocity = velocity;
        // Mimic animation root motion to player's rigidbody as well 
        transform.rotation *= animator.deltaRotation;
    }
}
