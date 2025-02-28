using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerJump;
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
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _airDrag;
    [SerializeField] private float _jumpResetTime;

    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _playerHeight;
    [SerializeField] private float _groundDrag;

    [Header("Slider Settings")]
    [SerializeField] private KeyCode _sliderKey;
    [SerializeField] private float _sliderMultiplier;
    [SerializeField] private float _sliderDrag;

    private StateController _stateController;
    private bool isSliding;
    private float _startmoveSpeed;
    private float _startJumpForce;
    private float _horizontalInput, _verticalInput;
    private Vector3 _moveDirection;
    private void Awake()
    {
        _stateController = GetComponent<StateController>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        _startmoveSpeed = _moveSpeed;
        _startJumpForce = _jumpForce;
    }

    private void Update()
    {
        SetInputs();
        SetState();
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
    private void SetState()
    {
        var moveDirect = GetMoveDirection();
        var isgrounded = isGrounded();
        var isliding = GetisSliding();
        var currentState = _stateController.GetCurrentState();

        var newState = currentState switch
        {
            _ when moveDirect == Vector3.zero && isgrounded && !isliding => PlayerState.Idle,
            _ when moveDirect != Vector3.zero && isgrounded && !isliding => PlayerState.Move,
            _ when moveDirect != Vector3.zero && isgrounded && isliding => PlayerState.Slide,
            _ when moveDirect == Vector3.zero && isgrounded && isliding => PlayerState.SlideIdle,
            _ when !_canJump && !isgrounded => PlayerState.Jump,
            _ => currentState
        };
        if (newState != currentState)
        {
            _stateController.ChangeState(newState);
        }
    }

    private void SetPlayerMovement()
    {
        _moveDirection = _orientationtransform.forward * _verticalInput + _orientationtransform.right * _horizontalInput;

        float _forceMultiplier = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => _sliderMultiplier,
            PlayerState.Jump => _airMultiplier,
            _ => 1f
        };
        _rigidbody.AddForce(_moveDirection.normalized * _moveSpeed * _forceMultiplier, ForceMode.Force);
    }
    private void SetPlayerJumping()
    {
        OnPlayerJump?.Invoke();
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);
        _rigidbody.AddForce(_rigidbody.transform.up * _jumpForce, ForceMode.Impulse);

    }
    private void SetPlayerDrag()
    {
        _rigidbody.linearDamping = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => _groundDrag,
            PlayerState.Slide => _sliderDrag,
            PlayerState.Jump => _airDrag,
            _ => _rigidbody.linearDamping
        };
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
    private Vector3 GetMoveDirection()
    {
        return _moveDirection;
    }
    private bool GetisSliding()
    {
        return isSliding;
    }
    private bool isGrounded()
    {
        return Physics.Raycast(_rigidbody.transform.position, Vector3.down, _playerHeight * 0.5f + 0.1f, _groundLayer);
    }
    private bool jumpReset()
    {
        return _canJump = true;
    }
    public void SetMovementSpeed(float speed, float duration)
    {
        _moveSpeed += speed;
        Invoke("ResetMovementSpeed", duration);
    }
    private void ResetMovementSpeed()
    {
        _moveSpeed = _startmoveSpeed;
    }
    public void SetJumpForce(float force, float duration)
    {
        _jumpForce += force;
        Invoke("ResetJumpForce", duration);
    }
    private void ResetJumpForce()
    {
        _jumpForce = _startJumpForce;
    }
}
