using UnityEngine;

public class PlayerUtilities : MonoBehaviour
{
    public bool isGrounded = false;
    public static PlayerUtilities playerUtilityInstance;

    private float hold;

    private Transform _playerLocation;

    private void Awake()
    {
        _playerLocation = transform;
        playerUtilityInstance = this;
    }
    // To decide which lane player is
    public bool WhichLane(float lane)
    {
        if (Mathf.Round(_playerLocation.position.x) == lane)
            return true;
        else
            return false;
    }

    public bool Equal(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) <= tolerance);
    }

    





}
