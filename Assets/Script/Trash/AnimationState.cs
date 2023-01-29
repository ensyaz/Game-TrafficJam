using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationState : MonoBehaviour
{
    Animator animator;
    Movement playerMovement;
    CollisionStateController collisionState;
    int isRunningHash;
    int isJumpingHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<Movement>();
        collisionState = GetComponent<CollisionStateController>();
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {
        ProcessAnimation();
    }

    void ProcessAnimation()
    {
        
        if (collisionState.IsCollided)
        {
            animator.SetBool("isCollided", true);
        }

        if (playerMovement.IsJumping && !collisionState.IsCollided)
        {
            animator.SetBool(isJumpingHash, true);
            Debug.Log(isJumpingHash);
        }

        if (!playerMovement.IsJumping)
        {
            animator.SetBool(isJumpingHash, false);
        }

        if (playerMovement.IsRolling && !collisionState.IsCollided)
        {
            animator.SetBool("isRolling", true);
        }


        else if(!playerMovement.IsRolling)
        {
            animator.SetBool("isRolling", false);
        }

        else if (!playerMovement.IsJumping)
        {
            animator.SetBool(isJumpingHash, false);
        }





    }
}
