using UnityEngine;
using System.Collections;

public class PlayerAnimationStateController : MonoBehaviour
{
    int velocityZHash;
    float runValue = 0.5f;
    float stopValue = 0f;

    Animator animator;
    CollisionStateController collisionState;


    void Awake()
    {
        animator = GetComponent<Animator>();
        velocityZHash = Animator.StringToHash("VelocityZ");
        collisionState = GetComponent<CollisionStateController>();
    }


    // Update is called once per frame
    void Update()
    {
        AnimationState();
    }

    void AnimationState()
    {
        if (collisionState.IsCollided)
        {
            RunAnimation();
        }

        else if(!collisionState.IsCollided)
        {
            StopAnimation();
        }
    }

    void RunAnimation()
    {
        animator.SetFloat(velocityZHash, runValue);
    }

    void StopAnimation()
    {
        animator.SetFloat(velocityZHash, stopValue);
    }





 


















}
