using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AdvancedNetworkManager : NetworkLobbyManager {

    [Header("Advanced Properties")]
    [SerializeField]
    private GameObject m_LobbyMenu;

    static public AdvancedNetworkManager s_Singleton;

    void Start()
    {
        s_Singleton = this;

        DontDestroyOnLoad(gameObject);
    }

    public void StartLobbyAsHost()
    {
        StartHost();
    }
    
    public void JoinLobby()
    {
        GameController.Instance().ChangeMenuTo(m_LobbyMenu);
        GameController.Instance().DebugText("Joining Player", 5);

        this.networkAddress = "127.0.0.1";
        this.StartClient();
    }

    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject obj = Instantiate<GameObject>(lobbyPlayerPrefab.gameObject);
        LobbyPlayer player = obj.GetComponent<LobbyPlayer>();

        GameController.Instance().DebugText("New Player Connected...", 5);

        return obj;
    }

    public override void OnStartHost()
    {
        base.OnStartHost();

        GameController.Instance().ChangeMenuTo(m_LobbyMenu);
        GameController.Instance().DebugText("Starting Host", 5);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        if(!NetworkServer.active) // checking if we are a pure client (no hosting)
        {
            GameController.Instance().ChangeMenuTo(m_LobbyMenu);
            GameController.Instance().DebugText("Connected to server", 5);
        }
    }
}
