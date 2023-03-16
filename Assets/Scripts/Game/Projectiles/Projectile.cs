using JoyWay.Game.Character.Components;
using JoyWay.Game.HitEffects;
using Mirror;
using Normal.Realtime;
using UnityEngine;

namespace JoyWay.Game.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected RealtimeView _realtimeView;
        [SerializeField] protected RealtimeTransform _realtimeTransform;
        
        [SerializeField] protected Rigidbody _rigidbody;
        [SerializeField] protected Collider _collider;

        [SerializeField] private HitEffect _hitEffect;
        [SerializeField] private float _force;

        public virtual void Throw(Vector3 direction)
        {
            _realtimeTransform.RequestOwnership();
            _rigidbody.AddForce(direction * _force, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<CharacterHealthRealtimeComponent>(out var characterHealth))
            {
                RealtimeView receiverRealtimeView = characterHealth.GetComponent<RealtimeView>();
                if (!receiverRealtimeView.isOwnedLocallySelf)
                    return;
                
                int receiverId = receiverRealtimeView.ownerIDSelf;
                if (receiverId == _realtimeView.ownerIDSelf)
                    return;
                
                _hitEffect.ApplyEffect(characterHealth);
            }

            Destroy(gameObject);
        }
    }
}