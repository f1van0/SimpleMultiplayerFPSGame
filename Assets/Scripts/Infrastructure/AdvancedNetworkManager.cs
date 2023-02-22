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

        [Inject]
        public void Construct(
            CharacterFactory characterFactory,
            IPublisher<CharacterSpawnedEvent> characterSpawnedPublisher)
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

        public void NotifyCharacterWasSpawned(CharacterContainer characterContainer)
        {
            _publisher.Publish(new CharacterSpawnedEvent(characterContainer));
        }

        private void RespawnCharacter(CharacterHealth characterHealth)
        {
            characterHealth.Died -= RespawnCharacter;
            var conn = characterHealth.netIdentity.connectionToClient;
            NetworkServer.Destroy(characterHealth.gameObject);
            SpawnCharacter(conn);
        }

        private void SpawnCharacter(NetworkConnectionToClient conn)
        {
            Transform spawnPoint = GetRandomSpawnPoint();
            var character = _characterFactory.CreateCharacter(spawnPoint, conn);
            var characterHealth = character.GetCharacterHealthComponent();
            characterHealth.Died += RespawnCharacter;
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
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
