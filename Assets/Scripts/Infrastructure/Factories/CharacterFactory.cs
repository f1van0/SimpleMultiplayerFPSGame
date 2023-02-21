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
        private PlayerInputs _playerInputs;
        private CameraService _cameraService;
        private AssetContainer _assetContainer;

        private ISubscriber<CharacterSpawnedEvent> _subscriber;
        private ProjectileFactory _projectileFactory;

        public CharacterFactory(
            PlayerInputs playerInputs,
            CameraService cameraService,
            AssetContainer assetContainer, 
            ISubscriber<CharacterSpawnedEvent> subscriber,
            ProjectileFactory projectileFactory)
        {
            _playerInputs = playerInputs;
            _cameraService = cameraService;
            _assetContainer = assetContainer;
            _subscriber = subscriber;
            _subscriber.Subscribe(x => InitializeSpawnedCharacter(x.CharacterContainer));
            _projectileFactory = projectileFactory;
        }

        public CharacterContainer CreateCharacter(Transform at, NetworkConnectionToClient player)
        {
            CharacterContainer characterContainer = Object.Instantiate(_assetContainer.Character.Value, at.position, at.rotation);
            NetworkServer.Spawn(characterContainer.gameObject, player);
            return characterContainer;
        }

        private void InitializeSpawnedCharacter(CharacterContainer characterContainer)
        {
            characterContainer.Initialize(_playerInputs, _cameraService, _projectileFactory);
        }
    }
}