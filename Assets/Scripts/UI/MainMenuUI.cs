using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JoyWay.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public Button.ButtonClickedEvent Connect;
        public Button.ButtonClickedEvent Host;
        
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _connectButton;

        public void Initialize()
        {
            Host = _hostButton.onClick;
            Connect = _connectButton.onClick;
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