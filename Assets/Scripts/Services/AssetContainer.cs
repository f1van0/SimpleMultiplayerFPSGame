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
            new LazyResource<CharacterContainer>(ResourcesPath.Character);

        public readonly LazyResource<Projectile> Fireball =
            new LazyResource<Projectile>(ResourcesPath.Fireball);

        public readonly LazyResource<MainMenuUI> MainMenuUI =
            new LazyResource<MainMenuUI>(ResourcesPath.MainMenuUI);

        public readonly LazyResource<HideableUI> CrosshairUI =
            new LazyResource<HideableUI>(ResourcesPath.CrosshairUI);

        public readonly LazyResource<CharacterConfig> CharacterConfig =
            new LazyResource<CharacterConfig>(ResourcesPath.CharacterConfig);
    }
}