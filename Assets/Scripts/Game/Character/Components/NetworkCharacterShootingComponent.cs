using JoyWay.Infrastructure.Factories;
using Mirror;
using UnityEngine;

namespace JoyWay.Game.Character.Components
{
    public class NetworkCharacterShootingComponent : NetworkBehaviour
    {
        [SerializeField] private Transform _handEndTransform;
        
        private NetworkCharacterLookComponent _lookComponent;
        private ProjectileFactory _projectileFactory;

        private Vector3 _lookDirection;

        public void Initialize(NetworkCharacterLookComponent lookComponent, ProjectileFactory projectileFactory)
        {
            _lookComponent = lookComponent;
            _projectileFactory = projectileFactory;
        }

        public void Fire()
        {
            _lookDirection = _lookComponent.GetLookDirection();
            CmdFire(_handEndTransform.position, _lookDirection);
        }

        [Command]
        private void CmdFire(Vector3 position, Vector3 lookDirection)
        {
            _projectileFactory.CreateFireball(position, lookDirection, netIdentity.netId);
        }
    }
}