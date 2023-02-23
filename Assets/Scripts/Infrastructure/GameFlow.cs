using System;
using JoyWay.Infrastructure;
using JoyWay.UI;
using UnityEngine;
using Zenject;

public class GameFlow : MonoBehaviour
{
    private AdvancedNetworkManager _networkManager;
    private UIFactory _uiFactory;

    private MainMenuUI _mainMenuUI;
    private HideableUI _crosshairUI;

    [Inject]
    public void Construct(AdvancedNetworkManager networkManager, UIFactory uiFactory)
    {
        _networkManager = networkManager;
        _uiFactory = uiFactory;
    }

    public void StartGame()
    {
        _mainMenuUI = _uiFactory.CreateMainMenuUI();
        _crosshairUI = _uiFactory.CreateCrosshairUI();
        _networkManager.Connected += _mainMenuUI.Hide;
        _networkManager.Disconnected += _mainMenuUI.Show;
        _networkManager.Connected += _crosshairUI.Show;
        _networkManager.Disconnected += _crosshairUI.Hide;
        _mainMenuUI.HostButtonClicked.AddListener(_networkManager.StartHost);
        _mainMenuUI.ConnectButtonClicked.AddListener(_networkManager.StartClient);
    }

    private void OnDestroy()
    {
        _networkManager.Connected -= _mainMenuUI.Hide;
        _networkManager.Disconnected -= _mainMenuUI.Show;
        _networkManager.Connected -= _crosshairUI.Show;
        _networkManager.Disconnected -= _crosshairUI.Hide;
        _mainMenuUI.HostButtonClicked.RemoveAllListeners();
        _mainMenuUI.ConnectButtonClicked.RemoveAllListeners();
    }
}