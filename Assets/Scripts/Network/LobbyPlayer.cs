using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyPlayer : NetworkLobbyPlayer {
    private static Color ReadyColor = Color.green;
    private static Color NotReadyColor = Color.red;

    [SyncVar(hook = "OnPlayerTypeSync")]
    public int m_PlayerType = 0; // 0 = scientist, 1 = robot (TODO: replace by enum)

    [SyncVar(hook = "OnNameSync")]
    public string m_PlayerName = "Player";

    public UnityEngine.UI.Text m_PlayerTypeLabel;
    public UnityEngine.UI.Button m_PlayerReadyButton;

    public InputField m_PlayerNameInput;

    public override void OnClientEnterLobby()
    {
        base.OnClientEnterLobby();

        if(isLocalPlayer)
        {
            SetupLocalPlayer();
        }
        else
        {
            SetupOtherPlayer();
        }

        OnNameSync(m_PlayerName);
        OnPlayerTypeSync(m_PlayerType);
    }

    public override void OnClientReady(bool readyState)
    {
        if(readyState)
        {
            ChangeReadyButtonColor(ReadyColor);
        }
        else
        {
            ChangeReadyButtonColor(isLocalPlayer ? NotReadyColor : NotReadyColor);
            m_PlayerReadyButton.interactable = isLocalPlayer;
        }
    }

    private void OnNameSync(string _name)
    {
        m_PlayerName = _name;
        m_PlayerNameInput.text = m_PlayerName;
    }

    private void OnPlayerTypeSync(int _type)
    {
        m_PlayerType = _type;
        m_PlayerTypeLabel.text = _type == 0 ? "Scientist" : _type == 1 ? "Robot" : "Error in the system.";
    }

    private void ChangeReadyButtonColor(Color c)
    {
        ColorBlock b = m_PlayerReadyButton.colors;
        b.normalColor = c;
        b.pressedColor = c;
        b.highlightedColor = c;
        b.disabledColor = c;
        m_PlayerReadyButton.colors = b;
    }

    private void SetupLocalPlayer()
    {
        m_PlayerNameInput.interactable = true;
        m_PlayerReadyButton.interactable = true;

        ChangeReadyButtonColor(NotReadyColor);

        m_PlayerNameInput.onEndEdit.RemoveAllListeners();
        m_PlayerNameInput.onEndEdit.AddListener(OnNameChanged);

        m_PlayerReadyButton.onClick.RemoveAllListeners();
        m_PlayerReadyButton.onClick.AddListener(OnReadyClicked);
    }

    private void OnReadyClicked()
    {
        SendReadyToBeginMessage();
        
    }

    public void OnNameChanged(string str)
    {
        CmdNameChanged(str);
    }

    [Command]
    public void CmdNameChanged(string name)
    {
        m_PlayerName = name;
    }

    private void SetupOtherPlayer()
    {
        m_PlayerNameInput.interactable = false;
        m_PlayerReadyButton.interactable = false;

        ChangeReadyButtonColor(NotReadyColor);
    }
}
