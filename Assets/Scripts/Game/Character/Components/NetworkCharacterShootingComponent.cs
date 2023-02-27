using JoyWay.Infrastructure.Factories;
using JoyWay.Services;
using Mirror;
using UnityEngine;

namespace JoyWay.Game.Character.Components
{
    public class NetworkCharacterShootingComponent : NetworkBehaviour
    {
        [SerializeField] private Transform _handEndTransform;
        
        private ProjectileFactory _projectileFactory;

        private Transform _cameraTransform;
        private Vector3 _lookDirection;

        public void Initialize(CameraService cameraService, ProjectileFactory projectileFactory)
        {
            _cameraTransform = cameraService.GetCameraTransform();
            _projectileFactory = projectileFactory;
        }

        public void Fire()
        {
            CmdFire(_handEndTransform.position, _cameraTransform.forward);
        }

        [Command]
        private void CmdFire(Vector3 position, Vector3 lookDirection)
        {
            _projectileFactory.CreateFireball(position, lookDirection, netIdentity.netId);
        }
    }
}