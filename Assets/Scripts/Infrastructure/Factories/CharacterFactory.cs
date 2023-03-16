using JoyWay.Game.Character;
using JoyWay.Resources;
using JoyWay.Services;
using Mirror;
using Normal.Realtime;
using UnityEngine;

namespace JoyWay.Infrastructure.Factories
{
    public class CharacterFactory
    {
        private AssetContainer _assetContainer;
        private InputService _inputSrevice;
        private CameraService _cameraService;
        private ProjectileFactory _projectileFactory;

        public CharacterFactory(
            AssetContainer assetContainer,
            InputService inputService,
            CameraService cameraService,
            ProjectileFactory projectileFactory)
        {
            _assetContainer = assetContainer;
            _inputSrevice = inputService;
            _cameraService = cameraService;
            _projectileFactory = projectileFactory;
        }

        public CharacterContainer CreateCharacter(Transform at, Realtime realtime)
        {
            var options = new Realtime.InstantiateOptions {
                ownedByClient            = true,
                preventOwnershipTakeover = true,
                destroyWhenOwnerLeaves   = true,
                useInstance              = realtime
            };
            
            GameObject character = Realtime.Instantiate(ResourcesPath.Character, options);
            character.transform.position = at.position;
            CharacterContainer characterContainer = character.GetComponent<CharacterContainer>();

            CharacterConfig characterConfig = _assetContainer.CharacterConfig.Value;
            
            characterContainer.HealthComponent.Setup(characterConfig.MaxHealth);
            characterContainer.InteractionComponent.Setup(characterConfig.MaxInteractionDistance);
            characterContainer.DamageDisplayComponent.Setup(characterConfig.DisplayDamageTakenDelay);
            characterContainer.NetworkCharacter.Initialize(_inputSrevice, _cameraService, _projectileFactory);

            characterContainer.MovementComponent.Setup(
                characterConfig.MaxSpeed,
                characterConfig.MovementForce,
                characterConfig.JumpForce,
                characterConfig.GroundDrag,
                characterConfig.AirDrag);
            
            return characterContainer;
        }
    }
}