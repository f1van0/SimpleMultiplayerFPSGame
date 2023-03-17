using JoyWay.Infrastructure.Factories;
using JoyWay.Services;
using Normal.Realtime;
using UnityEngine;

namespace JoyWay.Game.Character.Components
{
    public class CharacterShootingComponent : MonoBehaviour
    {
        [SerializeField] private Transform _handEndTransform;

        private ProjectileFactory _projectileFactory;
        private RealtimeView _realtimeView;

        private Transform _cameraTransform;
        private Vector3 _lookDirection;

        public void Initialize(CameraService cameraService, ProjectileFactory projectileFactory, RealtimeView realtimeView)
        {
            _cameraTransform = cameraService.GetCameraTransform();
            _projectileFactory = projectileFactory;
            _realtimeView = realtimeView;
        }

        public void Fire()
        {
            _projectileFactory
                .CreateFireball(_handEndTransform.position, _cameraTransform.forward, _realtimeView.ownerIDSelf);
        }
    }
}