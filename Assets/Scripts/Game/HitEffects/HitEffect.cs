using JoyWay.Game.Character.Components;
using UnityEngine;

namespace JoyWay.Game.HitEffects
{
    [System.Serializable]
    public abstract class HitEffect : ScriptableObject
    {
        public abstract void ApplyEffect(CharacterHealthRealtimeComponent characterHealthComponent);
    }
}