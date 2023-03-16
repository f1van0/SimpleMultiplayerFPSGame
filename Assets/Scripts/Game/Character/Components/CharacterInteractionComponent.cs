using JoyWay.Game.Interactable;
using JoyWay.Game.Projectiles;
using JoyWay.Services;
using Mirror;
using Normal.Realtime;
using UnityEngine;

namespace JoyWay.Game.Character.Components
{
    public class CharacterInteractionComponent : MonoBehaviour
    {
        [SerializeField] private Transform _handEndTransform;
        
        private InputService _inputService;
        private Transform _cameraTransform;
        private float _maxInteractionDistance;
        
        private PickableProjectile _objectInHand;
        private RealtimeView _realtimeView;

        public void Setup(float maxInteractionDistance)
        {
            _maxInteractionDistance = maxInteractionDistance;
        }
        
        public void Initialize(CameraService cameraService, RealtimeView realtimeView)
        {
            _cameraTransform = cameraService.GetCameraTransform();
            _realtimeView = realtimeView;
        }
        
        public void Interact()
        {
            Vector3 position = _cameraTransform.position;
            Vector3 direction = _cameraTransform.forward;
            
            if (_objectInHand != null)
            {
                _objectInHand.Throw(direction);
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

        private bool TryPickupObject(Transform hitTransform, out PickableProjectile pickableProjectile)
        {
            pickableProjectile = null;
            
            if (hitTransform.TryGetComponent<PickableProjectile>(out pickableProjectile))
                return true;
            else
                return false;
        }

        public bool TryGetInteractiveObject(Transform hitTransform, out IInteractable interactableObject)
        {
            interactableObject = null;

            if (hitTransform.TryGetComponent<IInteractable>(out interactableObject))
                return true;
            else
                return false;
        }
        
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