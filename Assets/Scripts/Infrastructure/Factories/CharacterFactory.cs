using Cysharp.Threading.Tasks.Triggers;
using Events.Game;
using JoyWay.Game.Character;
using JoyWay.Resources;
using JoyWay.Services;
using MessagePipe;
using Mirror;
using UnityEngine;
using Zenject;

namespace JoyWay.Infrastructure.Factories
{
    public class CharacterFactory
    {
        private DiContainer _diContainer;
        private ISubscriber<NetworkCharacterSpawnedEvent> _subscriber;
        private AssetContainer _assetContainer;

        public CharacterFactory(
            AssetContainer assetContainer,
            DiContainer diContainer,
            ISubscriber<NetworkCharacterSpawnedEvent> subscriber)
        {
            _assetContainer = assetContainer;
            _diContainer = diContainer;
            _subscriber = subscriber;
            _subscriber.Subscribe(x => InitializeSpawnedCharacter(x.Character));
        }

        public CharacterContainer CreateCharacter(Transform at, NetworkConnectionToClient conn)
        {
            CharacterContainer characterContainer = 
                Object.Instantiate(_assetContainer.Character.Value, at.position, at.rotation);

            NetworkServer.Spawn(characterContainer.gameObject, conn);
            
            CharacterConfig characterConfig = _assetContainer.CharacterConfig.Value;

            characterContainer.HealthComponent.Setup(characterConfig.MaxHealth);

            characterContainer.MovementComponent.Setup(
                characterConfig.MaxSpeed,
                characterConfig.MovementForce,
                characterConfig.JumpForce,
                characterConfig.GroundDrag,
                characterConfig.AirDrag);

            characterContainer.InteractionComponent.Setup(characterConfig.MaxInteractionDistance);

            return characterContainer;
        }

        private void InitializeSpawnedCharacter(NetworkCharacter character)
        {
            _diContainer.Inject(character);
        }
    }
}