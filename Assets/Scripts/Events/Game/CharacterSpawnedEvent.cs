using JoyWay.Game.Character;

namespace Events.Game
{
    public class CharacterSpawnedEvent
    {
        public CharacterContainer CharacterContainer;

        public CharacterSpawnedEvent(CharacterContainer characterContainer)
        {
            CharacterContainer = characterContainer;
        }
    }
}