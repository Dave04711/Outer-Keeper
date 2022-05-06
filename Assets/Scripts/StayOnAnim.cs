using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnAnim : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<MobileMovement>().Halt(true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<MobileMovement>().Halt(false);
    }
}
