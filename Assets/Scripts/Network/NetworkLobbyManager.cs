using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Network;

namespace Assets.Scripts.Network
{
    class NetworkLobbyManager : LobbyManager
    {
        public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
        {
            GameObject player = base.OnLobbyServerCreateLobbyPlayer(conn, playerControllerId);
            player.GetComponent<NetworkPlayer>().Type = lobbySlots.Where((NetworkLobbyPlayer) => { return NetworkLobbyPlayer != null; }).Count() > 0 ? PlayerType.ROBOT : PlayerType.SCIENTIST;
            return player;
        }
    }
}
