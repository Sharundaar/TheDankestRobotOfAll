using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameNetworkManager : NetworkManager {

    private delegate void BackButtonCallback();
    private BackButtonCallback m_BackBtnCbk;

    [SerializeField]
    private GameObject m_LobbyMenu;

    private static GameNetworkManager s_Instance = null;
    public static GameNetworkManager Instance()
    {
        return s_Instance;
    }

    private void Awake()
    {
        s_Instance = this;
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        GameController.Instance().DebugText("Connection success.", 5);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        GameController.Instance().DebugText("Successfully disconected.", 5);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);

        GameController.Instance().DebugText("New player connected.", 5);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);

        GameController.Instance().DebugText("Player disconnected.", 5);
    }

    public override void OnStartHost()
    {
        base.OnStartHost();

        GameController.Instance().DebugText("Hosting server started.", 5);
    }

    public override void OnStopHost()
    {
        base.OnStopHost();

        GameController.Instance().DebugText("Hosting server stopped.", 5);
    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        base.OnClientError(conn, errorCode);

        GameController.Instance().DebugText("An error has occured (client) : " + errorCode);
    }
    public override void OnServerError(NetworkConnection conn, int errorCode)
    {
        base.OnServerError(conn, errorCode);

        GameController.Instance().DebugText("An error has occured (server) : " + errorCode);
    }

    private void CancelHost()
    {
        StopHost();

        GameController.Instance().ReturnToMainMenu();
    }

    private void CancelJoin()
    {
        StopClient();

        GameController.Instance().ReturnToMainMenu();
    }

    public void StartHosting()
    {
        StartHost();
        m_BackBtnCbk = CancelHost;

        GameController.Instance().ChangeMenuTo(m_LobbyMenu);
    }

    public void JoinLocalServer()
    {
        networkAddress = "127.0.0.1";
        StartClient();
        m_BackBtnCbk = CancelJoin;

        GameController.Instance().ChangeMenuTo(m_LobbyMenu);
    }

    public void BackButtonClicked()
    {
        m_BackBtnCbk();
    }
}
