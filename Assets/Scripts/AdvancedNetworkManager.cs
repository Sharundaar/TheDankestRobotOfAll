using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AdvancedNetworkManager : NetworkLobbyManager {
    [SerializeField]
    private NetworkStartPosition m_ScientistSpawnPoint;

    [SerializeField]
    private NetworkStartPosition m_RobotSpawnPoint;

    static public AdvancedNetworkManager s_Singleton;

    void Start()
    {
        s_Singleton = this;

        DontDestroyOnLoad(gameObject);
    }

    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject obj = Instantiate<GameObject>(lobbyPlayerPrefab.gameObject);
        LobbyPlayer player = obj.GetComponent<LobbyPlayer>();

        return obj;
    }

    public void OnHostClicked()
    {
        StartHost();
    }

    public void OnJoinClicked()
    {
        networkAddress = "127.0.0.1";
        StartClient();
    }
}
