using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [Header("References")]
    [SerializeField] private Transform _orientationtransform;

    [Header("Movement Speed Settings")]
    [SerializeField] private KeyCode _moveKey;
    [SerializeField] private float _moveSpeed;

    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private bool _canJump;
    [SerializeField] private float _jumpResetTime;

    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _playerHeight;
    [SerializeField] private float _groundDrag;

    [Header("Slider Settings")]
    [SerializeField] private KeyCode _sliderKey;
    [SerializeField] private float _sliderMultiplier;
    [SerializeField] private float _sliderDrag;

    private bool isSliding;
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
        SetPlayerDrag();
        LimitPlayerSpeed();

    }
    private void FixedUpdate()
    {
        SetPlayerMovement();
    }
    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(_sliderKey))
        {
            isSliding = true;
        }
        else if (Input.GetKeyUp(_moveKey))
        {
            isSliding = false;
        }
        else if (Input.GetKey(_jumpKey) && _canJump && isGrounded())
        {
            _canJump = false;
            SetPlayerJumping();
            Invoke("jumpReset", _jumpResetTime);

        }
    }
    private void SetPlayerMovement()
    {
        _moveDirection = _orientationtransform.forward * _verticalInput + _orientationtransform.right * _horizontalInput;
        if (isSliding)
        {
            _rigidbody.AddForce(_moveDirection.normalized * _moveSpeed * _sliderMultiplier, ForceMode.Force);
        }
        else
        {
            _rigidbody.AddForce(_moveDirection.normalized * _moveSpeed, ForceMode.Force);

        }
    }
    private void SetPlayerJumping()
    {
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);
        _rigidbody.AddForce(_rigidbody.transform.up * _jumpForce, ForceMode.Impulse);

    }
    private bool jumpReset()
    {
        return _canJump = true;
    }
    private bool isGrounded()
    {
        return Physics.Raycast(_rigidbody.transform.position, Vector3.down, _playerHeight * 0.5f + 0.1f, _groundLayer);
    }
    private void SetPlayerDrag()
    {
        if (isSliding)
        {
            _rigidbody.linearDamping = _sliderDrag;
        }
        else
        {
            _rigidbody.linearDamping = _groundDrag;
        }
    }
    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);
        if (flatVelocity.magnitude > _moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _moveSpeed;
            _rigidbody.linearVelocity = new Vector3(limitedVelocity.x, _rigidbody.linearVelocity.y, limitedVelocity.z);
        }
    }
}
