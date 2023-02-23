using UnityEngine;
using UnityEngine.UI;

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

        private void Awake()
        {
            ConnectButtonClicked = _connectButton.onClick;
            HostButtonClicked = _hostButton.onClick;
        }
    }
}