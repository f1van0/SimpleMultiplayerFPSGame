using Game;
using UnityEngine;

namespace DefaultNamespace
{
    public class AssetContainer
    {
        public static readonly LazyResource<Character> Character =
            new LazyResource<Character>(Constants.Character);

        public static readonly LazyResource<MainMenuUI> MainMenuUI =
            new LazyResource<MainMenuUI>(Constants.MainMenuUI);
    }
}