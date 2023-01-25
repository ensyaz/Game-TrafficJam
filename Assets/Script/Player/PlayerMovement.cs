using DG.Tweening;
using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private Transform _transform;
    private CapsuleCollider _capsuleCollider;
    private Rigidbody _rigidBody;

    #region Control Parameters
    [SerializeField]
    private float run = 1f;
    [SerializeField]
    private float laneRange = 3f;
    [SerializeField]
    private float centerPoint = 0f;
    [SerializeField]
    private float moveDuration = 1f;
    [SerializeField]
    private float _jumpLerpDuration = 0.34f;
    [SerializeField]
    private float _jumpRange = 2f;
    #endregion


    private float _leftLane;
    private float _rightLane;
    private bool _isMoving = false;
    private bool _isLeftSwipe = false;
    private bool _isRightSwipe = false;
    private bool _isDownSwipe = false;
    private bool _isUpSwipe = false;
    private bool _isRolling = false;
    private bool _isJumping = false;
    private float _rollDuration = 0.95f;

    public bool IsJumping { get => _isJumping; }
    public bool IsRolling { get => _isRolling; }

    private float _initPos;


    private void Awake()
    {
        _transform = transform;
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
        InputManager.OnSwipeDown -= OnDownSwipe;
        InputManager.OnSwipeLeft -= OnLeftSwipe;
        InputManager.OnSwipeRight -= OnRightSwipe;
    }

    private void OnLeftSwipe() => _isLeftSwipe = true; 
    private void OnRightSwipe() => _isRightSwipe = true;
    private void OnDownSwipe() => _isDownSwipe = true;
    private void OnUpSwipe() => _isUpSwipe = true;

    private void Update()
    {
        

        ManageMovement();
    }

    private void ManageMovement()
    {
        if (!GameManager.sharedInstance.IsGameOver)
        {
            Run();
        }
        
        // Left
        if (_isLeftSwipe && !_isMoving && !PlayerUtilities.playerUtilityInstance.WhichLane(_leftLane))
            MoveLeft();
        // Right
        else if (_isRightSwipe && !_isMoving && !PlayerUtilities.playerUtilityInstance.WhichLane(_rightLane))
            MoveRight();
        // Jump
        else if (_isUpSwipe && !_isRolling && !_isJumping)
        {
            StartCoroutine(Jump());
        }
            
        // Roll
        else if (_isDownSwipe && !_isRolling && GameManager.sharedInstance.IsGrounded)
            StartCoroutine(Roll());

        else
        {
            _isLeftSwipe = false;
            _isRightSwipe = false;
            _isDownSwipe = false;
            _isUpSwipe = false;
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

    private IEnumerator Jump()
    {
        _isJumping = true;

        float timeElapsed = 0f;
        float initialHeight = _transform.position.y;
        float finalHeight = _jumpRange;
        float hold;

        // Increase height till 3 unit
        while (timeElapsed < _jumpLerpDuration)
        {
            timeElapsed += Time.deltaTime;
            hold = Mathf.Lerp(initialHeight, finalHeight, timeElapsed / _jumpLerpDuration);
            transform.position = new Vector3(transform.position.x, hold, transform.position.z);
            yield return null;
        }

        _isJumping = false;
        _isUpSwipe = false;
    }


    private IEnumerator Roll()
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