using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace JoyWay.Services
{
    public class InputService : MonoBehaviour
    {
        public event Action<Vector2> Move;
        public event Action Jump;
        public event Action Fire;
        public event Action Interact;

        private PlayerInputs _playerInputs;

        private Vector2 _moveDirection;

        private void OnInteract(InputAction.CallbackContext x) => Interact?.Invoke();
        private void OnFire(InputAction.CallbackContext x) => Fire?.Invoke();
        private void OnJump(InputAction.CallbackContext x) => Jump?.Invoke();
        
        [Inject]
        public void Construct()
        {
            _playerInputs = new PlayerInputs();
            _playerInputs.Character.Jump.performed += OnJump;
            _playerInputs.Character.Fire.performed += OnFire;
            _playerInputs.Character.Interact.performed += OnInteract;
            _playerInputs.Enable();
        }

        private void FixedUpdate()
        {
            _moveDirection = _playerInputs.Character.Move.ReadValue<Vector2>();
            Move?.Invoke(_moveDirection);
        }

        private void OnDestroy()
        {
            _playerInputs.Character.Jump.performed -= OnJump;
            _playerInputs.Character.Fire.performed -= OnFire;
            _playerInputs.Character.Interact.performed -= OnInteract;
        }
    }
}