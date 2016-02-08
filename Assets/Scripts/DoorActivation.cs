using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DoorActivation : NetworkBehaviour
{

    [SerializeField]
    GameObject m_doorHandler;

    [ServerCallback]
	void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject.layer == LayerMask.NameToLayer("ButtonActivator"))
        {
            RpcOpenDoor();
        }
    }

    [ServerCallback]
    void OnCollisionExit(Collision _collision)
    {
        if (_collision.gameObject.layer == LayerMask.NameToLayer("ButtonActivator"))
        {
            RpcCloseDoor();
        }
    }

    [ClientRpc]
    public void RpcOpenDoor()
    {
        foreach (var door in m_doorHandler.GetComponents<DoorComponent>())
        {
            door.Open();
        }
    }

    [ClientRpc]
    public void RpcCloseDoor()
    {
        foreach (var door in m_doorHandler.GetComponents<DoorComponent>())
        {
            door.Close();
        }
    }

}
