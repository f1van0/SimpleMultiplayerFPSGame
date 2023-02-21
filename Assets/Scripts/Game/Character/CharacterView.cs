using System.Collections;
using JoyWay.Services;
using UnityEngine;

namespace JoyWay.Game.Character
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private CharacterHealthBarUI _healthBarUI;
        [SerializeField] private Transform _shouldersHeightTransform;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private float _displayDamageTakenDelay;

        private CharacterLookController _lookController;
        
        private Material _individualMaterial;

        public void Initialize(CharacterHealth characterHealth, CharacterLookController lookController)
        {
            _lookController = lookController;
            _healthBarUI.Initialize(characterHealth);
            
            _individualMaterial = _meshRenderer.material;
            characterHealth.HealthChanged += DisplayDamageTaken;
            _lookController.LookDirectionChanged += ChangeCharacterLookDirection;
        }

        private void ChangeCharacterLookDirection(Vector3 lookDirection)
        {
            Vector3 flatLookDirection = new Vector3(lookDirection.x, 0, lookDirection.z);
            transform.forward = flatLookDirection;
            _shouldersHeightTransform.forward = lookDirection;
        }

        public void DisplayDamageTaken(int health)
        {
            StartCoroutine(CharacterReddening());
        }

        private IEnumerator CharacterReddening()
        {
            _individualMaterial.color = Color.red;
            yield return new WaitForSeconds(_displayDamageTakenDelay);
            _individualMaterial.color = Color.white;
        }

        private void OnDestroy()
        {
            _lookController.LookDirectionChanged -= ChangeCharacterLookDirection;
        }
    }
}