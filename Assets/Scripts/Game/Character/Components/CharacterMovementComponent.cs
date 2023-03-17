using Normal.Realtime;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace JoyWay.Game.Character.Components
{
    public class CharacterMovementComponent : MonoBehaviour
    {
        [SerializeField] private RealtimeTransform _realtimeTransform;
        [SerializeField] private Rigidbody _rigidbody;

        private float _maxSpeed;
        private float _movementForce;
        private float _jumpForce;
        private float _groundDrag;
        private float _airDrag;
        
        private const float GroundRaycastLength = 0.2f;

        private Vector3 _moveDirection;
        private Transform _bodyTransform;
        private bool _isGrounded;

        public void Setup(float maxSpeed, float movementForce, float jumpForce, float groundDrag, float airDrag)
        {
            _maxSpeed = maxSpeed;
            _movementForce = movementForce;
            _jumpForce = jumpForce;
            _groundDrag = groundDrag;
            _airDrag = airDrag;
        }

        public void Initialize(CharacterLookComponent lookComponent)
        {
            _bodyTransform = lookComponent.GetBodyTransform();
            _realtimeTransform.RequestOwnership();
        }
        
        public void Move(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return;
            
            _moveDirection = InputDirectionToCameraLookDirection(direction);
            
            ApplyDrag();
            _rigidbody.AddForce(_moveDirection * _movementForce, ForceMode.Force);
            ClampMovement();
        }
        
        public void Jump()
        {
            if (!CheckGrounded())
                return;
            
            var flatVelocity = GetFlatVelocity();
            _rigidbody.velocity = flatVelocity;
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        private void ApplyDrag()
        {
            if (CheckGrounded())
                _rigidbody.drag = _groundDrag;
            else
                _rigidbody.drag = _airDrag;
        }

        private void ClampMovement()
        {
            var flatVelocity = GetFlatVelocity();
            Vector3 clamped = Vector3.ClampMagnitude(flatVelocity, _maxSpeed);
            _rigidbody.velocity = new Vector3(clamped.x, _rigidbody.velocity.y, clamped.z);
        }

        private Vector3 GetFlatVelocity()
        {
            var velocity = _rigidbody.velocity;
            return new Vector3(velocity.x, 0, velocity.z);
        }

        private Vector3 InputDirectionToCameraLookDirection(Vector2 inputDirection)
        {
            Vector3 calibrationVector =
                _bodyTransform.right * inputDirection.x + 
                _bodyTransform.forward * inputDirection.y;
            calibrationVector.y = 0;
            return calibrationVector.normalized;
        }

        private bool CheckGrounded()
        {
            Ray rayToGround = new Ray(transform.position + Vector3.up * GroundRaycastLength / 2, -transform.up);
            bool isGrounded = Physics.Raycast(rayToGround, GroundRaycastLength);
            return isGrounded;
        }
    }
}
