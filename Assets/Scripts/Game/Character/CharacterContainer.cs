using System;
using JoyWay.Infrastructure;
using JoyWay.Infrastructure.Factories;
using JoyWay.Services;
using Mirror;
using UnityEngine;
using Zenject;

namespace JoyWay.Game.Character
{
    public class CharacterContainer : NetworkBehaviour
    {
        [SerializeField] private CharacterHealth _characterHealth;
        [SerializeField] private CharacterMovementController _movementController;
        [SerializeField] private CharacterShootingController _shootingController;
        [SerializeField] private CharacterInteractionController _interactionController;
        [SerializeField] private CharacterLookController _lookController;
        [SerializeField] private CharacterView _view;

        [Inject]
        public void Construct(
            InputService inputService,
            CameraService cameraService,
            ProjectileFactory projectileFactory)
        {
            _characterHealth.Initialize();
            
            _lookController.Initialize(cameraService);
            _movementController.Initialize(inputService, _lookController);
            _interactionController.Initialize(inputService, _lookController);
            
            _shootingController.Initialize(inputService, _lookController, projectileFactory);
            _view.Initialize(_characterHealth, _lookController);
        }
        
        private void Start()
        {
            Debug.Log(isOwned);
            AdvancedNetworkManager.singleton.NotifyCharacterWasSpawned(this);
        }

        public CharacterHealth GetCharacterHealthComponent()
        {
            return _characterHealth;
        }
    }
}