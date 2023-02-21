using System;
using Cinemachine;
using Game;
using UnityEngine;

namespace JoyWay.Services
{
    public class CameraService : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera _fpsCamera;

        private Camera _camera;

        public Action<Vector3> LookDirectionUpdated;

        public void Initialize()
        {
            _camera = Camera.main;
        }

        public void SetFollowTarget(Transform targetTransform)
        {
            _fpsCamera.Follow = targetTransform;
        }

        public Transform GetCameraTransform()
        {
            return _camera.transform;
        }

        private void LateUpdate()
        {
            LookDirectionUpdated?.Invoke(GetLookDirection());
        }

        public Vector3 GetLookDirection()
        {
            return _camera.transform.forward;
        }
    }
}