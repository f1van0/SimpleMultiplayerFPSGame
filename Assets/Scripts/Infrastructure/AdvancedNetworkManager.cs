using System;
using System.Linq;
using Events.Game;
using JoyWay.Game;
using JoyWay.Game.Character;
using JoyWay.Infrastructure.Factories;
using JoyWay.Services;
using MessagePipe;
using Mirror;
using Mirror.SimpleWeb;
using UnityEngine;
using Zenject;

namespace JoyWay.Infrastructure
{
    public class AdvancedNetworkManager : NetworkManager
    {
        public static new AdvancedNetworkManager singleton { get; private set; }
        
        public event Action Connected;
        public event Action Disconnected;
        
        private LevelSpawnPoints _levelSpawnPoints;
        private CharacterFactory _characterFactory;
        private IPublisher<CharacterSpawnedEvent> _publisher;
        private ISubscriber<CharacterSpawnedEvent> _subscriber;

        public void Initialize(
            CharacterFactory characterFactory,
            IPublisher<CharacterSpawnedEvent> characterSpawnedPublisher,
            ISubscriber<CharacterSpawnedEvent> characterSpawnedSubscriber)
        {
            _characterFactory = characterFactory;
            _publisher = characterSpawnedPublisher;
            _subscriber = characterSpawnedSubscriber;
            _levelSpawnPoints = FindObjectOfType<LevelSpawnPoints>();
        }
        
        public override void Awake()
        {
            base.Awake();
            singleton = this;
        }

        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            SpawnCharacter(conn);
        }

        public override void OnStartClient()
        {
            Connected?.Invoke();
        }

        public override void OnStopClient()
        {
            Disconnected?.Invoke();
        }

        public override void OnStartHost()
        {
            Connected?.Invoke();
        }

        public override void OnStopHost()
        {
            Disconnected?.Invoke();
        }

        public void NotifyCharacterWasSpawned(CharacterContainer characterContainer)
        {
            _publisher.Publish(new CharacterSpawnedEvent(characterContainer));
        }
        
        private void SpawnCharacter(NetworkConnectionToClient player)
        {
            //TODO: make random spawn points
            Transform spawnPoint = _levelSpawnPoints.GetSpawnPoints().First();
            _characterFactory.CreateCharacter(spawnPoint, player);
        }
    }
}
