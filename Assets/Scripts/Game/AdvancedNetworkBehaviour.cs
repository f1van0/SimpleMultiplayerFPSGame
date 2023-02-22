using System;
using Mirror;

namespace JoyWay.Game
{
    public class AdvancedNetworkBehaviour : NetworkBehaviour
    {
        //There is a flaw in the Mirror network library which is that when a network object is removed,
        //isOwned becomes false for the owner
        //
        //The solution is to cache the isOwned value, as Owner does not change and this value will remain until destroy
        protected private bool _isOwnedCached;
    }
}