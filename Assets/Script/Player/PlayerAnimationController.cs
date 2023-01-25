using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        SetAnimations();
    }

    private void SetAnimations()
    {
        SetJumpAnimation();
        SetRollAnimation();
    }

    private void SetJumpAnimation()
    {
        if (_playerMovement.IsJumping)
        {
            _animator.SetBool("isJump", true);
        }
            
        else
        {
            _animator.SetBool("isJump", false);
        }
            
    }

    private void SetRollAnimation()
    {
        if (_playerMovement.IsRolling)
            _animator.SetBool("isRoll", true);
        else
            _animator.SetBool("isRoll", false);
    }


}
