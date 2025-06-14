using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerJumped;
    public event Action<PlayerState> OnPlayerStateChanged;
    [Header("References")]
    [SerializeField] private Transform _orientationTransform;
    
    
    [Header("Movement Settings")]
    [SerializeField] private KeyCode _movementKey = KeyCode.Q;
    [SerializeField] private float _movementSpeed;

    [Header("Jump Settings")]
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _canJump;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float _jumpDrag;
    
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
    private StateController _stateController;

    private float _startingJumpForce, _startingMovementSpeed;
    
    private bool _isSliding;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _stateController = GetComponent<StateController>();
        _startingJumpForce = _jumpForce;
        _startingMovementSpeed = _movementSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play &&
            GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }
        SetInputs();
        SetStates();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play &&
            GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }
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
        else if (Input.GetKeyDown(_jumpKey)&& _canJump &&IsGrounded())
        {
            _canJump = false;
            SetPlayerJumping();
            Invoke("ResetJumping",_jumpCooldown);
        }
    }

    private void SetStates()
    {
        Vector3 movementDirection = GetMovementDirection();
        bool isGrounded = IsGrounded();
        bool isSliding = IsSliding();
        PlayerState currentState = _stateController.GetCurrentPlayerState();

        var newState = currentState switch
        {
            _ when movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
            _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
            _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
            _ when !_canJump && !isGrounded => PlayerState.Jump,
            _ => currentState
        };
        if (currentState != newState)
        {
            _stateController.ChangeState(newState);
            OnPlayerStateChanged?.Invoke(newState);
        }
    }
    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.right * _inputVector.x + _orientationTransform.forward * _inputVector.y;
        float forceMultiplier = _stateController.GetCurrentPlayerState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => _slideMultiplier,
            PlayerState.Jump => airMultiplier,
            _ => 1f
        };
        _rigidbody.AddForce(_movementDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);
    }

    private void SetPlayerDrag()
    {
        _rigidbody.linearDamping = _stateController.GetCurrentPlayerState() switch
        {
            PlayerState.Move => _groundDrag,
            PlayerState.Slide => _slideDrag,
            PlayerState.Jump => _jumpDrag,
            _ => _rigidbody.linearDamping
        };
    }

    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
        if (flatVelocity.magnitude > _movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
            _rigidbody.linearVelocity = new Vector3(limitedVelocity.x, _rigidbody.linearVelocity.y, limitedVelocity.z);
        }
    }
    private void SetPlayerJumping()
    {
        OnPlayerJumped?.Invoke();
        
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

   
    #region HelperFunctions
    
        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, _height * 0.5f +0.2f, _groundLayer);
        }
        
        private void ResetJumping()
        {
            _canJump = true;
        }
        
        private Vector3 GetMovementDirection()
        {
            return _movementDirection.normalized;
        }

        private bool IsSliding()
        {
            return _isSliding;
        }

        public void SetMovementSpeed(float speed, float duration)
        {
            _movementSpeed += speed;
            Invoke(nameof(ResetMovementSpeed),duration);
        }

        private void ResetMovementSpeed()
        {
            _movementSpeed = _startingMovementSpeed;
        }

        public void SetJumpForce(float jumpForce, float duration)
        {
            _jumpForce += jumpForce;
            Invoke(nameof(ResetJumpForce), duration);
        }

        private void ResetJumpForce()
        {
            _jumpForce = _startingJumpForce;
        }

        public Rigidbody GetPlayerRigidbody()
        {
            return _rigidbody;
        }
    #endregion
   
}
