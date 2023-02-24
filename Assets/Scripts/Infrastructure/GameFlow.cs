using JoyWay.UI;
using UnityEngine;
using Zenject;

namespace JoyWay.Infrastructure
{
    public class GameFlow : MonoBehaviour
    {
        private AdvancedNetworkManager _networkManager;
        private UIFactory _uiFactory;

        private MainMenuController _mainMenu;

        private HideableUI _crosshairUI;

        [Inject]
        public void Construct(AdvancedNetworkManager networkManager, UIFactory uiFactory)
        {
            _networkManager = networkManager;
            _uiFactory = uiFactory;
        }

        public void StartGame()
        {
            _mainMenu = _uiFactory.CreateMainMenu();
            _crosshairUI = _uiFactory.CreateCrosshairUI();
            _networkManager.Connected += GoToGame;
            _networkManager.Disconnected += GoToMenu;
        }

        private void GoToMenu()
        {
            _mainMenu.Show();
            _crosshairUI.Hide();
        }

        private void GoToGame()
        {
            _mainMenu.Hide();
            _crosshairUI.Show();
        }

        private void OnDestroy()
        {
            _networkManager.Connected -= _mainMenu.Hide;
            _networkManager.Disconnected -= _mainMenu.Show;
            _networkManager.Connected -= _crosshairUI.Show;
            _networkManager.Disconnected -= _crosshairUI.Hide;
        }
    }
}