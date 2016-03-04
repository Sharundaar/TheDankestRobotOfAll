using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Network;

class NetworkLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        base.OnLobbyServerSceneLoadedForPlayer(manager, lobbyPlayer, gamePlayer);

        var lbp = lobbyPlayer.GetComponent<NetworkPlayer>();
        var gp = gamePlayer.GetComponent<NetworkPlayerController>();

        gp.Type = lbp.Type;
    }
}
