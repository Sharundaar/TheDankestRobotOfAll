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

        NetworkPlayer lbp = lobbyPlayer.GetComponent<NetworkPlayer>();
        StartPoint[] startPoints = FindObjectsOfType<StartPoint>();
        gamePlayer.transform.position = startPoints.Where((pt) => pt.Type == lbp.Type).First().transform.position;
        NetworkCommands.Instance().ChangeRendererColor(gamePlayer, lbp.Type == PlayerType.ROBOT ? Color.red : Color.green);
    }
}
