using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;


[RequireComponent(typeof(GameManager))]
public class GameNetworkManager : NetworkLobbyManager {

    [Header("New Properties")]
    [SerializeField]
    private GameObject m_LobbyMenu;

    private delegate void BackButtonCallback();
    private BackButtonCallback BckBtnCallbck = null;

    GameManager m_GameManager;

    [SerializeField]
    NetworkPlayerList m_playerList;

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
        ClientScene.AddPlayer(0);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        m_GameManager.Debugger().AddMessage("Disconnection success.");
        m_playerList.ClearList();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        if (conn.address.Contains("Server")) // this is a dummy connection so we don't care
            return;

        m_GameManager.Debugger().AddMessage("New player connected.", Color.magenta);

        m_playerList.CreatePlayerInformation(conn.connectionId, "Player", Color.green, PlayerType.NONE);
        m_playerList.SyncPlayerInformationsList(); // we're on the server so we can go at it
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {   
        base.OnServerDisconnect(conn);

        m_GameManager.Debugger().AddMessage("A player has disconnected.", Color.magenta);
        m_playerList.RemovePlayerInformations(conn.connectionId); // remove info on the server
        m_playerList.RpcRemovePlayerInformations(conn.connectionId); // remove info on the clients
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
        m_playerList.ClearList();
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
