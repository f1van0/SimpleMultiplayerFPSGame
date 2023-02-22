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
            PlayerInputs playerInputs,
            CameraService cameraService,
            ProjectileFactory projectileFactory)
        {
            //if (isServer)
            //{
                _characterHealth.Initialize();
            //}
            
            //if (isLocalPlayer)
            //{
                _lookController.Initialize(cameraService);
                _movementController.Initialize(playerInputs, _lookController);
                _interactionController.Initialize(playerInputs, _lookController);
            //}
            
            //TODO: change cameraService to conrtoller that can send information about camera rotation
            _shootingController.Initialize(playerInputs, _lookController, projectileFactory);
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