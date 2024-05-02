using System;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _runSpeed = 10f;
    [SerializeField] float _jumpForce = 10f;

    [Space]
    [SerializeField] Transform _cameraTransfrom;
    [SerializeField] Rigidbody _rigidbody;

    Vector2 _moveInput;
    bool _isRunInput;
    bool _isJumpInput;

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
        Move();
        Jump(); 
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
        float speed = _isRunInput ? _runSpeed : _speed;
        Vector3 velocity = new Vector3(_moveInput.x * speed, _rigidbody.velocity.y, _moveInput.y * speed);
        _rigidbody.velocity = velocity;
    }

    void Jump()
    {
        if (_isJumpInput)
            _rigidbody.AddForce(Vector3.up * _jumpForce , ForceMode.Impulse);
    }
}
