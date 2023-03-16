using JoyWay;
using JoyWay.Infrastructure;
using JoyWay.Services;
using JoyWay.UI;
using UnityEngine;

public class UIFactory
{
    private readonly AssetContainer _assetContainer;
    private readonly RealtimeNetworkManager _networkManager;

    public UIFactory(AssetContainer assetContainer, RealtimeNetworkManager networkManager)
    {
        _assetContainer = assetContainer;
        _networkManager = networkManager;
    }
    
    public MainMenuController CreateMainMenu()
    {
        MainMenuUI mainMenu = Object.Instantiate(_assetContainer.MainMenuUI.Value);
        return new MainMenuController(mainMenu, _networkManager);
    }

    public HideableUI CreateCrosshairUI()
    {
        HideableUI crosshairUI = Object.Instantiate(_assetContainer.CrosshairUI.Value);
        return crosshairUI;
    }
}