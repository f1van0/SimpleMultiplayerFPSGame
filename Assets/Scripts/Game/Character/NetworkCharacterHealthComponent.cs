using System;
using JoyWay.Infrastructure;
using Mirror;
using UnityEngine;

namespace JoyWay.Game.Character
{
    public class NetworkCharacterHealthComponent : NetworkBehaviour
    {
        public event Action<int, int> HealthChanged;
        public event Action<NetworkCharacterHealthComponent> Died;

        [SyncVar]
        private int _maxHealth;
        
        [SyncVar(hook = nameof(SetHealth))]
        private int _health;

        public int MaxHealth => _maxHealth;
        public int Health => _health;

        public void Setup(int maxHealth)
        {
            _maxHealth = maxHealth;
            _health = maxHealth;
        }
        
        public void Initialize()
        {
            HealthChanged += (_, __) => CheckForDeath();
        }
        
        [Server]
        public void ApplyDamage(int damage)
        {
            _health -= damage;
        }

        [Server]
        public void CheckForDeath()
        {
            if (_health < 0)
            {
                Died?.Invoke(this);
            }
        }

        void SetHealth(int oldHealth, int newHealth)
        {
            HealthChanged?.Invoke(_health, _maxHealth);
        }
    }
}