using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript
{
    private Animator animator;
    private AnimationStates animationState;

    public AnimationScript(Animator animator)
    {
        this.animator = animator;
        animationState = AnimationStates.IDLE;
    }

    public void ChangeState(AnimationStates state)
    {
        switch (state)
        {
            case AnimationStates.RUN:
                animator.SetBool("isRunning", true);
                break;
            case AnimationStates.IDLE:
                animator.SetBool("isRunning", false);
                break;
        }

        animationState = state;
    }
}

public enum AnimationStates
{
    IDLE,
    RUN,
}