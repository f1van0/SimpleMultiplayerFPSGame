using JoyWay.Infrastructure.Factories;
using JoyWay.Services;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JoyWay.Game.Character
{
    public class CharacterShootingController : AdvancedNetworkBehaviour
    {
        [SerializeField] private Transform _handEndTransform;
        
        private InputService _inputService;
        private CharacterLookController _lookController;
        private ProjectileFactory _projectileFactory;

        private Vector3 _lookDirection;

        public void Initialize(InputService inputService, CharacterLookController lookController, ProjectileFactory projectileFactory)
        {
            _isOwnedCached = isOwned;
            if (_isOwnedCached)
            {
                _inputService = inputService;
                _inputService.Fire += Fire;
                _lookController = lookController;
            }

            if (isServer)
            {
                _projectileFactory = projectileFactory;
            }
        }

        private void Fire()
        {
            _lookDirection = _lookController.GetLookDirection();
            CmdFire(_handEndTransform.position, _lookDirection);
        }

        [Command]
        private void CmdFire(Vector3 position, Vector3 lookDirection)
        {
            _projectileFactory.CreateFireball(position, lookDirection);
        }

        private void OnDestroy()
        {
            if (_isOwnedCached)
                _inputService.Fire -= Fire;
        }
    }
}