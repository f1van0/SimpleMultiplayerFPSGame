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
                .AsSingle()
                .NonLazy();

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
                .AsSingle()
                .NonLazy();
            
            Container.Bind<InputService>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();
            
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
                .AsSingle()
                .NonLazy();
            
            Container.Bind<CharacterFactory>()
                .FromNew()
                .AsSingle()
                .NonLazy();

            Container.Bind<ProjectileFactory>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}