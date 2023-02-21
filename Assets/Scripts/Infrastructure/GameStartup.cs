using Events.Game;
using JoyWay.Infrastructure.Factories;
using JoyWay.Resources;
using JoyWay.Services;
using MessagePipe;
using UnityEngine;
using Zenject;

namespace JoyWay.Infrastructure
{
    public class GameStartup : IInitializable
    {
        private AdvancedNetworkManager _networkManager;
        private CharacterFactory _characterFactory;
        private SceneLoader _sceneLoader;
        private AssetContainer _assetContainer;

        private IPublisher<CharacterSpawnedEvent> _publisher;
        private ISubscriber<CharacterSpawnedEvent> _subscriber;

        public GameStartup(
            AdvancedNetworkManager networkManager,
            CharacterFactory characterFactory,
            SceneLoader sceneLoader,
            IPublisher<CharacterSpawnedEvent> publisher,
            ISubscriber<CharacterSpawnedEvent> subscriber)
        {
            _networkManager = networkManager;
            _characterFactory = characterFactory;
            _sceneLoader = sceneLoader;
            _publisher = publisher;
            _subscriber = subscriber;
        }

        public void Initialize()
        {
            _sceneLoader.Load(Constants.GameScene, SceneLoaded);
        }

        private void SceneLoaded()
        {
            _networkManager.Initialize(_characterFactory, _publisher, _subscriber);
            _networkManager.StartHost();
        }
    }
}