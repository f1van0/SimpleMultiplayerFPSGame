using System;
using System.Collections;
using System.Collections.Generic;
using JoyWay.Game;
using JoyWay.Game.Character;
using JoyWay.Game.Character.Components;
using JoyWay.Infrastructure.Factories;
using Normal.Realtime;
using UnityEngine;
using Zenject;

namespace JoyWay
{
    public class RealtimeNetworkManager : MonoBehaviour
    {
        public event Action Connected;
        public event Action Disconnected;
        
        [SerializeField] private Realtime _realtime;

        public Realtime Realtime { get; private set; }

        private CharacterFactory _characterFactory;
        private LevelSpawnPoints _levelSpawnPoints;

        [Inject]
        public void Contruct(CharacterFactory characterFactory)
        {
            _characterFactory = characterFactory;

            _realtime.didConnectToRoom += ConnectedToRoom;
            _realtime.didDisconnectFromRoom += DisconnectedFromRoom;
        }

        public void JoinRoom(string roomName)
        {
            _realtime.Connect(roomName);
        }

        public void Disconnect()
        {
            if (_realtime.connected) 
                _realtime.Disconnect();
        }

        public void ConnectedToRoom(Realtime realtime)
        {
            Connected?.Invoke();
            SpawnCharacter();
        }

        public void DisconnectedFromRoom(Realtime realtime)
        {
            Disconnected?.Invoke();
        }

        private void SpawnCharacter()
        {
            Transform spawnPoint = GetRandomSpawnPoint();
            var character = _characterFactory.CreateCharacter(spawnPoint, _realtime);
            var characterHealth = character.HealthComponent;
            characterHealth.Died += RespawnCharacter;
        }

        private void RespawnCharacter(CharacterHealthRealtimeComponent healthComponent)
        {
            healthComponent.Died -= RespawnCharacter;
            Realtime.Destroy(healthComponent.gameObject);
            SpawnCharacter();
        }

        private Transform GetRandomSpawnPoint()
        {
            if (_levelSpawnPoints == null)
                _levelSpawnPoints = FindObjectOfType<LevelSpawnPoints>();

            return _levelSpawnPoints.GetRandomSpawnPoint();
        }
    }
}
