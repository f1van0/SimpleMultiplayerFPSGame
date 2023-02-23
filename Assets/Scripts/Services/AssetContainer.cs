using JoyWay.Game.Character;
using JoyWay.Game.Projectiles;
using JoyWay.Resources;
using JoyWay.UI;
using UnityEngine;

namespace JoyWay.Services
{
    public class AssetContainer
    {
        public readonly LazyResource<CharacterContainer> Character =
            new LazyResource<CharacterContainer>(Constants.Character);

        public readonly LazyResource<Projectile> Fireball =
            new LazyResource<Projectile>(Constants.Fireball);

        public readonly LazyResource<MainMenuUI> MainMenu =
            new LazyResource<MainMenuUI>(Constants.MainMenu);

        public readonly LazyResource<HideableUI> CrosshairUI =
            new LazyResource<HideableUI>(Constants.CrosshairUI);

        public readonly LazyResource<CharacterConfig> CharacterConfig =
            new LazyResource<CharacterConfig>(Constants.CharacterConfig);
    }
}