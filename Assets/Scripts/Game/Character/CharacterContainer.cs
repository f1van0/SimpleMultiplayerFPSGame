using JoyWay.Game.Character.Components;
using UnityEngine;

namespace JoyWay.Game.Character
{
    public class CharacterContainer : MonoBehaviour
    {
        [field: SerializeField] public NetworkCharacter NetworkCharacter { get; private set; }
        [field: SerializeField] public CharacterHealthRealtimeComponent HealthComponent { get; private set; }
        [field: SerializeField] public CharacterMovementComponent MovementComponent { get; private set; }
        [field: SerializeField] public CharacterShootingComponent ShootingComponent { get; private set; }
        [field: SerializeField] public CharacterInteractionComponent InteractionComponent { get; private set; }
        [field: SerializeField] public CharacterDamageDisplayComponent DamageDisplayComponent { get; private set; }
        [field: SerializeField] public CharacterLookComponent lookComponent { get; private set; }
        [field: SerializeField] public CharacterHealthBarUI HealthBarUI { get; private set; }
    }
}