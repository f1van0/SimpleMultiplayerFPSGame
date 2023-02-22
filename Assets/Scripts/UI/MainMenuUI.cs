using System;
using JoyWay.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JoyWay.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _connectButton;

        public void Initialize(AdvancedNetworkManager networkManager)
        {
            networkManager.Connected += Hide;
            networkManager.Disconnected += Show;
            _connectButton.onClick.AddListener(networkManager.StartClient);
            _hostButton.onClick.AddListener(networkManager.StartHost);
        }

        public void Show()
        {
            _panel.SetActive(true);
        }

        public void Hide()
        {
            _panel.SetActive(false);
        }
    }
}