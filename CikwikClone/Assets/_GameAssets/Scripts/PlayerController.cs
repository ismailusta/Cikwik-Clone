using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [Header("References")]
    [SerializeField] private Transform _orientationtransform;
    [Header("Movement Speed Settings")]
    [SerializeField] private float _moveSpeed;

    private float _horizontalInput, _verticalInput;
    private Vector3 _moveDirection;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    private void Update()
    {
        SetInputs();

    }
    private void FixedUpdate()
    {
        SetPlayerMovement();
    }
    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void SetPlayerMovement()
    {
        _moveDirection = _orientationtransform.forward * _verticalInput + _orientationtransform.right * _horizontalInput;
        _rigidbody.AddForce(_moveDirection * _moveSpeed, ForceMode.Force);

    }
}
