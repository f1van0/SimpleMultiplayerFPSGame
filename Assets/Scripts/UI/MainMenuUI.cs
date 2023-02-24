using TMPro;
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

        [SerializeField] private TMP_InputField _addressInputField;
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _connectButton;

        private void Awake()
        {
            ConnectButtonClicked = _connectButton.onClick;
            HostButtonClicked = _hostButton.onClick;
        }

        public string GetAddress()
        {
            return _addressInputField.text;
        }
    }
}