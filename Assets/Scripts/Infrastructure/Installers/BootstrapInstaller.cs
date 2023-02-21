using Events.Game;
using JoyWay.Infrastructure.Factories;
using JoyWay.Resources;
using JoyWay.Services;
using JoyWay.UI;
using MessagePipe;
using UnityEngine;
using Zenject;

namespace JoyWay.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller<BootstrapInstaller>
    {
        [SerializeField] private CameraService _cameraService;
        [SerializeField] private AdvancedNetworkManager _networkManagerPrefab;
        
        public override void InstallBindings()
        {
            InstallServices();
            InstallFactories();
            InstallMessagePipe();

            Container.Bind<AdvancedNetworkManager>()
                .FromComponentInNewPrefab(_networkManagerPrefab)
                .AsSingle();

            Container.Bind<IInitializable>()
                .To<GameStartup>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallMessagePipe()
        {
            var options = Container.BindMessagePipe( /* configure option */);
            Container.BindMessageBroker<CharacterSpawnedEvent>(options);
        }

        private void InstallServices()
        {
            Container.Bind<AssetContainer>()
                .FromNew()
                .AsSingle();
            
            Container.Bind<PlayerInputs>()
                .FromNew()
                .AsSingle();
            
            Container.Bind<SceneLoader>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();

            Container.Bind<CameraService>()
                .FromComponentInNewPrefab(_cameraService)
                .AsSingle()
                .NonLazy();
        }

        private void InstallFactories()
        {
            Container.Bind<UIFactory>()
                .FromNew()
                .AsSingle();
            
            Container.Bind<CharacterFactory>()
                .FromNew()
                .AsSingle();

            Container.Bind<ProjectileFactory>()
                .FromNew()
                .AsSingle();
        }
    }
}