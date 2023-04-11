using DG.Tweening;
using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    #region State Parameters
    public bool IsJumping { get => _isJumping; }
    public bool IsRolling { get => _isRolling; }
    #endregion

    #region Control Parameters
    [SerializeField]
    private float speed = 7f;
    [SerializeField]
    private float laneRange = 3f;
    [SerializeField]
    private float centerPoint = 0f;
    [SerializeField]
    private float moveDuration = 0.5f;
    [SerializeField]
    private float _jumpLerpDuration = 0.5f;
    [SerializeField]
    private float _jumpRange = 1.75f;
    #endregion

    private Transform _transform;
    private CapsuleCollider _capsuleCollider;
    private Rigidbody _rigidBody;

    private float _leftLane;
    private float _rightLane;
    private float _rollDuration = 0.95f;
    private float _speedIncreaseRate = 1f;
    private float _jumpDurationDecreaseRate = 0.0125f;
    private float _moveDurationDecreaseRate = 0.000111f;

    #region State Parameters
    private bool _isMoving = false;
    private bool _isLeftSwipe = false;
    private bool _isRightSwipe = false;
    private bool _isDownSwipe = false;
    private bool _isUpSwipe = false;
    private bool _isRolling = false;
    private bool _isJumping = false;
    private bool _isGameOver = false;
    #endregion


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
        GameManager.sharedInstance.OnGameOverEvent += GameOver;
        GameManager.sharedInstance.OnSetSpeedJumpTimingEvent += SetSpeedJumpTiming;
    }

    private void OnDisable()
    {
        InputManager.OnSwipeUp -= OnUpSwipe;
        InputManager.OnSwipeDown -= OnDownSwipe;
        InputManager.OnSwipeLeft -= OnLeftSwipe;
        InputManager.OnSwipeRight -= OnRightSwipe;
        GameManager.sharedInstance.OnGameOverEvent -= GameOver;
        GameManager.sharedInstance.OnSetSpeedJumpTimingEvent -= SetSpeedJumpTiming;
    }

    private void OnLeftSwipe() => _isLeftSwipe = true; 
    private void OnRightSwipe() => _isRightSwipe = true;
    private void OnDownSwipe() => _isDownSwipe = true;
    private void OnUpSwipe() => _isUpSwipe = true;
    private void GameOver() => _isGameOver = true;
    private void SetSpeedJumpTiming() { speed += _speedIncreaseRate; 
                                        _jumpLerpDuration -= _jumpDurationDecreaseRate; 
                                        moveDuration -= _moveDurationDecreaseRate; }

    private void Update()
    {
        if (_isGameOver) return;
        ManageMovement();

    }

    private void ManageMovement()
    {
        Run();

        // Left
        if (_isLeftSwipe && !_isMoving && !PlayerUtilities.playerUtilityInstance.WhichLane(_leftLane))
        {
            MoveLeft();
        }
            
        // Right
        if (_isRightSwipe && !_isMoving && !PlayerUtilities.playerUtilityInstance.WhichLane(_rightLane))
        {
            MoveRight();
        }
            
        // Jump
        if (_isUpSwipe && !_isRolling && !_isJumping)
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
        _transform.Translate(0, 0, speed * Time.deltaTime);
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

        // Increase height till the final height unit
        while (timeElapsed < _jumpLerpDuration)
        {
            timeElapsed += Time.deltaTime;
            hold = Mathf.Lerp(initialHeight, finalHeight, timeElapsed / _jumpLerpDuration);
            transform.position = new Vector3(transform.position.x, hold, transform.position.z);
            yield return null;
        }

        while (!GameManager.sharedInstance.IsGrounded)
        {
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
