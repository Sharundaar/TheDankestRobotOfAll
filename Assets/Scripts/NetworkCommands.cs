using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkCommands : NetworkBehaviour {

    private Dictionary<NetworkInstanceId, NetworkIdentity> m_objects = new Dictionary<NetworkInstanceId, NetworkIdentity>(); 

	// Use this for initialization
	void Start () {
        var sceneObjects = FindObjectsOfType<NetworkIdentity>();
        foreach(var obj in sceneObjects)
        {
            m_objects.Add(obj.netId, obj);
        }
	}

    [ClientRpc]
    public void RpcRegisterNetworkId(NetworkInstanceId id)
    {
        var sceneObjects = FindObjectsOfType<NetworkIdentity>();
        foreach (var obj in sceneObjects)
        {
            if(obj.netId.Equals(id))
            {
                m_objects.Add(id, obj);
                break;
            }
        }
    }

    [ClientRpc]
    public void RpcSetParentTransformToNull(GameObject child)
    {
        // var childId = m_objects[child];
        var childTransform = child.GetComponent<Transform>();

        childTransform.parent = null;
    } 

    [ClientRpc]
    public void RpcSetParentTransform(GameObject child, GameObject parent)
    {
        // var childId = m_objects[child];

        var childTransform = child.GetComponent<Transform>();

        // var parentId = m_objects[parent];
        var parentTransform = parent.GetComponent<Transform>();

        childTransform.parent = parentTransform;
    }

    [ClientRpc]
    public void RpcSetUseGravity(GameObject obj, bool value)
    {
        // var objId = m_objects[obj];
        var objRigidbody = obj.GetComponent<Rigidbody>();
        objRigidbody.useGravity = value;
    }
}
