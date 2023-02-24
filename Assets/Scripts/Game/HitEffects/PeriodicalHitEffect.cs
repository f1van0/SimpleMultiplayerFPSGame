using JoyWay.Game.Character.Components;
using JoyWay.Game.Projectiles;
using UnityEngine;

namespace JoyWay.Game.HitEffects
{
    [CreateAssetMenu(menuName = "HitEffects/PeriodicalHit", fileName = "PeriodicalHit", order = 0)]
    public class PeriodicalHitEffect : InstantHitEffect
    {
        [SerializeField] private float _applingEffectDelay;
        [SerializeField] private int _numberOfTimes;
        [SerializeField] private int _periodicDamage;
        
        public override void ApplyEffect(NetworkCharacterHealthComponent characterHealthComponent)
        {
            base.ApplyEffect(characterHealthComponent);
            
            if (characterHealthComponent.gameObject.TryGetComponent<PeriodicalDamageComponent>(out var periodicalDamage))
            {
                periodicalDamage.StopEffect();
            }
            else
            {
                periodicalDamage = characterHealthComponent.gameObject.AddComponent<PeriodicalDamageComponent>();
            }

            periodicalDamage.Apply(characterHealthComponent, _periodicDamage, _numberOfTimes, _applingEffectDelay);
        }

    }
}