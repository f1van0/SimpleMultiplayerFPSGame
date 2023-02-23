using Cysharp.Threading.Tasks.Triggers;
using JoyWay.Infrastructure;
using JoyWay.Infrastructure.Factories;
using JoyWay.Services;
using UnityEngine;
using Zenject;

namespace JoyWay.Game.Character
{
    public class NetworkCharacter : AdvancedNetworkBehaviour
    {
        [SerializeField] private CharacterContainer _container;
        private CameraService _cameraService;
        private InputService _inputService;

        [Inject]
        public void Construct(
            InputService inputService,
            CameraService cameraService,
            ProjectileFactory projectileFactory)
        {
            _inputService = inputService;
            _cameraService = cameraService;
            
            if (isServer)
            {
                _container.HealthComponent.Initialize();
            }

            if (isOwned)
            {
                _inputService.Move += _container.MovementComponent.Move;
                _inputService.Jump += _container.MovementComponent.Jump;
                _inputService.Interact += _container.InteractionComponent.Interact;
                _inputService.Fire += _container.ShootingComponent.Fire;
                _cameraService.LookDirectionUpdated += _container.LookComponent.UpdateLookDirection;
                
                _container.LookComponent.Initialize(_cameraService);
                _container.MovementComponent.Initialize(_container.LookComponent);
                _container.InteractionComponent.Initialize(_container.LookComponent);
            }
            
            _container.LookComponent.LookDirectionChanged += _container.ViewComponent.ChangeLookDirection;
            _container.HealthComponent.HealthChanged += (_, __) => _container.ViewComponent.DisplayDamageTaken();
            _container.HealthComponent.HealthChanged += _container.HealthBarUI.SetHealth;
            
            _container.ShootingComponent.Initialize(_container.LookComponent, projectileFactory);
            _container.ViewComponent.Initialize(isOwned);
            _container.HealthBarUI.Initialize(_container.HealthComponent.Health, _container.HealthComponent.MaxHealth);
        }
        
        private void Start()
        {
            _isOwnedCached = isOwned;
            AdvancedNetworkManager.singleton.NotifyCharacterWasSpawned(this);
        }

        private void OnDestroy()
        {
            if (_isOwnedCached)
            {
                _cameraService.LookDirectionUpdated -= _container.LookComponent.UpdateLookDirection;
                _inputService.Move -= _container.MovementComponent.Move;
                _inputService.Jump -= _container.MovementComponent.Jump;
                _inputService.Interact -= _container.InteractionComponent.Interact;
                _inputService.Fire -= _container.ShootingComponent.Fire;
            }
            
            _container.LookComponent.LookDirectionChanged -= _container.ViewComponent.ChangeLookDirection;
            _container.HealthComponent.HealthChanged -= _container.HealthBarUI.SetHealth;
        }
    }
}