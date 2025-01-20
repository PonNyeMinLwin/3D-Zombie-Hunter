using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    private PlayerController playerController;
    private PlayerManager playerManager;

    [Header("Hand IK Constraints")]
    // According to the "Animations Rigging" package documentation 
    // These constraints allow the character model to properly hold the pistol 
    public TwoBoneIKConstraint rightHandIK;
    public TwoBoneIKConstraint leftHandIK;

    [Header("Aiming Multi-Constraints")]
    // These constraints turn the character toward the downsight (crosshair)
    public MultiAimConstraint firstSpineAim;
    public MultiAimConstraint secondSpineAim;
    public MultiAimConstraint headAim;
    
    private RigBuilder rigBuilder;

    private float horizontalDirection;
    private float verticalDirection;

    private void Awake() {
        animator = GetComponent<Animator>();
        rigBuilder = GetComponent<RigBuilder>();
        playerController = GetComponent<PlayerController>();
        playerManager = GetComponent<PlayerManager>();
    }

    public void PlayAnimationWithoutRootMotions(string targetAnimation, bool isPerformingInput) {
        animator.applyRootMotion = false;
        animator.SetBool("isPerformingInput", isPerformingInput);
        animator.SetBool("disableRootMotion", true);
        // This line makes my camera move while shooting and make a weird effect - will find fix
        //animator.CrossFade(targetAnimation, 0.2f);
    }

    public void PlayAnimationWithRootMotions(string targetAnimation, bool isPerformingInput) {
        animator.SetBool("isPerformingInput", isPerformingInput);
        animator.SetBool("disableRootMotion", true);
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

    public void AssignHandIK(RightHandIKTarget rightTarget, LeftHandIKTarget leftTarget) {
        rightHandIK.data.target = rightTarget.transform;
        leftHandIK.data.target = leftTarget.transform;
        rigBuilder.Build();
    }

    public void UpdateAimConstraints() {
        // While aiming, the player will turn to the crosshair - but when idle, the camera will be back to normal
        if (playerManager.isAimingGun) {
            firstSpineAim.weight = 0.3f;
            secondSpineAim.weight = 0.3f;
            headAim.weight = 0.7f;
        } else {
            firstSpineAim.weight = 0f;
            secondSpineAim.weight = 0f;
            headAim.weight = 0f;
        }
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
