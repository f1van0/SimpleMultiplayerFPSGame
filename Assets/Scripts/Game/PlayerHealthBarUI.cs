using TMPro;
using UnityEngine;

namespace Game
{
    public class PlayerHealthBarUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthLabel;

        private int _maxHealth;
        
        public void Initialize(Character _character)
        {
            
        }

        private void SetHealth(int health)
        {
            _healthLabel.text = $"{health}/_maxHealth";
        }
    }
}