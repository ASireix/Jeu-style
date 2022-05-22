using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBehavior : StateMachineBehaviour
{
    protected PlayerMovement playerMovement;

    public bool Rotations;
    public bool Movements;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!playerMovement)
        {
            playerMovement = animator.gameObject.GetComponent<PlayerMovement>();
        }
        
        if (Rotations)
        {
            playerMovement.LockRot();
        }

        if (Movements)
        {
            playerMovement.LockMov();
        }
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerMovement.UnlockAll();
    }
}
