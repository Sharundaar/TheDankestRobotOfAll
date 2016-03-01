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
    }

    public override void OnLobbyServerSceneChanged(string _sceneName, IEnumerable<NetworkLobbyPlayer> _players)
    {
        foreach(var player in _players)
        {
            NetworkPlayer lbp = player.GetComponent<NetworkPlayer>();
            GameObject gamePlayer = GameManager.Instance().GetPlayer(lbp.Type);
            if (gamePlayer != null)
            {
                gamePlayer.GetComponent<NetworkFirstPersonController>().TakeLocalControl(lbp.Connection);
                gamePlayer.GetComponent<NetworkFirstPersonController>().RpcTakeLocalControl(lbp.gameObject);
            }
        }
    }
}
