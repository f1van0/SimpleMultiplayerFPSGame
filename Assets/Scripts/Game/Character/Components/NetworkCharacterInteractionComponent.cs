﻿using JoyWay.Game.Interactable;
using JoyWay.Game.Projectiles;
using JoyWay.Services;
using Mirror;
using UnityEngine;

namespace JoyWay.Game.Character.Components
{
    public class NetworkCharacterInteractionComponent : NetworkBehaviour
    {
        [SerializeField] private Transform _handEndTransform;
        
        private InputService _inputService;
        private Transform _cameraTransform;
        private float _maxInteractionDistance;
        
        private PickableProjectile _objectInHand;

        public void Setup(float maxInteractionDistance)
        {
            _maxInteractionDistance = maxInteractionDistance;
        }
        
        public void Initialize(CameraService cameraService)
        {
            _cameraTransform = cameraService.GetCameraTransform();
        }
        
        public void Interact()
        {
            CmdHandleInteraction(_cameraTransform.position, _cameraTransform.forward);
        }

        [Command]
        private void CmdHandleInteraction(Vector3 position, Vector3 direction)
        {
            if (_objectInHand != null)
            {
                _objectInHand.Throw(direction, netIdentity.netId);
                _objectInHand = null;
                return;
            }

            Transform hitTransform = GetRaycastHitTransform(position, direction);

            if (hitTransform == null)
                return;
            
            if (TryPickupObject(hitTransform, out var pickableObject))
            {
                if (pickableObject.CanPick)
                {
                    pickableObject.Pickup(_handEndTransform);
                    _objectInHand = pickableObject;
                }
                return;
            }

            if (TryGetInteractiveObject(hitTransform, out var interactableObject))
            {
                interactableObject.Interact();
            }
        }

        [Server]
        private bool TryPickupObject(Transform hitTransform, out PickableProjectile pickableProjectile)
        {
            pickableProjectile = null;
            
            if (hitTransform.TryGetComponent<PickableProjectile>(out pickableProjectile))
                return true;
            else
                return false;
        }

        [Server]
        public bool TryGetInteractiveObject(Transform hitTransform, out IInteractable interactableObject)
        {
            interactableObject = null;

            if (hitTransform.TryGetComponent<IInteractable>(out interactableObject))
                return true;
            else
                return false;
        }
        
        [Server]
        private Transform GetRaycastHitTransform(Vector3 position, Vector3 direction)
        {
            Ray ray = new Ray(position, direction);
            RaycastHit raycastHit;
            Physics.Raycast(ray, out raycastHit, _maxInteractionDistance);
            Transform hitTransform = raycastHit.transform;
            return hitTransform;
        }
    }
}