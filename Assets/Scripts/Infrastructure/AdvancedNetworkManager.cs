using System;
using JoyWay.Game;
using JoyWay.Game.Character;
using JoyWay.Infrastructure.Factories;
using Mirror;
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

        [Inject]
        public void Construct(CharacterFactory characterFactory)
        {
            _characterFactory = characterFactory;
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

        private void RespawnCharacterOnServer(NetworkCharacterHealthComponent networkCharacterHealthComponent)
        {
            networkCharacterHealthComponent.Died -= RespawnCharacterOnServer;
            var conn = networkCharacterHealthComponent.netIdentity.connectionToClient;
            NetworkServer.Destroy(networkCharacterHealthComponent.gameObject);
            SpawnCharacterOnServer(conn);
        }

        private void SpawnCharacterOnServer(NetworkConnectionToClient conn)
        {
            Transform spawnPoint = GetRandomSpawnPoint();
            var character = _characterFactory.SpawnCharacterOnServer(spawnPoint, conn);
            var characterHealth = character.HealthComponent;
            characterHealth.Died += RespawnCharacterOnServer;
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);
            SpawnCharacterOnServer(conn);
        }

        private Transform GetRandomSpawnPoint()
        {
            if (_levelSpawnPoints == null)
                _levelSpawnPoints = FindObjectOfType<LevelSpawnPoints>();

            return _levelSpawnPoints.GetRandomSpawnPoint();
        }
    }
}
