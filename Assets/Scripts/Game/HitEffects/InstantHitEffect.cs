using JoyWay.Game.Character.Components;
using UnityEngine;

namespace JoyWay.Game.HitEffects
{
    [CreateAssetMenu(menuName = "HitEffects/InstantHit", fileName = "InstantHit", order = 0)]
    public class InstantHitEffect : HitEffect
    {
        [SerializeField] private int _damage;
        
        public override void ApplyEffect(NetworkCharacterHealthComponent characterHealthComponent)
        {
            characterHealthComponent.ApplyDamage(_damage);
        }
    }
}