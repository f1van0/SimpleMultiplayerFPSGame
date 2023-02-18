using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Mirror;
using UnityEngine;

public class AdvancedNetworkManager : NetworkManager
{
    public event Action Connected;
    public event Action Disconnected;
    
    public override void OnStartClient()
    {
        Connected?.Invoke();
    }

    public override void OnStopClient()
    {
        Disconnected?.Invoke();
    }

    public override void OnClientConnect()
    {
        //NetworkServer.Spawn(AssetContainer.Character, );
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
