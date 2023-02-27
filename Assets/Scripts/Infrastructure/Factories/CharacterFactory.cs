using JoyWay.Game.Character;
using JoyWay.Services;
using Mirror;
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
            NetworkClient.RegisterPrefab(_assetContainer.Character.Value.gameObject, SpawnCharacterOnClient, UnspawnCharacterOnClient);
        }

        public CharacterContainer SpawnCharacterOnServer(Transform at, NetworkConnectionToClient conn)
        {
            bool isOwner = conn.identity.isOwned;
            var characterContainer = CreateCharacter(at.position, at.rotation, isOwner, true);
            NetworkServer.Spawn(characterContainer.gameObject, conn);
            return characterContainer;
        }

        private GameObject SpawnCharacterOnClient(SpawnMessage msg)
        {
            var characterContainer = CreateCharacter(msg.position, msg.rotation, msg.isOwner, false);
            return characterContainer.gameObject;
        }


        private void UnspawnCharacterOnClient(GameObject spawned)
        {
            Object.Destroy(spawned);
        }

        private CharacterContainer CreateCharacter(Vector3 position, Quaternion rotation, bool isOwner, bool isHost)
        {
            CharacterContainer characterContainer = 
                Object.Instantiate(_assetContainer.Character.Value, position, rotation);

            CharacterConfig characterConfig = _assetContainer.CharacterConfig.Value;
            
            characterContainer.HealthComponent.Setup(characterConfig.MaxHealth);
            characterContainer.InteractionComponent.Setup(characterConfig.MaxInteractionDistance);
            characterContainer.LookComponent.Setup(characterConfig.InterpolationTimeInterval);
            characterContainer.DamageDisplayComponent.Setup(characterConfig.DisplayDamageTakenDelay);
            characterContainer.NetworkCharacter.Initialize(isOwner, isHost, _inputSrevice, _cameraService, _projectileFactory);

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