using System.Collections;
using UnityEngine;

namespace JoyWay.Game.Character.Components
{
    public class CharacterDamageDisplayComponent : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private Material _individualMaterial;
        private float _displayDamageTakenDelay = 0.2f;

        public void Setup(float displayDamageTakenDelay)
        {
            _displayDamageTakenDelay = displayDamageTakenDelay;
        }
        
        public void Initialize()
        {
            _individualMaterial = _meshRenderer.material;
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