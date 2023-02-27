using JoyWay.Game.Character.Components;
using UnityEngine;

namespace JoyWay.Game.Character
{
    public class CharacterContainer : MonoBehaviour
    {
        [field: SerializeField] public NetworkCharacter NetworkCharacter { get; private set; }
        [field: SerializeField] public NetworkCharacterHealthComponent HealthComponent { get; private set; }
        [field: SerializeField] public NetworkCharacterMovementComponent MovementComponent { get; private set; }
        [field: SerializeField] public NetworkCharacterShootingComponent ShootingComponent { get; private set; }
        [field: SerializeField] public NetworkCharacterInteractionComponent InteractionComponent { get; private set; }
        [field: SerializeField] public NetworkCharacterLookComponent LookComponent { get; private set; }
        [field: SerializeField] public CharacterDamageDisplayComponent DamageDisplayComponent { get; private set; }
        [field: SerializeField] public CharacterRotationComponent RotationComponent { get; private set; }
        [field: SerializeField] public CharacterHealthBarUI HealthBarUI { get; private set; }
    }
}