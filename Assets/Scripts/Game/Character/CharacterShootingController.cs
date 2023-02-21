using JoyWay.Infrastructure.Factories;
using JoyWay.Services;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JoyWay.Game.Character
{
    public class CharacterShootingController : NetworkBehaviour
    {
        [SerializeField] private Transform _handEndTransform;
        
        private PlayerInputs _playerInputs;
        private CharacterLookController _lookController;
        private ProjectileFactory _projectileFactory;

        private Vector3 _lookDirection;

        public void Initialize(PlayerInputs playerInputs, CharacterLookController lookController, ProjectileFactory projectileFactory)
        {
            if (isOwned)
            {
                _playerInputs = playerInputs;
                _playerInputs.Character.Fire.performed += Fire;
                _lookController = lookController;
            }

            if (isServer)
            {
                _projectileFactory = projectileFactory;
            }
        }

        private void Fire(InputAction.CallbackContext obj)
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
            if (isOwned)
                _playerInputs.Character.Fire.performed -= Fire;
        }
    }
}