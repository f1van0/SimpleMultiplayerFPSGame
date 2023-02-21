using Game;
using JoyWay.Game.Character;
using Mirror;
using UnityEngine;

namespace JoyWay.Game.Projectiles
{
    public class Projectile : NetworkBehaviour
    {
        [SerializeField] protected Rigidbody _rigidbody;
        [SerializeField] protected bool _handleCollision;
        
        [SerializeField] private HitEffect _hitEffect;
        [SerializeField] private float _force;

        [Server]
        public virtual void Throw(Vector3 direction)
        {
            _rigidbody.AddForce(direction * _force, ForceMode.Impulse);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (!_handleCollision || !isServer)
                return;
            
            if (other.gameObject.TryGetComponent<CharacterHealth>(out var characterHealth))
            {
                _hitEffect.ApplyEffect(characterHealth);
            }

            Destroy(gameObject);
        }
    }
}