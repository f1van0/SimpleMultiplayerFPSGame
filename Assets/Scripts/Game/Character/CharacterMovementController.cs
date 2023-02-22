using JoyWay.Services;
using Mirror;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JoyWay.Game.Character
{
    public class CharacterMovementController : AdvancedNetworkBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _movementForce;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _groundDrag;
        [SerializeField] private float _airDrag;
        [SerializeField] private float _groundRaycastLength;
        
        private InputService _inputService;
        private CharacterLookController _lookController;

        private Vector3 _moveDirection;
        private Transform _cameraTransform;
        private bool _isGrounded;

        public void Initialize(InputService inputService, CharacterLookController lookController)
        {
            _isOwnedCached = isOwned;
            if (!_isOwnedCached)
                return;
                
            _inputService = inputService;
            _inputService.Move += Move;
            _inputService.Jump += Jump;
            _lookController = lookController;
            _cameraTransform = _lookController.GetCameraTransform();
        }
        
        [Client]
        private void Move(Vector2 moveDirection)
        {
            _moveDirection = InputDirectionToCameraLookDirection(moveDirection);
            CmdPerformMove(_moveDirection);
        }
        
        private void Jump()
        {
            CmdPerformJump();
        }

        [Command]
        private void CmdPerformMove(Vector3 direction)
        {
            ApplyDrag();
            _rigidbody.AddForce(direction * _movementForce, ForceMode.Force);
            ClampMovement();
        }

        [Command]
        private void CmdPerformJump()
        {
            if (CheckGrounded())
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        [Server]
        private void ApplyDrag()
        {
            if (CheckGrounded())
                _rigidbody.drag = _groundDrag;
            else
                _rigidbody.drag = _airDrag;
        }

        [Server]
        private void ClampMovement()
        {
            var velocity = _rigidbody.velocity;
            Vector3 flatMovement = new Vector3(velocity.x, 0, velocity.z);
            Vector3 clamped = Vector3.ClampMagnitude(flatMovement, _maxSpeed);
            _rigidbody.velocity = new Vector3(clamped.x, velocity.y, clamped.z);
        }

        private Vector3 InputDirectionToCameraLookDirection(Vector2 inputDirection)
        {
            Vector3 calibrationVector =
                _cameraTransform.right * inputDirection.x + 
                _cameraTransform.forward * inputDirection.y;
            calibrationVector.y = 0;
            return calibrationVector.normalized;
        }

        private bool CheckGrounded()
        {
            Ray rayToGround = new Ray(transform.position, -transform.up);
            bool isGrounded = Physics.Raycast(rayToGround, _groundRaycastLength);
            return isGrounded;
        }

        private void OnDestroy()
        {
            if (_isOwnedCached)
            {
                _inputService.Move -= Move;
                _inputService.Jump -= Jump;
            }
        }
    }
}
