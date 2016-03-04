using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Network;

public class NetworkPlayerController : NetworkBehaviour {

    public bool ShowDebugInformations = false;

    [SyncVar(hook = "OnPlayerTypeChanged")]
    private PlayerType m_type;
    public PlayerType Type
    {
        get { return m_type; }
        set { m_type = value; UpdateController(); }
    }

    private NetworkFirstPersonController m_controller = null;

    private float m_oldHorizontal = 0;
    private float m_oldVertical = 0;
    private bool m_oldJump = false;
    private bool m_oldInteract = false;

    private float m_oldMouseX = 0;
    private float m_oldMouseY = 0;

    private float m_horizontal = 0; // hold horizontal axis value
    private float m_vertical = 0; // hold vertical vertical value
    private bool m_jump = false; // true when the jump button is down
    private bool m_interact = false; // true when the interact button is down

    private float m_mouseX = 0;
    private float m_mouseY = 0;

    private void OnPlayerTypeChanged(PlayerType _type)
    {
        m_type = _type;
        UpdateController();

        Debug.LogWarning("PlayerType of object : " + name + " changed");
    }

    [ClientCallback]
    void OnGUI()
    {
        if (!isLocalPlayer)
            return;

        if (ShowDebugInformations)
        {
            GUI.Label(new Rect(Screen.width - 200, 10, 200, 200), "Ping: " + Network.GetLastPing(Network.player));
        }
    }

    [ClientCallback]
    void Update() // This is called only on the client side
    {
        if (!isLocalPlayer)
            return;

        if (m_controller == null)
        {
            UpdateController(); // If we don't have any controller, we search for one
        }
        else
        {
            m_controller.MouseLook.UpdateCursorLock();

            UpdateInputValues();

            if(m_oldHorizontal != m_horizontal 
                || m_oldVertical != m_vertical 
                || m_oldJump != m_jump 
                || m_oldInteract != m_interact
                || m_oldMouseX != m_mouseX
                || m_oldMouseY != m_mouseY)
            {
                // need to send new value
                SendInputValues();
            }

            SaveInputValues();
        }
    }

    private void UpdateInputValues()
    {
        m_horizontal = Input.GetAxis("Horizontal");
        m_vertical = Input.GetAxis("Vertical");
        m_jump = Input.GetButtonDown("Jump");
        m_interact = Input.GetButtonDown("Interact");

        m_mouseX = Input.GetAxis("Mouse X");
        m_mouseY = Input.GetAxis("Mouse Y");
    }

    private void SendInputValues()
    {
        CmdSendInputValues(m_horizontal, m_vertical, m_jump, m_interact, m_mouseX, m_mouseY);
    }

    [Command]
    private void CmdSendInputValues(float _horizontal, float _vertical, bool _jump, bool _interact, float _mouseX, float _mouseY) // On Server
    {
        m_horizontal = _horizontal;
        m_vertical = _vertical;
        m_jump = _jump;
        m_interact = _interact;
        m_mouseX = _mouseX;
        m_mouseY = _mouseY;

        ControlController();
    }

    [Server]
    private void ControlController() // Call only on server !
    {
        if (m_controller == null)
        {
            var player = GameManager.Instance().GetPlayer(m_type);
            m_controller = player != null ? player.GetComponent<NetworkFirstPersonController>() : null;
        }
        else
        {
            m_controller.SetInput(m_horizontal, m_vertical, m_mouseX, m_mouseY, m_jump, m_interact);
        }
    }

    private void SaveInputValues()
    {
        m_oldInteract = m_interact;
        m_oldJump = m_jump;
        m_oldHorizontal = m_horizontal;
        m_oldVertical = m_vertical;

        m_oldMouseX = m_mouseX;
        m_oldMouseY = m_mouseY;
    }

    private void UpdateController()
    {
        if (!isLocalPlayer)
            return;

        var player = GameManager.Instance().GetPlayer(m_type);
        if(player != null)
        {
            DisableCamera();
            DisableAudioListener();
            m_controller = player.GetComponent<NetworkFirstPersonController>();
            EnableCamera();
            EnableAudioListener();
        }
    }

    private void EnableCamera()
    {
        if (m_controller != null)
            m_controller.GetComponentInChildren<Camera>().enabled = true;
    }
    
    private void EnableAudioListener()
    {
        if (m_controller != null)
            m_controller.GetComponentInChildren<AudioListener>().enabled = true;
    }

    private void DisableCamera()
    {
        if(m_controller != null)
            m_controller.GetComponentInChildren<Camera>().enabled = false;
    }

    private void DisableAudioListener()
    {
        if (m_controller != null)
            m_controller.GetComponentInChildren<AudioListener>().enabled = false;
    }


    public override float GetNetworkSendInterval()
    {
        return 0.01f;
    }
}
