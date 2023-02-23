using System;
using JoyWay.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JoyWay.UI
{
    public class MainMenuUI : HideableUI
    {
        [HideInInspector]
        public Button.ButtonClickedEvent ConnectButtonClicked;

        [HideInInspector]
        public Button.ButtonClickedEvent HostButtonClicked;
        
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _connectButton;

        public void Initialize()
        {
            ConnectButtonClicked = _connectButton.onClick;
            HostButtonClicked = _hostButton.onClick;
        }
    }
}