using System;
using JoyWay.Services;
using Mirror;
using UnityEngine;

namespace JoyWay.Game.Character
{
    public class CharacterLookController : AdvancedNetworkBehaviour
    {
        public Action<Vector3> LookDirectionChanged;

        [SerializeField] private Transform _eyes;
        
        [SyncVar(hook = nameof(SetLookDirection))]
        private Vector3 _lookDirection;
        
        private CameraService _cameraService;

        public void Initialize(CameraService cameraService)
        {
            //TODO: Divide into 2 parts:
            //TODO: 1 part - localplayer who subscribe Action LookDirectionChanged from CameraService directly
            //TODO: 2 part - remoteClients who subscribe on Action LookDirectionChanged from changing syncvar LookDirection

            _isOwnedCached = isOwned;
            if (!_isOwnedCached)
                return;

            _cameraService = cameraService;
            _cameraService.SetFollowTarget(_eyes);
            _cameraService.LookDirectionUpdated += UpdateLookDirection;
        }

        private void UpdateLookDirection(Vector3 direction)
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
            if (!_isOwnedCached)
                LookDirectionChanged?.Invoke(newLookDirection);
        }

        public Transform GetCameraTransform()
        {
            return _cameraService.GetCameraTransform();
        }

        public Vector3 GetLookDirection()
        {
            return _cameraService.GetLookDirection();
        }

        private void OnDestroy()
        {
            if (_isOwnedCached)
                _cameraService.LookDirectionUpdated -= UpdateLookDirection;
        }
    }
}