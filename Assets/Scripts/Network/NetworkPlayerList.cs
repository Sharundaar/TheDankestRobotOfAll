using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

class NetworkPlayerList : NetworkBehaviour
{
    private LinkedList<PlayerInformations> m_players = new LinkedList<PlayerInformations>();
    private PlayerInformations m_localPlayer;

    [Command]
    public void CmdSyncPlayerInformationsList()
    {
        SyncPlayerInformationsList();
    }

    public void SyncPlayerInformationsList()
    {
        if(isServer)
        {
            GameManager.Instance().Debugger().AddMessage("Syncing players.", Color.magenta);
            foreach (var info in m_players)
            {
                RpcSyncPlayerInformations(info.ID, info.Name, info.Color, info.Type);
            }
        }
    }

    [ClientRpc]
    public void RpcSyncPlayerInformations(int _id, string _name, Color _color, PlayerInformations.PlayerType _type)
    {
        bool found = false;
        foreach (var info in m_players)
        {
            if (info.ID == _id)
            {
                GameManager.Instance().Debugger().AddMessage("Player found and synced : " + info.ID, Color.green);

                info.Name = _name;
                info.Color = _color;
                info.Type = _type;
                found = true;
                break;
            }
        }

        if (!found)
        {
            CreatePlayerInformation(_id, _name, _color, _type);
        }
    }

    public void CreatePlayerInformation(int _id, string _name, Color _color, PlayerInformations.PlayerType _type)
    {
        m_players.AddLast(new PlayerInformations(_id, _name, _color, _type));
    }

    public void RemovePlayerInformations(int _id)
    {
        PlayerInformations player = null;
        foreach (var info in m_players)
        {
            if (info.ID == _id)
            {
                player = info;
                break;
            }
        }

        if (player != null)
        {
            m_players.Remove(player);
            GameManager.Instance().Debugger().AddMessage("Remove player id : " + player.ID, Color.red);
        }
    }

    [ClientRpc]
    public void RpcRemovePlayerInformations(int _id)
    {
        RemovePlayerInformations(_id);
    }

    public void ClearList()
    {
        m_players.Clear();
    }
}
