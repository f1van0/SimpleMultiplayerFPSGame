using System;
using JoyWay.Services;
using Mirror;
using UnityEngine;

namespace JoyWay.Game.Character.Components
{
    public class NetworkCharacterLookComponent : NetworkBehaviour
    {
        public event Action<Vector3> LookDirectionChanged;

        [SerializeField] private Transform _eyes;

        [SyncVar(hook = nameof(SetLookDirection))]
        private Vector3 _lookDirection;
        
        private CameraService _cameraService;

        private Vector3 _newLookDirection;
        private Vector3 _currentLookDirection;
        private float _interpolationTimeInterval;
        private float _timer;

        public void Setup(float interpolationTimeInterval)
        {
            _interpolationTimeInterval = interpolationTimeInterval;
        }

        public void Initialize(CameraService cameraService)
        {
            _cameraService = cameraService;
            _cameraService.SetFollowTarget(_eyes);
        }

        public void UpdateLookDirection(Vector3 direction)
        {
            LookDirectionChanged?.Invoke(direction);
            CmdChangeLookDirection(direction);
        }

        [Command]
        private void CmdChangeLookDirection(Vector3 direction)
        {
            _lookDirection = direction;
        }

        private void SetLookDirection(Vector3 oldLookDirection, Vector3 newLookDirection)
        {
            _timer = 0;
            _newLookDirection = newLookDirection;
        }

        private void Update()
        {
            UpdateLookDirectionByInterpolation();
        }

        private void UpdateLookDirectionByInterpolation()
        {
            if (isOwned)
                return;

            _timer += Time.deltaTime;

            _currentLookDirection =
                Vector3.Lerp(_currentLookDirection, _newLookDirection, _timer / _interpolationTimeInterval);

            LookDirectionChanged?.Invoke(_currentLookDirection);
        }
    }
}