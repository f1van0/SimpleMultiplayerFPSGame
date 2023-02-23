using System;
using JoyWay.Services;
using Mirror;
using UnityEngine;

namespace JoyWay.Game.Character
{
    public class NetworkCharacterLookComponent : NetworkBehaviour
    {
        public event Action<Vector3> LookDirectionChanged;

        [SerializeField] private Transform _eyes;
        
        [SyncVar(hook = nameof(SetLookDirection))]
        private Vector3 _lookDirection;
        
        private CameraService _cameraService;

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
            if (!isOwned)
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
    }
}