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
            player.GetComponent<NetworkPlayer>().Connection = conn;

            // Also need to force Type sync
            foreach(var networkPlayer in lobbySlots)
            {
                NetworkPlayer np = networkPlayer as NetworkPlayer;
                if (np != null && np != player.GetComponent<NetworkPlayer>())
                {
                    np.Type = np.Type; // Seems stupid but should work
                }
            }
            return player;
        }
    }
}
