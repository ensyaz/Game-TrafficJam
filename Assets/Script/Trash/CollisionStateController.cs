using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionStateController : MonoBehaviour
{
    bool isCollided = false;
    public bool isTurningPoint = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            isCollided = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TurningPoint")
        {
            isTurningPoint = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TurningPoint")
        {
            isTurningPoint = false;
        }
    }

    public bool IsCollided { get { return isCollided; } }
    public bool IsTurningPoint { get { return isTurningPoint; } }


}
