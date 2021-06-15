using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public struct PlayerMessage : NetworkMessage
{
    public string name;
}

public class NetworkManager2 : NetworkManager
{
    private NetworkIdentity idPlayer;
    public static NetworkManager2 instance;

    private bool colapse = false, isMap = false, colapse1 = false, colapse2 = false;

    public GameObject serverOrClient, localPlayer, player;

    [SerializeField]
    private NetworkIdentity localPlayerPrefab;

    [SerializeField]
    private Transform startPoint;

    public string[] nextScene;
    public GameObject playerPrefabb;

    #region Network Manager

    public override void OnStartServer()
    {
        base.OnStartServer();
        serverOrClient.name = ("SERVER");
        NetworkServer.RegisterHandler<PlayerMessage>(OnCreateCharacter);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("Player Connected");
        base.OnClientConnect(conn);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
    }

    public override void OnClientNotReady(NetworkConnection conn)
    {
        Debug.Log("Client Not Ready");
    }

    public override void ServerChangeScene(string newSceneName)
    {
        base.ServerChangeScene(newSceneName);
        OnServerSceneChanged(newSceneName);
        OnServerChangeScene(newSceneName);
        OnClientChangeScene(newSceneName, SceneOperation.Normal, false);
    }

    public override void OnServerChangeScene(string newSceneName)
    {
        base.OnServerChangeScene(newSceneName);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);
        Debug.Log("after scene changed");
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        localPlayer = Instantiate(localPlayerPrefab.gameObject, transform.position, Quaternion.identity);

        localPlayer.name = $"{localPlayerPrefab.gameObject.name} [connId={conn.connectionId}]";
        NetworkServer.AddPlayerForConnection(conn, localPlayer);

        base.OnServerReady(conn);
    }

    public override void OnClientChangeScene(string newSceneName, SceneOperation sceneOperation, bool customHandling)
    {
        GameObject[] allObject = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject plyr in allObject)
        {
            if (plyr.CompareTag("Player"))
            {
                NetworkServer.Destroy(plyr);
            }
        }
        Debug.Log("destroy p;layer");
        base.OnClientChangeScene(newSceneName, sceneOperation, customHandling);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        PlayerMessage characterMessage = new PlayerMessage
        {
        };

        conn.Send(characterMessage);
        base.OnClientSceneChanged(conn);

        Debug.Log("Client Changed Scene Connected");
    }

    private void OnCreateCharacter(NetworkConnection conn, PlayerMessage message)
    {
        player = Instantiate(playerPrefabb, transform.position, transform.rotation);

        NetworkServer.Spawn(player, conn);
    }

    #endregion Network Manager

    #region MonoBehaviour

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (!isNetworkActive)
        {
            Debug.Log("ADD SERVER");
            StartServer();
            networkAddress = "localhost";
        }
    }

    private void Update()
    {
        Scene m_Scene = SceneManager.GetActiveScene();

        if (numPlayers >= 1)
        {
            if (!colapse)
            {
                isMap = true;
                colapse = true;
                ServerChangeScene(nextScene[0]);
            }
        }
    }

    #endregion MonoBehaviour
}