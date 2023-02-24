using JoyWay.Resources;
using JoyWay.Services;
using Zenject;

namespace JoyWay.Infrastructure
{
    public class GameStartup : IInitializable
    {
        private SceneLoader _sceneLoader;
        private GameFlow _gameFlow;

        public GameStartup(
            SceneLoader sceneLoader,
            GameFlow gameFlow)
        {
            _sceneLoader = sceneLoader;
            _gameFlow = gameFlow;
        }

        public void Initialize()
        {
            _sceneLoader.Load(Constants.GameScene, SceneLoaded);
        }

        private void SceneLoaded()
        {
            _gameFlow.StartGame();
        }
    }
}