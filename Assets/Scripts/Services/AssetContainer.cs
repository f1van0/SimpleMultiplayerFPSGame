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

        public readonly LazyResource<MainMenuUI> MainMenuUI =
            new LazyResource<MainMenuUI>(Constants.MainMenuUI);

        public readonly LazyResource<CrosshairUI> CrosshairUI =
            new LazyResource<CrosshairUI>(Constants.CrosshairUI);
    }
}