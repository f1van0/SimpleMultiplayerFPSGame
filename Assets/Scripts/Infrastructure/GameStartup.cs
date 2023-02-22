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
        private SceneLoader _sceneLoader;
        private UIFactory _uiFactory;

        public GameStartup(
            UIFactory uiFactory,
            SceneLoader sceneLoader)
        {
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            _sceneLoader.Load(Constants.GameScene, SceneLoaded);
        }

        private void SceneLoaded()
        {
            _uiFactory.CreateMainMenuUI();
            _uiFactory.CreateCrosshairUI();
        }
    }
}