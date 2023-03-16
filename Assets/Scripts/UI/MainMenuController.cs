using System;
using System.Net;
using JoyWay.Infrastructure;
using UnityEngine;

namespace JoyWay.UI
{
    public class MainMenuController
    {
        private readonly MainMenuUI _mainMenuUI;
        private RealtimeNetworkManager _networkManager;

        public MainMenuController(MainMenuUI mainMenuUI, RealtimeNetworkManager networkManager)
        {
            _networkManager = networkManager;
            _mainMenuUI = mainMenuUI;

            _mainMenuUI.JoinButtonClicked.AddListener(Join);
        }

        private void Join()
        {
            _networkManager.JoinRoom(_mainMenuUI.GetRoomName());
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