using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    float smoothSpeed = 100f;
    [SerializeField]
    Transform target;

    [SerializeField]
    Vector3 offset = new Vector3(0, 1, -12);
    Vector3 desiredPosition;
    Vector3 smoothedPosition;

    void FixedUpdate()
    {
        trackCamera();
    }

    void trackCamera()
    {
        desiredPosition = target.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }
}
