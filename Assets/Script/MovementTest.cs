using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    float _rotationSpeed;
    float _laneChangeRange = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable()
    {
        Actions.OnPlayerMove += LaneChange;
    }

    void OnDisable()
    {
        Actions.OnPlayerMove -= LaneChange;
    }

    void LaneChange(int direction)
    {

        Vector3 initialPosition;
        Vector3 currentPosition;
        float traveledDistance = 0f;
        float rotationDirection = _rotationSpeed * direction;

        initialPosition = transform.InverseTransformDirection(transform.position);

        while (traveledDistance < _laneChangeRange)
        {
            transform.Translate(rotationDirection, 0, 0, Space.Self);
            currentPosition = transform.InverseTransformDirection(transform.position);
            traveledDistance = Mathf.Abs(currentPosition.x - initialPosition.x);
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
    }
}
