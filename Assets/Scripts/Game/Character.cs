using Mirror;
using UnityEngine;

namespace Game
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private int Health;
        
        [SerializeField] private CharacterMovementController _movementController;
        [SerializeField] private CharacterShootingController _shootingController;

        [SerializeField] private PlayerHealthBarUI _playerHealthBarUI;

        private PlayerInputs _playerInputs;

        public void Initialize(PlayerInputs playerInputs)
        {
            _playerInputs = playerInputs;
            
        }
    }
}