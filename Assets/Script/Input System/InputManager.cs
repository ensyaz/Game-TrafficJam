using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Events
    private delegate void StartTouch(Vector2 position, float time);
    private event StartTouch OnStartTouch;

    private delegate void EndTouch(Vector2 position, float time);
    private event EndTouch OnEndTouch;

    public delegate void LeftSwipe();
    public static event LeftSwipe OnSwipeLeft;

    public delegate void RightSwipe();
    public static event RightSwipe OnSwipeRight;

    public delegate void UpSwipe();
    public static event UpSwipe OnSwipeUp;

    public delegate void DownSwipe();
    public static event DownSwipe OnSwipeDown;

    #endregion

    #region Fields
    [SerializeField]
    private float _minimumDistance = 15f;
    [SerializeField]
    private float _maximumTime = 1f;
    [SerializeField, Range(0f, 1f)]
    private float directionThreshold = 0.9f;

    private Vector2 _startPosition, _endPosition;
    private float _startTime, _endTime;
    private InputActions _playerInput;
    #endregion 


    private void OnEnable()
    {
        _playerInput = new InputActions();
        _playerInput.Touch.Enable();
        _playerInput.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        _playerInput.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        OnStartTouch += SwipeStart;
        OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        _playerInput.Touch.Disable();
        _playerInput.Touch.PrimaryContact.started -= ctx => StartTouchPrimary(ctx);
        _playerInput.Touch.PrimaryContact.canceled -= ctx => EndTouchPrimary(ctx);
        OnStartTouch -= SwipeStart;
        OnEndTouch -= SwipeEnd;
    }


    private void StartTouchPrimary(InputAction.CallbackContext ctx) 
    {
        if (OnStartTouch != null)
            OnStartTouch(TouchPosition(), (float)ctx.startTime);    
    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx) 
    {
        if (OnEndTouch != null)
            OnEndTouch(TouchPosition(), (float)ctx.time);
    }

    private Vector2 TouchPosition()
    {
        return _playerInput.Touch.PrimaryPosition.ReadValue<Vector2>();
    }

    private void SwipeStart(Vector2 position, float time)
    {
        _startPosition = position;
        _startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        _endPosition = position;
        _endTime = time;

        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(_startPosition, _endPosition) >= _minimumDistance && (_endTime - _startTime) < _maximumTime)
        {
            Vector3 direction = _endPosition - _startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        // Up
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold )
        {
            OnSwipeUp?.Invoke();
        }
        // Down
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            OnSwipeDown?.Invoke();
        }
        // Left
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold )
        {
            OnSwipeLeft?.Invoke();
        }
        // Right
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            OnSwipeRight?.Invoke();
        }
    }
}
