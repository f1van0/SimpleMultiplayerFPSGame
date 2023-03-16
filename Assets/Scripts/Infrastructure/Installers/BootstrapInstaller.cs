using JoyWay.Infrastructure.Factories;
using JoyWay.Services;
using UnityEngine;
using Zenject;

namespace JoyWay.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller<BootstrapInstaller>
    {
        [SerializeField] private CameraService _cameraService;
        [SerializeField] private RealtimeNetworkManager _networkManagerPrefab;
        
        public override void InstallBindings()
        {
            InstallServices();
            InstallFactories();

            Container.Bind<RealtimeNetworkManager>()
                .FromComponentInNewPrefab(_networkManagerPrefab)
                .AsSingle()
                .NonLazy();

            Container.Bind<IInitializable>()
                .To<GameStartup>()
                .AsSingle()
                .NonLazy();

            Container.Bind<GameFlow>()
                .FromNewComponentOnNewGameObject()
                .AsSingle()
                .NonLazy();
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