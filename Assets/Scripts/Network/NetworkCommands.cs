using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkCommands : NetworkBehaviour {

    [SerializeField]
    UnityEngine.UI.Text m_LevelFinishedText;

    private static NetworkCommands s_Instance = null;
    public static NetworkCommands Instance()
    {
        return s_Instance;
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);

        if(m_LevelFinishedText != null)
            m_LevelFinishedText.gameObject.SetActive(false);
        s_Instance = this;
	}

    public void DisplayLevelFinished()
    {
        if (m_LevelFinishedText != null)
            m_LevelFinishedText.gameObject.SetActive(true);
    }

    [ClientRpc]
    public void RpcChangeRendererColor(GameObject obj, Color _color)
    {
        obj.GetComponentInChildren<Renderer>().material.color = _color;
    }

    public void ChangeRendererColor(GameObject obj, Color _color)
    {
        if(isServer)
        {
            obj.GetComponentInChildren<Renderer>().material.color = _color;
        }
        else
        {
            RpcChangeRendererColor(obj, _color);
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
    public void RpcSetParentTransformToChild(GameObject child, GameObject parent, int childIndex)
    {
        var childTransform = child.transform;
        var parentTransform = parent.transform;

        childTransform.parent = parentTransform.GetChild(childIndex);
    }

    [ClientRpc]
    public void RpcSetUseGravity(GameObject obj, bool value)
    {
        // var objId = m_objects[obj];
        var objRigidbody = obj.GetComponent<Rigidbody>();
        objRigidbody.useGravity = value;
    }

    [ClientRpc]
    public void RpcSetKinematic(GameObject obj, bool value)
    {
        var objRigidbody = obj.GetComponent<Rigidbody>();
        objRigidbody.isKinematic = value;
    }
}
