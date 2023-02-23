using JoyWay.Game.Character;

namespace Events.Game
{
    public class NetworkCharacterSpawnedEvent
    {
        public NetworkCharacter Character;

        public NetworkCharacterSpawnedEvent(NetworkCharacter character)
        {
            Character = character;
        }
    }
}