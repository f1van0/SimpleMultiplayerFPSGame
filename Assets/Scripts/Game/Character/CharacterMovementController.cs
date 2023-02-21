using JoyWay.Services;
using Mirror;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JoyWay.Game.Character
{
    public class CharacterMovementController : NetworkBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _movementForce;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _groundDrag;
        [SerializeField] private float _groundRaycastLength;
        [SerializeField] private LayerMask _terrainLayer;

        private PlayerInputs _playerInputs;
        private CharacterLookController _lookController;

        private Vector3 _moveDirection;
        private Vector2 _inputDirection;
        private Transform _cameraTransform;
        private bool _isGrounded;

        public void Initialize(PlayerInputs playerInputs, CharacterLookController lookController)
        {
            if (!isOwned)
                return;
                
            _playerInputs = playerInputs;
            _playerInputs.Character.Jump.performed += Jump;
            _lookController = lookController;
            _cameraTransform = _lookController.GetCameraTransform();
        }
        
        private void Move()
        {
            _inputDirection = _playerInputs.Character.Move.ReadValue<Vector2>();
            _moveDirection = InputDirectionToCameraLookDirection(_inputDirection);
            CmdPerformMove(_moveDirection);
        }
        
        private void Jump(InputAction.CallbackContext obj)
        {
            CmdPerformJump();
        }

        private void FixedUpdate()
        {
            Move();
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
            //TODO: make this on server, but i think its made already beacuse of the command
            if (isServer && !isLocalPlayer)
                Debug.Log("calls remotely from client and runs on server");
            
            if (CheckGrounded())
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        [Server]
        private void ApplyDrag()
        {
            if (CheckGrounded())
                _rigidbody.drag = _groundDrag;
            else
                _rigidbody.drag = 0;
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
                _cameraTransform.right * _inputDirection.x + 
                _cameraTransform.forward * _inputDirection.y;
            calibrationVector.y = 0;
            return calibrationVector.normalized;
        }

        private bool CheckGrounded()
        {
            Ray rayToGround = new Ray(transform.position, -transform.up);
            bool isGrounded = Physics.Raycast(rayToGround, _groundRaycastLength, _terrainLayer.value);
            return isGrounded;
        }

        private void OnDestroy()
        {
            if (isLocalPlayer)
                _playerInputs.Character.Jump.performed -= Jump;
        }
    }
}
