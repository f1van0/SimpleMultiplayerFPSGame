using System;
using JoyWay.Infrastructure;
using Mirror;
using UnityEngine;

namespace JoyWay.Game.Character
{
    public class CharacterHealth : NetworkBehaviour
    {
        public Action<int> HealthChanged;
        public Action<CharacterHealth> Died;

        [field: SerializeField] public int MaxHealth { get; private set; }
        
        [SyncVar(hook = nameof(SetHealth))]
        private int _health;

        public void Initialize()
        {
            if (!isServer)
                return;
            
            _health = MaxHealth;
            HealthChanged += x => CheckForDeath();
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
            HealthChanged?.Invoke(_health);
        }
    }
}