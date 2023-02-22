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

        [Inject]
        public void Construct()
        {
            _playerInputs = new PlayerInputs();
            _playerInputs.Character.Jump.performed += x => Jump?.Invoke();
            _playerInputs.Character.Fire.performed += x => Fire?.Invoke();
            _playerInputs.Character.Interact.performed += x => Interact?.Invoke();
            _playerInputs.Enable();
        }

        private void FixedUpdate()
        {
            _moveDirection = _playerInputs.Character.Move.ReadValue<Vector2>();
            Move?.Invoke(_moveDirection);
        }
    }
}