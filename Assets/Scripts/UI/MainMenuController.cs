using JoyWay.Infrastructure;
using UnityEngine;

namespace JoyWay.UI
{
    public class MainMenuController
    {
        private readonly MainMenuUI _mainMenuUI;

        public MainMenuController(MainMenuUI mainMenuUI, AdvancedNetworkManager networkManager)
        {
            _mainMenuUI = mainMenuUI;

            _mainMenuUI.HostButtonClicked.AddListener(networkManager.StartHost);
            _mainMenuUI.ConnectButtonClicked.AddListener(networkManager.StartClient);
        }

        public void Show()
        {
            _mainMenuUI.Show();
        }

        public void Hide()
        {
            _mainMenuUI.Hide();
        }
    }
}