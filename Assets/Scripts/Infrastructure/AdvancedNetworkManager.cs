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
        public new static AdvancedNetworkManager singleton { get; private set; }
        
        public event Action Connected;
        public event Action Disconnected;
        
        private LevelSpawnPoints _levelSpawnPoints;
        private CharacterFactory _characterFactory;
        private IPublisher<NetworkCharacterSpawnedEvent> _publisher;

        [Inject]
        public void Construct(
            CharacterFactory characterFactory,
            IPublisher<NetworkCharacterSpawnedEvent> characterSpawnedPublisher)
        {
            _characterFactory = characterFactory;
            _publisher = characterSpawnedPublisher;
        }
        
        public override void Awake()
        {
            base.Awake();
            singleton = this;
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            Connected?.Invoke();
        }

        public override void OnStopClient()
        {
            base.OnStopClient();
            Disconnected?.Invoke();
        }

        public override void OnStartHost()
        {
            base.OnStartHost();
            Connected?.Invoke();
        }

        public override void OnStopHost()
        {
            base.OnStopHost();
            Disconnected?.Invoke();
        }

        public void NotifyCharacterWasSpawned(NetworkCharacter character)
        {
            _publisher.Publish(new NetworkCharacterSpawnedEvent(character));
        }

        private void RespawnCharacter(NetworkCharacterHealthComponent networkCharacterHealthComponent)
        {
            networkCharacterHealthComponent.Died -= RespawnCharacter;
            var conn = networkCharacterHealthComponent.netIdentity.connectionToClient;
            NetworkServer.Destroy(networkCharacterHealthComponent.gameObject);
            SpawnCharacter(conn);
        }

        private void SpawnCharacter(NetworkConnectionToClient conn)
        {
            Transform spawnPoint = GetRandomSpawnPoint();
            var character = _characterFactory.CreateCharacter(spawnPoint, conn);
            var characterHealth = character.HealthComponent;
            characterHealth.Died += RespawnCharacter;
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);
            SpawnCharacter(conn);
        }

        private Transform GetRandomSpawnPoint()
        {
            if (_levelSpawnPoints == null)
                _levelSpawnPoints = FindObjectOfType<LevelSpawnPoints>();

            return _levelSpawnPoints.GetRandomSpawnPoint();
        }
    }
}
