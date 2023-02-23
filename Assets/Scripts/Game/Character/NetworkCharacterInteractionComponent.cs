using JoyWay.Game.Projectiles;
using JoyWay.Services;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JoyWay.Game.Character
{
    public class NetworkCharacterInteractionComponent : NetworkBehaviour
    {
        [SerializeField] private Transform _handEndTransform;
        
        private InputService _inputService;
        private NetworkCharacterLookComponent _lookComponent;
        private float _maxInteractionDistance;
        private Stone _stone;

        public void Setup(float maxInteractionDistance)
        {
            _maxInteractionDistance = maxInteractionDistance;
        }
        
        public void Initialize(NetworkCharacterLookComponent lookComponent)
        {
            _lookComponent = lookComponent;
        }
        
        public void Interact()
        {
            Transform cameraTransform = _lookComponent.GetCameraTransform();
            CmdHandleInteraction(cameraTransform.position, cameraTransform.forward);
        }

        [Command]
        private void CmdHandleInteraction(Vector3 position, Vector3 direction)
        {
            if (_stone != null && !_stone.CanPick)
            {
                _stone.Throw(direction);
                _stone = null;
                return;
            }

            Transform hitTransform = GetRaycastHitTransform(position, direction);

            if (hitTransform == null)
                return;
            
            if (TryPickupStone(hitTransform, out var stone))
            {
                if (stone.CanPick)
                {
                    stone.Pickup(_handEndTransform);
                    _stone = stone;
                }
                return;
            }

            if (TryGetInteractiveObject(hitTransform, out var interactableObject))
            {
                interactableObject.Interact();
            }
        }

        [Server]
        private bool TryPickupStone(Transform hitTransform, out Stone stone)
        {
            stone = null;
            
            if (hitTransform.TryGetComponent<Stone>(out stone))
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