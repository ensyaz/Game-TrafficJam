using UnityEngine;

public class PlayerRagdollController : MonoBehaviour
{
    [SerializeField] private Collider _parentCollider;
    [SerializeField] private Animator _animator;

    public Rigidbody _parentRigidBody;

    private Transform _transform;

    public bool IsRagdollEnabled;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable() => GameManager.sharedInstance.OnGameOverEvent += OnObstacleHit;
    private void OnDisable() => GameManager.sharedInstance.OnGameOverEvent -= OnObstacleHit;

    private void Start()
    {
        SetRagdollMode(IsRagdollEnabled);
    }

    private void OnObstacleHit()
    {
        SetRagdollMode(true);
    }

    private void SetRagdollMode(bool value)
    {
        IsRagdollEnabled = value;

        if (value)
        {
            EnableRagdoll();
        }
        else
        {
            DisableRagdoll();
        }
    }

    private void EnableRagdoll()
    {
        _animator.enabled = false;
        SetKinematicRigidBodies(false);
        SetColliders(true);
    }

    private void DisableRagdoll()
    {
        _animator.enabled = true;
        SetKinematicRigidBodies(true);
        SetColliders(false);
    }
    // Whether to enable or disable the rigidbodies of player
    private void SetKinematicRigidBodies(bool value)
    {
        Rigidbody[] rigidBodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody childRigidBody in rigidBodies)
        {
            //childRigidBody.velocity = Vector3.zero;
            childRigidBody.isKinematic = value;
        }

        _parentRigidBody.isKinematic = !value;
    }
    // Whether to enable or disable the colliders of player
    private void SetColliders(bool value)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider childCollider in colliders)
        {
            childCollider.enabled = value;
        }

        _parentCollider.enabled = true;

        if (value)
        {
            _parentCollider.enabled = false;
        }
    }
}
