using JoyWay.Infrastructure.Factories;
using JoyWay.Services;
using Normal.Realtime;
using UnityEngine;

namespace JoyWay.Game.Character
{
    public class NetworkCharacter : MonoBehaviour
    {
        [SerializeField] private CharacterContainer _container;
        [SerializeField] private RealtimeView _realtimeView;
        
        private CameraService _cameraService;
        private InputService _inputService;

        private bool _isOwner;

        public void Initialize(
            InputService inputService,
            CameraService cameraService,
            ProjectileFactory projectileFactory)
        {
            _inputService = inputService;
            _cameraService = cameraService;
            _isOwner = true;

            _inputService.Move += _container.MovementComponent.Move;
            _inputService.Jump += _container.MovementComponent.Jump;
            _inputService.Interact += _container.InteractionComponent.Interact;
            _inputService.Fire += _container.ShootingComponent.Fire;
            _cameraService.LookDirectionUpdated += _container.lookComponent.ChangeLookDirection;

            _container.HealthComponent.Initialize();
            _container.lookComponent.Initialize(_cameraService);
            _container.MovementComponent.Initialize(_container.lookComponent);
            _container.InteractionComponent.Initialize(_cameraService, _realtimeView);
            _container.ShootingComponent.Initialize(_cameraService, projectileFactory, _realtimeView);
        }

        private void Start()
        {
            _container.HealthComponent.HealthChanged += (_, __) => _container.DamageDisplayComponent.DisplayDamageTaken();
            _container.HealthComponent.HealthChanged += _container.HealthBarUI.SetHealth;
            
            _container.DamageDisplayComponent.Initialize();
            _container.HealthBarUI.Initialize(_container.HealthComponent.Health, _container.HealthComponent.MaxHealth);
        }

        private void OnDestroy()
        {
            if (_isOwner)
            {
                _cameraService.LookDirectionUpdated -= _container.lookComponent.ChangeLookDirection;
                _inputService.Fire -= _container.ShootingComponent.Fire; // something wrong with unsubscribe, because after respawn character cant attack
                _inputService.Move -= _container.MovementComponent.Move;
                _inputService.Jump -= _container.MovementComponent.Jump;
                _inputService.Interact -= _container.InteractionComponent.Interact;
            }
            
            _container.HealthComponent.HealthChanged -= _container.HealthBarUI.SetHealth;
        }
    }
}