using System;
using System.Collections;
using JoyWay.Services;
using UnityEngine;

namespace JoyWay.Game.Character
{
    public class CharacterViewComponent : MonoBehaviour
    {
        [SerializeField] private Transform _shouldersTransform;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        //TODO: can be migrated into ScriptableObject but in my case I cant setup this variables in factory on client
        [SerializeField] private float _displayDamageTakenDelay;
        [SerializeField] private float _interpolationTimeInterval;
        
        private float _timer;
        private Material _individualMaterial;

        private Vector3 _currentCharacterLookDirection;
        private Vector3 _newCharacterLookDirection;
        private Vector3 _flatLookDirection;

        public void Initialize(bool isOwner)
        {
            _individualMaterial = _meshRenderer.material;
            _newCharacterLookDirection = transform.forward;

            if (isOwner)
                _interpolationTimeInterval = 0;
        }

        public void Update()
        {
            UpdateLookDirection();
        }

        public void ChangeLookDirection(Vector3 lookDirection)
        {
            _newCharacterLookDirection = lookDirection;
            _timer = 0;
        }

        private void UpdateLookDirection()
        {
            _timer += Time.deltaTime;
            
            _currentCharacterLookDirection =
                Vector3.Lerp(_currentCharacterLookDirection, _newCharacterLookDirection, _timer / _interpolationTimeInterval);
            
            Vector3 flatLookDirection = new Vector3(_currentCharacterLookDirection.x, 0, _currentCharacterLookDirection.z);
            transform.forward = flatLookDirection;
            _shouldersTransform.forward = _currentCharacterLookDirection;
        }

        public void DisplayDamageTaken()
        {
            StartCoroutine(CharacterReddening());
        }

        private IEnumerator CharacterReddening()
        {
            _individualMaterial.color = Color.red;
            yield return new WaitForSeconds(_displayDamageTakenDelay);
            _individualMaterial.color = Color.white;
        }
    }
}