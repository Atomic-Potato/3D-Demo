using System;
using System.Net.Http.Headers;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField, Min(0)] float _speed = 5f;
    [SerializeField, Min(0)] float _runSpeed = 10f;
    [SerializeField, Min(0)] float _jumpForce = 10f;

    [Space]
    [SerializeField] Vector3 _groundedBoxHalfExtents;
    [SerializeField] Transform _groundedTransfrom;
    [SerializeField] LayerMask _walkableLayer;

    [Space]
    [SerializeField] Transform[] _slopeDetectionTransfroms;

    [Space]
    [SerializeField] Transform _cameraTransfrom;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Holder _holder;
    public Holder Holder => _holder; 

    Vector2 _moveInput;
    bool _isRunInput;
    bool _isJumpInput;
    public bool IsGrounded {get; protected set;}
    public bool IsOnSlope {get; protected set;}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, 
            transform.position + new Vector3(-_cameraTransfrom.right.z, 0f, _cameraTransfrom.right.x));
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateInput();
        UpdateGrounded();
        UpdateIsOnSlope();
        Move();
    }

    void UpdateInput()
    {
        Vector2 forward = new Vector2(-_cameraTransfrom.right.z, _cameraTransfrom.right.x);
        Vector2 right = new Vector2(_cameraTransfrom.right.x, _cameraTransfrom.right.z);
        _moveInput = forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal");
        _isRunInput = Input.GetKeyDown(KeyCode.LeftShift);
        _isJumpInput = Input.GetKeyDown(KeyCode.Space);
    }

    void Move()
    {
        _rigidbody.useGravity = !(IsOnSlope && IsGrounded);
        float speed = _isRunInput ? _runSpeed : _speed;
        Vector3 direction = Vector3.ProjectOnPlane(
                new Vector3(_moveInput.x, 0f, _moveInput.y), _slopeHit.normal).normalized;
        Vector3 velocity = new Vector3(
            direction.x * speed, 
            _rigidbody.useGravity ? _rigidbody.velocity.y : direction.y * speed, 
            direction.z * speed);
        if (_isJumpInput && IsGrounded)
            velocity.y = _jumpForce;
        _rigidbody.velocity = velocity;
    }

    void UpdateGrounded()
    {
        Collider[] cols = Physics.OverlapBox(_groundedTransfrom.position, _groundedBoxHalfExtents, Quaternion.identity, _walkableLayer);
        IsGrounded = cols.Length != 0;
    }

    float _slopeAngle;
    RaycastHit _slopeHit;
    void UpdateIsOnSlope()
    {
        if(!IsGrounded)
            return ;

        foreach(Transform trans in _slopeDetectionTransfroms)
        {
            bool rayHit = Physics.Raycast(trans.position, Vector3.down, out _slopeHit, 5f);
            if(rayHit)
            {
                _slopeAngle = Vector3.Angle(Vector3.up, _slopeHit.normal);
                if(_slopeAngle != 0f)
                {
                    IsOnSlope = true;
                    return;
                }
            }
        }
        _slopeAngle = 0;
        IsOnSlope = false;
    }
}
