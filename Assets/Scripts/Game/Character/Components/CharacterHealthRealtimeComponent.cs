using System;
using Mirror;
using Normal.Realtime;
using UnityEngine;

namespace JoyWay.Game.Character.Components
{
    public class CharacterHealthRealtimeComponent : RealtimeComponent<CharacterHealthModel>
    {
        public event Action<int, int> HealthChanged;
        public event Action<CharacterHealthRealtimeComponent> Died;

        public int MaxHealth => model.maxHealth;
        public int Health => model.health;

        public void Setup(int maxHealth)
        {
            model.maxHealth = maxHealth;
            model.health = maxHealth;
        }
        
        public void Initialize()
        {
            HealthChanged += (_, __) => CheckForDeath();
        }
        
        public void ApplyDamage(int damage)
        {
            model.health -= damage;
        }

        public void CheckForDeath()
        {
            if (model.health < 0)
            {
                Died?.Invoke(this);
            }
        }

        protected override void OnRealtimeModelReplaced(CharacterHealthModel previousModel, CharacterHealthModel currentModel)
        {
            if (previousModel != null)
            {
                currentModel.healthDidChange -= HandleHealthDidChange;
            }

            currentModel.healthDidChange += HandleHealthDidChange;
        }

        public void HandleHealthDidChange(CharacterHealthModel characterHealthModel, int value)
        {
            HealthChanged?.Invoke(model.health, model.maxHealth);
            CheckForDeath();
        }
    }
}