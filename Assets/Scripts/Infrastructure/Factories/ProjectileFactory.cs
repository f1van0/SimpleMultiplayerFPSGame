using JoyWay.Game.Projectiles;
using JoyWay.Resources;
using JoyWay.Services;
using Normal.Realtime;
using UnityEngine;

namespace JoyWay.Infrastructure.Factories
{
    public class ProjectileFactory
    {
        private AssetContainer _assetContainer;
        private RealtimeNetworkManager _networkManager;

        public ProjectileFactory(AssetContainer assetContainer, RealtimeNetworkManager networkManager)
        {
            _assetContainer = assetContainer;
            _networkManager = networkManager;
        }
        
        public Projectile CreateFireball(Vector3 at, Vector3 direction, int sender)
        {
            var options = new Realtime.InstantiateOptions {
                ownedByClient            = true,
                preventOwnershipTakeover = false,
                useInstance              = _networkManager.Realtime
            };
            
            GameObject fireball = Realtime.Instantiate(ResourcesPath.Fireball, options);
            fireball.transform.position = at;
            Projectile projectile = fireball.GetComponent<Projectile>();
            projectile.Throw(direction);
            return projectile;
        }
    }
}