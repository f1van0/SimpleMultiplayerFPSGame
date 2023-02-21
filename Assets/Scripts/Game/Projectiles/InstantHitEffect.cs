using JoyWay.Game.Character;
using UnityEngine;

namespace JoyWay.Game.Projectiles
{
    [CreateAssetMenu(menuName = "HitEffects/InstantHit", fileName = "InstantHit", order = 0)]
    public class InstantHitEffect : HitEffect
    {
        [SerializeField] private int _damage;
        
        public override void ApplyEffect(CharacterHealth characterHealth)
        {
            characterHealth.ApplyDamage(_damage);
        }
    }
}