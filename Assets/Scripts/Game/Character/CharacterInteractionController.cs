using JoyWay.Game.Projectiles;
using JoyWay.Services;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JoyWay.Game.Character
{
    public class CharacterInteractionController : NetworkBehaviour
    {
        [SerializeField] private Transform _handEndTransform;
        [SerializeField] private float _maxInteractionDistance;
        
        private InputService _inputService;
        private CharacterLookController _lookController;

        private Stone _stone;

        public void Initialize(InputService inputService, CharacterLookController lookController)
        {
            if (!isOwned)
                return;
            
            _inputService = inputService;
            _inputService.Interact += Interact;
            _lookController = lookController;
        }
        
        private void Interact()
        {
            Transform cameraTransform = _lookController.GetCameraTransform();
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

        private void OnDestroy()
        {
            if (isOwned)
                _inputService.Interact -= Interact;
        }
    }
}