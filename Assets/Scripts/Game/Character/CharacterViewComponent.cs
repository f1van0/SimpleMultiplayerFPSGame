using System;
using System.Collections;
using JoyWay.Services;
using UnityEngine;

namespace JoyWay.Game.Character
{
    public class CharacterViewComponent : MonoBehaviour
    {
        [SerializeField] private Transform _shouldersHeightTransform;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        //TODO: can be migrated into ScriptableObject but in my case I cant setup this variables in factory on client
        [SerializeField] private float _displayDamageTakenDelay;

        private Material _individualMaterial;
        private Vector3 _flatLookDirection;

        public void Initialize(bool isOwner)
        {
            _individualMaterial = _meshRenderer.material;
        }

        public void ChangeLookDirection(Vector3 lookDirection)
        {
            Vector3 flatLookDirection = new Vector3(lookDirection.x, 0, lookDirection.z);
            transform.forward = flatLookDirection;
            _shouldersHeightTransform.forward = lookDirection;
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