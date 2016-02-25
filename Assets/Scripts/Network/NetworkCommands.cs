using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkCommands : NetworkBehaviour {

    [SerializeField]
    UnityEngine.UI.Text m_LevelFinishedText;

	// Use this for initialization
	void Start () {
        m_LevelFinishedText.gameObject.SetActive(false);
	}

    public void DisplayLevelFinished()
    {
        m_LevelFinishedText.gameObject.SetActive(true);
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
