using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Vector3 forwardMove = new Vector3(0, 0, 5f);
    Vector3 direction = new Vector3(0, 0, 5);

    float rotationSpeed = 0.1f;
    float jumpingHeight = 10f;
    float speed = 50f;
    float laneChangeRange = 3f;
    float heightRange = 3f;
    float jumpLerpDuration = 0.5f;
    float rotationLerpDuration = 1f;
    float rollTimer;

    CollisionStateController collisionState;
    CapsuleCollider capsuleCollider;

    [SerializeField] AnimationCurve rollCurve;

    bool isRotating = false;
    bool isLaneChanging = false;
    bool isRolling = false;
    bool isJumping = false;
    bool onCenter;
    bool onSideLane;

    Swipe swipe;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        swipe = GetComponent<Swipe>();
        collisionState = GetComponent<CollisionStateController>();

        Keyframe roll_lastFrame = rollCurve[rollCurve.length - 1];
        rollTimer = roll_lastFrame.time;
        capsuleCollider = GetComponent<CapsuleCollider>();

    }
    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
        PlayerCondition();
    }

    void ProcessMovement()
    {
        if (!collisionState.IsCollided)
        {
            Run();
        }

        if (!collisionState.IsCollided && collisionState.IsTurningPoint && swipe.SwipeLeft && !isRotating)
        {
            StartCoroutine(Rotation(-90));
        }

        if (!collisionState.IsCollided && collisionState.IsTurningPoint && swipe.SwipeRight && !isRotating)
        {
            StartCoroutine(Rotation(90));
        }

        if (!collisionState.IsCollided && !collisionState.IsTurningPoint && swipe.SwipeLeft && !isLaneChanging && !LeftLane() )
        {
            StartCoroutine(LaneChange(-1));
        }

        if (!collisionState.IsCollided && !collisionState.IsTurningPoint && swipe.SwipeRight && !isLaneChanging && !RightLane() )
        {
            StartCoroutine(LaneChange(1));
        }

        if (!collisionState.IsCollided && swipe.SwipeUp && !isJumping)
        {
            StartCoroutine(Jump());
        }
        
        if (!collisionState.IsCollided && swipe.SwipeDown && !isRolling)
        {
            StartCoroutine(Roll());
        }

        if (collisionState.IsCollided)
        {
            StopAllCoroutines();
        }
    }

    void Run()
    {
        transform.Translate(forwardMove * Time.deltaTime, Space.Self);
    }


    IEnumerator Jump()
    {
        isJumping = true;

        float timeElapsed = 0f;
        float initialHeight = transform.position.y;
        float finalHeight = heightRange;
        float temp;

        // Increase height till 3 unit
        while (timeElapsed < jumpLerpDuration)
        {
            timeElapsed += Time.deltaTime;
            temp = Mathf.Lerp(initialHeight, finalHeight, timeElapsed / jumpLerpDuration);
            transform.position = new Vector3(transform.position.x, temp, transform.position.z);           
            yield return null;
        }
        // Collider'ýn Player'ý takip edememesinden dolayý yere girmesine engellemek için yine de tam olmadý
        capsuleCollider.center = new Vector3(0, 0.65f, 0);
        transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y), transform.position.z);


        // To wait for the player touch the ground
        while (!Equal(transform.position.y, 0f, 0.3f) )
        {
            yield return null;
        }
        


        isJumping = false;
        capsuleCollider.center = new Vector3(0, 0.95f, 0);
    }
    
    IEnumerator Roll()
    {
        isRolling = true;
        float timer = 0;
        capsuleCollider.center = new Vector3(0f, 0.55f, 0f);
        capsuleCollider.height = 1f;
        capsuleCollider.radius = 0.5f;

        while (timer < rollTimer)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        isRolling = false;
        capsuleCollider.center = new Vector3(0f, 0.95f, 0f);
        capsuleCollider.height = 1.8f;
        capsuleCollider.radius = 0.25f;
    }

    IEnumerator LaneChange(int direction)
    {
        isLaneChanging = true;

        Vector3 initialPosition;
        Vector3 currentPosition;
        float traveledDistance = 0f;
        float rotationDirection = rotationSpeed * direction;

        initialPosition = transform.InverseTransformDirection(transform.position);

        while (traveledDistance < laneChangeRange)
        {
            transform.Translate(rotationDirection, 0, 0, Space.Self);
            currentPosition = transform.InverseTransformDirection(transform.position);
            traveledDistance = Mathf.Abs(currentPosition.x - initialPosition.x);
            yield return null;
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
        isLaneChanging = false;
    }

    IEnumerator Rotation(float angle)
    {
        //rotating = true;
        float timeElapsed = 0f;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, angle, 0);
        

        while (timeElapsed < rotationLerpDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / rotationLerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        //rotating = false;
    }

    void PlayerCondition()
    {
        if(Equal(transform.position.x, 0f, 0.1f) )
        {
            onCenter = true;
        }

        else if (!Equal(transform.position.x, 0f, 0.1f))
        {
            onCenter = false;
        }

        if (Equal(transform.position.x, 3f, 0.1f) || Equal(transform.position.x, -3f, 0.1f))
        {
            onSideLane = true;
        }

        else if( !Equal(transform.position.x, 3f, 0.1f) && !Equal(transform.position.x, -3f, 0.1f))
        {
            onSideLane = false;
        }

    }

    bool Equal(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) <= tolerance);
    }

    bool LeftLane()
    {
        if (transform.position.x == -3f)
        {
            return true;
        }

        else
            return false;
    }

    bool RightLane()
    {
        if (transform.position.x == 3f)
        {
            return true;
        }

        else
            return false;
    }

    bool Center()
    {
        if (transform.position.x == 0f)
        {
            return true;
        }

        else
            return false;

    }

    


    public bool IsJumping { get { return isJumping; } }
    public bool IsRolling { get { return isRolling; } }




}
