using System;
using System.Collections;
using JoyWay.Game.Character;
using UnityEngine;

namespace JoyWay.Game.Projectiles
{
    public class PeriodicalDamageComponent : MonoBehaviour
    {
        private int _periodicDamage;
        private int _numberOfTimes;
        private float _applyingEffectDelay;
        private Coroutine _coroutine;
        private NetworkCharacterHealthComponent _characterHealthComponent;

        public void StopEffect()
        {
            StopCoroutine(_coroutine);
        }

        public void Apply(NetworkCharacterHealthComponent networkCharacterHealthComponent, int periodicDamage, int numberOfTimes, float applingEffectDelay)
        {
            _characterHealthComponent = networkCharacterHealthComponent;
            _applyingEffectDelay = applingEffectDelay;
            _numberOfTimes = numberOfTimes;
            _periodicDamage = periodicDamage;
            _coroutine = StartCoroutine(ApplyPeriodicEffect());
        }
        
        public IEnumerator ApplyPeriodicEffect()
        {
            for (int i = 0; i < _numberOfTimes; i++)
            {
                yield return new WaitForSeconds(_applyingEffectDelay);
                _characterHealthComponent.ApplyDamage(_periodicDamage);
            }
            
            Destroy(this);
        }

        private void OnDestroy()
        {
            StopEffect();
        }
    }
}