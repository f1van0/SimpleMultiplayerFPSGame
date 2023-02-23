using UnityEngine;

namespace JoyWay.Game.Character
{
    [CreateAssetMenu(menuName = "Character/CharacterConfig", fileName = "CharacterConfig", order = 0)]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public int MaxHealth { get; private set; }

        [field: SerializeField] public int MaxSpeed { get; private set; }
        [field: SerializeField] public int MovementForce { get; private set; }
        [field: SerializeField] public int JumpForce { get; private set; }
        [field: SerializeField] public int GroundDrag { get; private set; }
        [field: SerializeField] public int AirDrag { get; private set; }
        
        [field: SerializeField] public float MaxInteractionDistance { get; private set; }
    }
}