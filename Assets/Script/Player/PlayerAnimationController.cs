using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Animator _animator;

    private int _isJumpingHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _isJumpingHash = _animator.GetParameter(0).GetHashCode();
    }

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        
    }

    private void SetAnimations()
    {
        if (_playerMovement.IsJumping)
            _animator.SetBool(_isJumpingHash, true); 

    }


}
