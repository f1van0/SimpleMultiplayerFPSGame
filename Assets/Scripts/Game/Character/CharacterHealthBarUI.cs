using TMPro;
using UnityEngine;

namespace JoyWay.Game.Character
{
    public class CharacterHealthBarUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthLabel;

        private int _maxHealth;
        
        public void Initialize(CharacterHealth characterHealth)
        {
            _maxHealth = characterHealth.MaxHealth;
            SetHealth(_maxHealth);
            characterHealth.HealthChanged += SetHealth;
        }

        private void SetHealth(int health)
        {
            _healthLabel.text = $"{health}/{_maxHealth}";
        }
    }
}