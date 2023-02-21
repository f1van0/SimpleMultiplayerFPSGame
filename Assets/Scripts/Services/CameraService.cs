using System;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace JoyWay.Services
{
    public class CameraService : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _fpsCamera;
        [SerializeField] private Camera _camera;

        public Action<Vector3> LookDirectionUpdated;

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