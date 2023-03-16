using JoyWay.Services;
using Normal.Realtime;
using UnityEngine;

namespace JoyWay.Game.Character.Components
{
    public class CharacterLookComponent : MonoBehaviour
    {
        [SerializeField] private RealtimeTransform _body;
        [SerializeField] private RealtimeTransform _shoulders;

        [SerializeField] private Transform _eyes;

        public void Initialize(CameraService cameraService)
        {
            _body.RequestOwnership();
            _shoulders.RequestOwnership();
            cameraService.SetFollowTarget(_eyes);
        }

        public void ChangeLookDirection(Vector3 lookDirection)
        {
            Vector3 flatLookDirection = new Vector3(lookDirection.x, 0, lookDirection.z);
            _body.transform.forward = flatLookDirection;
            _shoulders.transform.forward = lookDirection;
        }

        public Transform GetBodyTransform()
        {
            return _body.transform;
        }
    }
}