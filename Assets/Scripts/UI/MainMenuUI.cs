using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JoyWay.UI
{
    public class MainMenuUI : HideableUI
    {
        [HideInInspector]
        public Button.ButtonClickedEvent JoinButtonClicked;

        [SerializeField] private TMP_InputField _addressInputField;
        [SerializeField] private Button _joinButton;

        private void Awake()
        {
            JoinButtonClicked = _joinButton.onClick;
        }

        public string GetRoomName()
        {
            return _addressInputField.text;
        }
    }
}