using JoyWay.Game.Character;
using UnityEngine;

namespace JoyWay.Game.Projectiles
{
    [CreateAssetMenu(menuName = "HitEffects/PeriodicalHit", fileName = "PeriodicalHit", order = 0)]
    public class PeriodicalHitEffect : InstantHitEffect
    {
        [SerializeField] private float _applingEffectDelay;
        [SerializeField] private int _numberOfTimes;
        [SerializeField] private int _periodicDamage;
        
        public override void ApplyEffect(CharacterHealth characterHealth)
        {
            base.ApplyEffect(characterHealth);
            
            if (characterHealth.gameObject.TryGetComponent<PeriodicalDamageComponent>(out var periodicalDamage))
            {
                periodicalDamage.StopEffect();
            }
            else
            {
                periodicalDamage = characterHealth.gameObject.AddComponent<PeriodicalDamageComponent>();
            }

            periodicalDamage.Apply(characterHealth, _periodicDamage, _numberOfTimes, _applingEffectDelay);
        }

    }
}