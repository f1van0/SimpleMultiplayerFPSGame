using Mirror;
using Normal.Realtime;
using UnityEngine;
using UnityEngine.Animations;

namespace JoyWay.Game.Projectiles
{
    public class PickableProjectile : Projectile
    {
        [SerializeField] private ParentConstraint _parentConstraint;

        private bool _isInHand = false;

        public bool CanPick => !_isInHand;
        
        public void Pickup(Transform hand)
        {
            _realtimeView.RequestOwnership();
            _realtimeView.preventOwnershipTakeover = true;
            _realtimeTransform.RequestOwnership();
            PutInHand(hand);
        }

        public override void Throw(Vector3 direction)
        {
            ReleaseFromHand(); 
            base.Throw(direction);
        }

        private void PutInHand(Transform hand)
        {
            _rigidbody.isKinematic = true;
            _collider.isTrigger = false;
            _isInHand = true;
            ConstraintSource newConstraintSource = new ConstraintSource();
            newConstraintSource.sourceTransform = hand;
            newConstraintSource.weight = 1;
            _parentConstraint.AddSource(newConstraintSource);
            _parentConstraint.constraintActive = true;
        }

        private void ReleaseFromHand()
        {
            _rigidbody.isKinematic = false;
            _collider.isTrigger = true;
            _isInHand = false;
            if (_parentConstraint.sourceCount > 0)
            {
                _parentConstraint.RemoveSource(0);
            }
            _parentConstraint.constraintActive = false;
        }
    }
}