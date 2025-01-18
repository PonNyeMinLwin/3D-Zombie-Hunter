using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBoolsInAnimator : StateMachineBehaviour
{
    [Header("Performing Action")]
    public string isPerformingInput = "isPerformingInput";
    public bool isPerformingInputStatus = false;

    [Header("Performing Turn")]
    public string isTurning = "isTurning";
    public bool isTurningStatus = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetBool(isPerformingInput, isPerformingInputStatus);
        animator.SetBool(isPerformingInput, isPerformingInputStatus);
    }
}
