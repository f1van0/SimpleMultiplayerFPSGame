using Events.Game;
using JoyWay.Infrastructure.Factories;
using JoyWay.Resources;
using JoyWay.Services;
using MessagePipe;
using UnityEngine;
using Zenject;

namespace JoyWay.Infrastructure
{
    public class GameStartup : IInitializable
    {
        private AdvancedNetworkManager _networkManager;
        private SceneLoader _sceneLoader;
        private AssetContainer _assetContainer;
        private UIFactory _uiFactory;
        private PlayerInputs _playerInputs;

        public GameStartup(
            AdvancedNetworkManager networkManager,
            UIFactory uiFactory,
            SceneLoader sceneLoader,
            PlayerInputs playerInputs)
        {
            _uiFactory = uiFactory;
            _networkManager = networkManager;
            _sceneLoader = sceneLoader;
            _playerInputs = playerInputs;
        }

        public void Initialize()
        {
            _sceneLoader.Load(Constants.GameScene, SceneLoaded);
        }

        private void SceneLoaded()
        {
            _playerInputs.Enable();
            _uiFactory.CreateMainMenuUI();
            _uiFactory.CreateCrosshairUI();
        }
    }
}