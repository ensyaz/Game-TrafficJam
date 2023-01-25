using UnityEngine;

public class PlayerUtilities : MonoBehaviour
{
    public bool isGrounded = false;
    public static PlayerUtilities playerUtilityInstance;

    private float hold;

    private Transform _playerLocation;

    private void Awake()
    {
        _playerLocation = GetComponent<Transform>();
        playerUtilityInstance = this;
    }

    public bool WhichLane(float lane)
    {
        if (_playerLocation.position.x == lane)
            return true;
        else
            return false;
    }

    public bool Equal(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) <= tolerance);
    }

    





}
