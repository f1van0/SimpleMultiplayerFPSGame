using JoyWay.Infrastructure;
using JoyWay.Services;
using JoyWay.UI;
using UnityEngine;

public class UIFactory
{
    private AssetContainer _assetContainer;
    private AdvancedNetworkManager _networkManager;

    public UIFactory(AssetContainer assetContainer, AdvancedNetworkManager networkManager)
    {
        _assetContainer = assetContainer;
        _networkManager = networkManager;
    }
    
    public MainMenuUI CreateMainMenuUI()
    {
        MainMenuUI mainMenuUI = Object.Instantiate(_assetContainer.MainMenuUI.Value);
        mainMenuUI.Initialize(_networkManager);
        return mainMenuUI;
    }

    public CrosshairUI CreateCrosshairUI()
    {
        CrosshairUI crosshairUI = Object.Instantiate(_assetContainer.CrosshairUI.Value);
        crosshairUI.Initialize(_networkManager);
        return crosshairUI;
    }
}