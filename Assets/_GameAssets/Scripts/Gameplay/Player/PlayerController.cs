using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _orientationTransform;
    
    
    [Header("Movement Settings")]
    [SerializeField] private KeyCode _movementKey = KeyCode.Q;
    [SerializeField] private float _movementSpeed;

    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private float _jumpForce;

    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _slideKey = KeyCode.E;
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _slideDrag;
    
    [Header("GroundCheck Settings")]
    [SerializeField] private float _height;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDrag;
    private Rigidbody _rigidbody;
    
    private Vector2 _inputVector;
    private Vector3 _movementDirection;

    private bool _isSliding;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        SetInputs();
        SetPlayerDrag();
    }

    private void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void SetInputs()
    {
        _inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(_slideKey))
        {
            _isSliding = true;
        }
        else if (Input.GetKeyDown(_movementKey))
        {
            _isSliding = false;
        }
        else if (Input.GetKeyDown(_jumpKey)&& IsGrounded())
        {
            SetPlayerJumping();
        }
    }

    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.right * _inputVector.x + _orientationTransform.forward * _inputVector.y;
        if (_isSliding)
        {
            _rigidbody.AddForce(_movementDirection.normalized * _movementSpeed * _slideMultiplier, ForceMode.Force);
        }
        else
        {
            _rigidbody.AddForce(_movementDirection.normalized * _movementSpeed, ForceMode.Force);
            
        }
    }

    private void SetPlayerDrag()
    {
        if (_isSliding)
            _rigidbody.linearDamping = _slideDrag;
        else
        {
            _rigidbody.linearDamping = _groundDrag;
        }
    }

    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
    }
    private void SetPlayerJumping()
    {
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _height * 0.5f +0.2f, _groundLayer);
    }
}
