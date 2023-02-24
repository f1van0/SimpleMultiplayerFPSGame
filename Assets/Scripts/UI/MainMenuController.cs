using System;
using System.Net;
using JoyWay.Infrastructure;
using UnityEngine;

namespace JoyWay.UI
{
    public class MainMenuController
    {
        private readonly MainMenuUI _mainMenuUI;
        private AdvancedNetworkManager _networkManager;

        public MainMenuController(MainMenuUI mainMenuUI, AdvancedNetworkManager networkManager)
        {
            _networkManager = networkManager;
            _mainMenuUI = mainMenuUI;

            _mainMenuUI.HostButtonClicked.AddListener(networkManager.StartHost);
            _mainMenuUI.ConnectButtonClicked.AddListener(Connect);
            
        }

        private void Connect()
        {
            _networkManager.Connect(GetAddress());
        }

        public void Show()
        {
            _mainMenuUI.Show();
        }

        public void Hide()
        {
            _mainMenuUI.Hide();
        }

        public IPAddress GetAddress()
        {
            var ipString = _mainMenuUI.GetAddress();
            
            if (IPAddress.TryParse(ipString, out var ipAddress))
            {
                return ipAddress;
            }
            else
            {
                return IPAddress.Loopback;
            }
        }
    }
}