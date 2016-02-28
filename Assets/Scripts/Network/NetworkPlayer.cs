using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityStandardAssets.Network;

public class NetworkPlayer : LobbyPlayer
{
    [SerializeField]
    private Button m_PlayerTypeButton;

    [SyncVar(hook = "SyncPlayerType")]
    private PlayerType m_type;
    public PlayerType Type
    {
        get { return m_type; }
        set
        {
            m_type = value;
            UpdateTypeButton();
        }
    }

    public void SyncPlayerType(PlayerType _type)
    {
        Type = _type;
    }
    
    private void UpdateTypeButton()
    {
        switch(Type)
        {
            case PlayerType.ROBOT:
                m_PlayerTypeButton.GetComponentInChildren<Text>().text = "Robot";
                break;
            case PlayerType.SCIENTIST:
                m_PlayerTypeButton.GetComponentInChildren<Text>().text = "Scientist";
                break;
            default:
                m_PlayerTypeButton.GetComponentInChildren<Text>().text = "NONE";
                break;
        }
    }
}
