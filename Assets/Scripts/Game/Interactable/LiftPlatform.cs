using Normal.Realtime;
using UnityEngine;

namespace JoyWay.Game.Interactable
{
    public class LiftPlatform : RealtimeComponent<LiftModel>, IInteractable
    {
        [SerializeField] private RealtimeTransform _platformRealtimeTransform;
        [SerializeField] private RealtimeView _realtimeView;
        [SerializeField] private Rigidbody _platformRigidBody;
        [SerializeField] private Transform _bottom;
        [SerializeField] private Transform _top;
        
        private float _timeInterval = 1;
        private float _timer;

        private Vector3 _startPosition;
        private Vector3 _endPosition;
        
        protected override void OnRealtimeModelReplaced(LiftModel previousModel, LiftModel currentModel)
        {
            if (model.isFreshModel)
            {
                model.isLifting = false;
                model.isRaised = false;
            }

            model.isLifting = currentModel.isLifting;
            model.isRaised = currentModel.isRaised;
        }

        public void Interact()
        {
            if (!model.isLifting)
            {
                _realtimeView.RequestOwnership();
                _platformRealtimeTransform.RequestOwnership();

                if (model.isRaised)
                {
                    _startPosition = _top.position;
                    _endPosition = _bottom.position;
                }
                else
                {
                    _startPosition = _bottom.position;
                    _endPosition = _top.position;
                }

                _timer = 0;
                model.isRaised = !model.isRaised;
                model.isLifting = true;
            }
        }

        private void FixedUpdate()
        {
            Lift();
        }

        private void Lift()
        {
            if (model == null || !model.isLifting)
                return;
            
            var delta = Vector3.Lerp(_startPosition, _endPosition, _timer / _timeInterval);
            _platformRigidBody.MovePosition(delta);
            
            _timer += Time.fixedDeltaTime;

            if (_timer >= _timeInterval)
                model.isLifting = false;
        }
    }
}