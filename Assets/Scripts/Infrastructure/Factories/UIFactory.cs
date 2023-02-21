using JoyWay.Services;
using JoyWay.UI;
using UnityEngine;

public class UIFactory
{
    private AssetContainer _assetContainer;

    public UIFactory(AssetContainer assetContainer)
    {
        _assetContainer = assetContainer;
    }
    
    public MainMenuUI CreateMainMenuUI()
    {
        MainMenuUI mainMenuUI = Object.Instantiate(_assetContainer.MainMenuUI.Value);
        mainMenuUI.Initialize();
        return mainMenuUI;
    }

    public GameObject CreateCrosshairUI()
    {
        GameObject crosshairUI = Object.Instantiate(_assetContainer.CrosshairUI.Value);
        return crosshairUI;
    }
}