using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkEndLevelLine : NetworkBehaviour
{

    NetworkCommands m_NetworkCommands;
    int m_PlayerCount = 0;

    void Start()
    {
        m_NetworkCommands = FindObjectOfType<NetworkCommands>();
    }

    [ServerCallback]
    void Update()
    {
        if(m_PlayerCount == 2)
        {
            RpcSendLevelFinished();
        }
    }

    [ClientRpc]
    public void RpcSendLevelFinished()
    {
        m_NetworkCommands.DisplayLevelFinished();
    }

    [ServerCallback]
    void OnTriggerEnter(Collider _collider)
    {
        if (_collider.tag == "Player")
            m_PlayerCount++;
    }

    [ServerCallback]
    void OnTriggerExit(Collider _collider)
    {
        if(_collider.tag == "Player")
            m_PlayerCount--;
    }
}
