using DG.Tweening;
using System;
using UnityEngine;


public class DOTweenMovement : MonoBehaviour
{
    private Transform _transform;
    private CapsuleCollider _capsuleCollider;
    private Rigidbody _rigidBody;

    [SerializeField]
    private float run = 1f;
    [SerializeField]
    private float laneRange = 3f;
    [SerializeField]
    private float centerPoint = 0f;
    [SerializeField]
    private float moveDuration = 1f;

    private float _leftLane;
    private float _rightLane;
    private bool _isMoving = false;
    private bool _isLeftSwipe = false;
    private bool _isRightSwipe = false;


    


    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _rigidBody = GetComponent<Rigidbody>();

        _leftLane = -1 * laneRange;
        _rightLane = laneRange;
    }

    private void OnEnable()
    {
        InputManager.OnSwipeUp += OnUpSwipe;
        InputManager.OnSwipeDown += OnDownSwipe;
        InputManager.OnSwipeLeft += OnLeftSwipe;
        InputManager.OnSwipeRight += OnRightSwipe;
    }

    private void OnDisable()
    {
        InputManager.OnSwipeUp -= OnUpSwipe;
        InputManager.OnSwipeDown -= OnDown;
        InputManager.OnSwipeLeft -= OnLeftSwipe;
        InputManager.OnSwipeRight -= OnRightSwipe;
    }

    private void OnLeftSwipe()
    {
        _isLeftSwipe = true; 
    }
    
    
    private void OnRightSwipe()
    {
        _isRightSwipe = true;
    }

        

    private void Update()
    {
        ManageMovement();
    }



    private void ManageMovement()
    {
        Run();

        //Left
        if (_isLeftSwipe && !_isMoving && !PlayerUtilities.playerUtilityInstance.WhichLane(_leftLane))
            MoveLeft();
        //Right
        else if(_isRightSwipe && !_isMoving && !PlayerUtilities.playerUtilityInstance.WhichLane(_rightLane))
            MoveRight();

        else
        {
            _isLeftSwipe = false;
            _isRightSwipe = false;
        }
    }

    private void Run()
    {
        _transform.Translate(0, 0, run * Time.deltaTime);
    }

    private void MoveLeft()
    {
        _isMoving = true;

        // If player is on right lane, move to center
        if (PlayerUtilities.playerUtilityInstance.WhichLane(_rightLane))
            Move(centerPoint);
        // If player is on center, move to left lane
        else
            Move(_leftLane);
    }

    private void MoveRight()
    {
        _isMoving = true;

        // If player is on left lane, move to center
        if (PlayerUtilities.playerUtilityInstance.WhichLane(_leftLane))
            Move(centerPoint);
        // If player is on center, move to right lane
        else
            Move(_rightLane);
    }

    private void Move(float destination)
    {
        _transform.DOMoveX(destination, moveDuration).OnComplete(() => _isMoving = false);
    }

    private void OnUpSwipe()
    {
        float jumpPower = 7.5f;
        if (PlayerUtilities.playerUtilityInstance.isGrounded)
        {
            _rigidBody.velocity = Vector3.up * jumpPower;
        }
    }


    private IEnumerator OnDownSwipe()
    {
        _isRolling = true;

        float timeElapsed = 0;
        _capsuleCollider.center = new Vector3(0f, 0.55f, 0f);
        _capsuleCollider.height = 1f;
        _capsuleCollider.radius = 0.5f;

        while (timeElapsed < _rollDuration)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _capsuleCollider.center = new Vector3(0f, 0.95f, 0f);
        _capsuleCollider.height = 1.8f;
        _capsuleCollider.radius = 0.25f;

        _isRolling = false;
    }









}
