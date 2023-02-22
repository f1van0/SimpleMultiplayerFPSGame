using JoyWay.Infrastructure;
using Mirror;
using UnityEngine;

namespace JoyWay.UI
{
    public class CrosshairUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _crosshair;
        
        public void Initialize(AdvancedNetworkManager networkManager)
        {
            networkManager.Connected += Show;
            networkManager.Disconnected += Hide;
        }

        private void Show()
        {
            _crosshair.SetActive(true);
        }

        private void Hide()
        {
            _crosshair.SetActive(false);
        }
    }
}