using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Events
    /*
    private delegate void RunAnimation();
    private event RunAnimation OnRunAnimation;
    
    private delegate void JumpAnimation();
    private event JumpAnimation OnJumpAnimation;

    private delegate void RollAnimation();
    private event RollAnimation OnRollAnimation;
    */
    #endregion

    #region Fields
    [SerializeField]
    private float _forwardSpeed = 10f;
    [SerializeField]
    private float _laneChangeSpeed = 1f;
    [SerializeField]
    private float _jumpRange = 3f;
    [SerializeField]
    private float _jumpLerpDuration = 0.5f;
    [SerializeField]
    private float _rollDuration = 0.95f;
    private float _laneRange = 3f;
    
    private bool _isLeftSwipe = false;
    private bool _isRightSwipe = false;
    private bool _isUpSwipe = false;
    private bool _isDownSwipe = false;
    private bool _isJumping = false;
    private bool _isRolling = false;
    private bool _isLaneChanging = false;

    private InputManager _inputManager;
    private Transform _playerPosition;
    private CapsuleCollider _capsuleCollider;
    #endregion

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _playerPosition = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        /*
        _inputManager.OnSwipeLeft += Left;
        _inputManager.OnSwipeRight += Right;
        _inputManager.OnSwipeUp += Jump;
        _inputManager.OnSwipeDown += Roll;
        */
    }

    private void OnDisable()
    {
        /*
        _inputManager.OnSwipeLeft -= Left;
        _inputManager.OnSwipeRight -= Right;
        _inputManager.OnSwipeUp -= Jump;
        _inputManager.OnSwipeDown -= Roll;
        */
    }

    private void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        ManageMovement();
    }

    private void Left() => _isLeftSwipe = true;
    private void Right() => _isRightSwipe = true;
    private void Jump() => _isUpSwipe = true;
    private void Roll() => _isDownSwipe = true;

    private void ManageMovement()
    {
        ManageForward();
        ManageLeft();
        ManageRight();
        ManageJump();
        ManageRoll();
    }

    private void ManageForward()
    {
        _playerPosition.Translate(0, 0, _forwardSpeed * Time.deltaTime);
    }

    private void ManageLeft()
    {
        if (!_isLaneChanging && _isLeftSwipe && !PlayerUtilities.playerUtilityInstance.WhichLane(-1 * _laneRange))
            StartCoroutine(OnLaneChange(-1));
        else
            _isLeftSwipe = false;
    }

    private void ManageRight()
    {
        if (!_isLaneChanging && _isRightSwipe && !PlayerUtilities.playerUtilityInstance.WhichLane(_laneRange))
            StartCoroutine(OnLaneChange(1));    
        else
            _isRightSwipe = false;
    }

    private void ManageJump()
    {
        if (!_isJumping && _isUpSwipe)
            StartCoroutine(OnJump());
        else
            _isUpSwipe = false;
        
    }

    private void ManageRoll()
    {
        if (!_isRolling && _isDownSwipe)
            StartCoroutine(OnRoll());
        else
            _isDownSwipe = false;
    }

    private IEnumerator OnLaneChange(int direction)
    {
        _isLaneChanging = true;

        float initialPosition = _playerPosition.position.x; 
        float currentPosition;
        float traveledDistance = 0f;
        float way = direction * _laneChangeSpeed * Time.deltaTime;

        while (traveledDistance < _laneRange)
        {
            transform.Translate(way, 0, 0);
            currentPosition = _playerPosition.position.x;
            traveledDistance = Mathf.Abs(currentPosition - initialPosition);
            yield return null;
        }

        _playerPosition.position = new Vector3(Mathf.Round(_playerPosition.position.x), _playerPosition.position.y, _playerPosition.position.z);
        _isLaneChanging = false;
    }

    private IEnumerator OnJump()
    {
        _isJumping = true;

        float timeElapsed = 0f;
        float initialHeight = _playerPosition.position.y;
        float finalHeight = _jumpRange;
        float temp;

        // Increase height till 3 unit
        while (timeElapsed < _jumpLerpDuration)
        {
            timeElapsed += Time.deltaTime;
            temp = Mathf.Lerp(initialHeight, finalHeight, timeElapsed / _jumpLerpDuration);
            transform.position = new Vector3(transform.position.x, temp, transform.position.z);
            yield return null;
        }

        _capsuleCollider.center = new Vector3(0, 0.65f, 0);
        _playerPosition.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y), transform.position.z);

        // To wait for the player touch the ground
        while (!PlayerUtilities.playerUtilityInstance.Equal(transform.position.y, 0f, 0.3f))
        {
            yield return null;
        }

        _capsuleCollider.center = new Vector3(0, 0.95f, 0);
        _isJumping = false;
    }

    private IEnumerator OnRoll()
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
