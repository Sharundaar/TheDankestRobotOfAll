using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


[RequireComponent(typeof(GameManager))]
public class GameNetworkManager : NetworkManager {

    [Header("New Properties")]
    [SerializeField]
    private GameObject m_LobbyMenu;

    private delegate void BackButtonCallback();
    private BackButtonCallback BckBtnCallbck = null;

    GameManager m_GameManager;

    PlayerInformations m_ScientistOwner;
    PlayerInformations m_RobotOwner;

    void Start()
    {
        this.autoCreatePlayer = false;

        m_GameManager = GetComponent<GameManager>();
    }

    public void OnBackClicked()
    {
        if (BckBtnCallbck != null)
            BckBtnCallbck();

        m_GameManager.ChangeMenuToMainMenu();
    }

    public void HostGame()
    {
        StartHost();
        m_GameManager.ChangeMenuTo(m_LobbyMenu);

        BckBtnCallbck = StopHost;
    }

    public void JoinLocalHost()
    {
        networkAddress = "127.0.0.1";
        StartClient();
        m_GameManager.ChangeMenuTo(m_LobbyMenu);

        BckBtnCallbck = StopClient;
    }

    private void OnScientistOwnerChanged()
    {

    }

    private void OnRobotOwnerChanged()
    {

    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        m_GameManager.Debugger().AddMessage("Connection success.");
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        m_GameManager.Debugger().AddMessage("Disconnection success.");
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        m_GameManager.Debugger().AddMessage("New player connected.", Color.magenta);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);

        m_GameManager.Debugger().AddMessage("A player as disconnected.", Color.magenta);
    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        base.OnClientError(conn, errorCode);

        m_GameManager.Debugger().AddMessage("<Client> - An error has occured : " + errorCode, Color.red);
    }

    public override void OnServerError(NetworkConnection conn, int errorCode)
    {
        base.OnServerError(conn, errorCode);

        m_GameManager.Debugger().AddMessage("<Server> - An error has occured : " + errorCode, Color.red);
    }

    public override void OnStartHost()
    {
        base.OnStartHost();

        m_GameManager.Debugger().AddMessage("Hosting started.");
    }

    public override void OnStopHost()
    {
        base.OnStopHost();

        m_GameManager.Debugger().AddMessage("Hosting stopped.");
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);

        m_GameManager.Debugger().AddMessage("AddPlayer called.", Color.blue);
    }

    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
    {
        base.OnServerRemovePlayer(conn, player);

        m_GameManager.Debugger().AddMessage("RemovePlayer called.");
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
    }
}
