using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : StateMachineBehaviour
{
    protected PlayerState playerState;

    public bool startup;
    public bool active;
    public bool recovery;

    public bool dontExitState;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!playerState)
        {
            playerState = animator.gameObject.GetComponent<PlayerState>();
        }

        if (startup)
        {
            playerState.startup = true;
        }

        if (active)
        {
            playerState.active = true;
        }

        if (recovery)
        {
            playerState.recovery = true;
        }
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!dontExitState)
        {
            playerState.recovery = false;
            playerState.startup = false;
            playerState.active = false;
        }
        
    }

}
