using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpPower;

    private PlayerInputs _playerInputs;
    
    private Vector3 _moveDirection;
    private Vector2 _inputDirection;

    // Start is called before the first frame update
    void Start()
    {
        _playerInputs = new PlayerInputs();
        _playerInputs.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        _inputDirection = _playerInputs.Character.Move.ReadValue<Vector2>();
        _moveDirection = InputDirectionToCameraLookDirection(_inputDirection, _cameraTransform);
        _rigidbody.AddForce(_moveDirection * _movementSpeed * Time.deltaTime, ForceMode.VelocityChange);
        Debug.Log($"{_moveDirection} vel: {_rigidbody.velocity.magnitude}");
    }

    private Vector3 InputDirectionToCameraLookDirection(Vector2 inputDirection, Transform cameraTransform)
    {
        Vector3 calibrationVector = _cameraTransform.right * _inputDirection.x + _cameraTransform.forward * _inputDirection.y;
        calibrationVector.y = 0;
        return calibrationVector.normalized;
    }
}
