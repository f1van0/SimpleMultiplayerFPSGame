﻿using System;
using Cinemachine;
using JoyWay.Infrastructure;
using UnityEngine;
using Zenject;

namespace JoyWay.Services
{
    public class CameraService : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _fpsCamera;
        [SerializeField] private Camera _camera;

        public event Action<Vector3> LookDirectionUpdated;

        [Inject]
        public void Construct(RealtimeNetworkManager networkManager)
        {
            networkManager.Connected += EnableFpsCamera;
            networkManager.Disconnected += DisableFpsCamera;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void EnableFpsCamera()
        {
            _fpsCamera.enabled = true;
        }

        private void DisableFpsCamera()
        {
            _fpsCamera.enabled = false;
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