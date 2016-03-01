using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

class LocalAuthorityDebugger : NetworkBehaviour
{
    NetworkConnection m_owner = null;

    void Start()
    {
        m_owner = GetComponent<NetworkIdentity>().clientAuthorityOwner;
        Debug.Log("Authority of object " + name + " is given to : " + (m_owner != null ? m_owner.address : "null"));
        GameManager.Instance().Debugger().AddMessage("Authority of object " + name + " is given to : " + (m_owner != null ? m_owner.address : "null"));
    }

    void Update()
    {
        var owner = GetComponent<NetworkIdentity>().clientAuthorityOwner;
        if(owner != m_owner)
        {
            m_owner = owner;
            Debug.Log("Authority of object " + name + " is given to : " + (m_owner != null ? m_owner.address : "null"));
            GameManager.Instance().Debugger().AddMessage("Authority of object " + name + " is given to : " + (m_owner != null ? m_owner.address : "null"));
        }
    }
}
