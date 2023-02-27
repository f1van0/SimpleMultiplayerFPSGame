using UnityEngine;

namespace JoyWay.Game.Character.Components
{
    public class CharacterRotationComponent : MonoBehaviour
    {
        [SerializeField] private Transform _shouldersHeightTransform;
        private Vector3 _flatLookDirection;
        
        public void ChangeLookDirection(Vector3 lookDirection)
        {
            Vector3 flatLookDirection = new Vector3(lookDirection.x, 0, lookDirection.z);
            transform.forward = flatLookDirection;
            _shouldersHeightTransform.forward = lookDirection;
        }

        public Transform GetBodyTransform()
        {
            return transform;
        }
    }
}