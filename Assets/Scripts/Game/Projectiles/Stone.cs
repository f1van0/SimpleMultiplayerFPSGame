using Mirror;
using UnityEngine;
using UnityEngine.Animations;

namespace JoyWay.Game.Projectiles
{
    public class Stone : Projectile
    {
        [SerializeField] private ParentConstraint _parentConstraint;

        private bool _isInHand = false;

        public bool CanPick => !_isInHand;
        
        [Server]
        public void Pickup(Transform hand)
        {
            if (_isInHand)
                return;

            PutInHand(hand);
        }

        [Server]
        public override void Throw(Vector3 direction)
        {
            if (!_isInHand)
                return;
            
            ReleaseFromHand(); 
            base.Throw(direction);
        }

        private void PutInHand(Transform hand)
        {
            _rigidbody.isKinematic = true;
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
            _handleCollision = true;
            _isInHand = false;
            if (_parentConstraint.sourceCount > 0)
            {
                _parentConstraint.RemoveSource(0);
            }
            _parentConstraint.constraintActive = false;
        }
    }
}