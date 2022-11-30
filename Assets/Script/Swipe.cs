using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    bool isDragging = false;
    Vector2 startTouch, swipeDelta;

    void Update()
    {
        // Mobile Input
        InitiateSwipe();

        // Calculate the distance between points
        StartFinalTouchDistance();

        HandleDirection();
    }

    void InitiateSwipe()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDragging = true;
                tap = true;
                startTouch = Input.touches[0].position;
            }

            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }
    }

    void StartFinalTouchDistance()
    {
        swipeDelta = Vector2.zero;

        if (isDragging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
        }

    }

    void HandleDirection()
    {
        // Did we cross the deadzone?
        if (swipeDelta.magnitude > 75)
        {
            // Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Left or Right
                if (x < 0)
                    swipeLeft = true;

                else
                    swipeRight = true;
            }

            else
            {
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }

            Reset();
        }
    }

    void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }


}
