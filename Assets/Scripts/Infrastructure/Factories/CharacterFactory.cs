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
        private ISubscriber<CharacterSpawnedEvent> _subscriber;
        private AssetContainer _assetContainer;

        public CharacterFactory(
            AssetContainer assetContainer,
            DiContainer diContainer,
            ISubscriber<CharacterSpawnedEvent> subscriber)
        {
            _assetContainer = assetContainer;
            _diContainer = diContainer;
            _subscriber = subscriber;
            _subscriber.Subscribe(x => InitializeSpawnedCharacter(x.CharacterContainer));
        }

        public CharacterContainer CreateCharacter(Transform at, NetworkConnectionToClient player)
        {
            CharacterContainer characterContainer = Object.Instantiate(_assetContainer.Character.Value, at.position, at.rotation);
            NetworkServer.Spawn(characterContainer.gameObject, player);
            return characterContainer;
        }

        private void InitializeSpawnedCharacter(CharacterContainer characterContainer)
        {
            _diContainer.Inject(characterContainer);
        }
    }
}