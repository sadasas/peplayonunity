using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using Peplayon;
using Networking;
using kcp2k;

public struct PlayerMessages : NetworkMessage
{
    public string name;
}

public struct IndexPlayer : NetworkMessage
{
    public int cc;
}

public class NetworkManagerTesting : NetworkManager
{
    [Header("Tambahan Bocil")]
    public ushort portRunning;
    [Scene] public string LobbyScene;
    [SerializeField] GameObject networkPlayerManagerPrefab;
    public GameObject netPlayer;

    [Header("Wahyu")]
    private int playerCount;

    public static NetworkManagerTesting instance;

    private bool colapse = false;

    public GameObject player = null, cameraPrefab, killZonePrefab, cameraPlayer, randomScenePrefab, spawnManagerPrefab;
    public GameObject[] characterPrefab, itemPrefab, obstacle1Map2Prefab, obstacle2Map2Prefab, obstacle3Map2Prefab;

    public List<Transform> spawnPointItem = new List<Transform>();

    /**
     * GW PAKAI ATTR DARI MIRROR
     * SCENE ATR MIRROR RETURN STRING(PATH SCENE) EXAMPLE : ASSET/SCENE.UNITY
     */
    [Scene]
    public string[] sceneNameList;

    [SerializeField]
    private NetworkIdentity localPlayerPrefab;

    #region Network Manager

    public override void OnStartServer()
    {
        Debug.Log("SERVER ACTIVE");
        portRunning = GetComponent<KcpTransport>().Port;
        Debug.Log($"Running in port : {portRunning}");
        base.OnStartServer();
        netPlayer = Instantiate(spawnPrefabs.Find(pref => pref.name == "--NetPlayerManager"));
        NetworkServer.Spawn(netPlayer);
        GameObject go = Instantiate(spawnManagerPrefab, NetworkManager.startPositions[startPositionIndex].position, Quaternion.identity);
        NetworkServer.Spawn(go);
    }

    public override void OnStartClient()
    {
        Debug.Log("CLIENT ACTIVE");
        base.OnStartClient();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("PLAYER CONNECTED");
        base.OnClientConnect(conn);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
    }

    public override void OnClientNotReady(NetworkConnection conn)
    {
        base.OnClientNotReady(conn);
    }

    public override void ServerChangeScene(string newSceneName)
    {
        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerChangeScene(string newSceneName)
    {
        playerCount = 0;
        Scene activeScene = SceneManager.GetActiveScene();
        if(activeScene.path != onlineScene)
        {
            ObstacleManager.instance.itemPoint.Clear();
        }

        base.OnServerChangeScene(newSceneName);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Debug.Log("OnServerAddPlayer");
        base.OnServerAddPlayer(conn);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if(activeScene.path == onlineScene)
        {
            Debug.Log("lobby scene");
        }
        if (activeScene.path == sceneNameList[2] || activeScene.path == sceneNameList[1])
        {
            Debug.Log("OnServerSceneChanged map");
            ObstacleManager.instance.setItem = true;
            ObstacleManager.instance.setWin = true;
            ObstacleManager.instance.pointAdded = false;
        }

        SpawnManager.instance.startpos = 0;

        if (activeScene.path == sceneNameList[2])
        {
            ObstacleManager.instance.setObstacleMap2 = true;
        }
        if (activeScene.path == sceneNameList[3])
        {
            ObstacleManager.instance.setObstacleMap3 = true;
        }
        base.OnServerSceneChanged(sceneName);
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.path == sceneNameList[1] || activeScene.path == sceneNameList[2] || activeScene.path == sceneNameList[3])
        {
            SpawnManager.instance.isLobbyScene = false;
            SpawnManager.instance.SetCharacter(conn);
        }

        if (activeScene.path == sceneNameList[0])
        {
            AddLocalPlayer(conn);
            SpawnManager.instance.isLobbyScene = true;
            SpawnManager.instance.SetCharLobby(conn);
        }
        playerCount++;

        base.OnServerReady(conn);
    }

    public override void OnClientChangeScene(string newSceneName, SceneOperation sceneOperation, bool customHandling)
    {
        base.OnClientChangeScene(newSceneName, sceneOperation, customHandling);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        NetworkClient.Ready();
        base.OnClientSceneChanged(conn);
    }

    #endregion Network Manager

    #region Network Message

    private void AddLocalPlayer(NetworkConnection conn)
    {
        //Debug.Log("AddLocalPlayer");
        //NetworkServer.Spawn(Instantiate(networkPlayerManagerPrefab));

        GameObject localPlayer = Instantiate(localPlayerPrefab.gameObject, NetworkManager.startPositions[1].position, Quaternion.identity);

        localPlayer.name = $"{localPlayerPrefab.gameObject.name} [connId={conn.connectionId}]";

        NetworkServer.AddPlayerForConnection(conn, localPlayer);
        NetworkPlayerManager.instance.IntPlayerOnline = numPlayers;

    }

    #endregion Network Message

    #region MonoBehaviour

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        /**
         * STRESS
         */
        //if (!isNetworkActive)
        //{
        //    Debug.Log("ADD SERVER");
        //    StartServer();
        //    networkAddress = "localhost";
        //}
    }

    [Server]
    private void Update()
    {
        //Scene activeScene = SceneManager.GetActiveScene();
        //if (activeScene.name == sceneNameList[3])
        //{
        //    if (numPlayers == 1)
        //    {
        //        ChangeScene(4);
        //    }
        //}
        ////Debug.Log(numPlayers);
        ///**
        // * <remarks>Ini gw matiin buat start ada dilobby</remarks>
        // */
        //if (numPlayers >= 2)
        //{
        //    if (!colapse)
        //    {
        //        //colapse = true;
        //        //SetRandomMap();
        //        //Debug.Log("START MATCH");
        //    }
        //}
        //else
        //{
        //    Debug.Log("WAIT OTHER PLAYER JOIN");
        //}

        //if (Input.GetKey(KeyCode.Alpha1))
        //{
        //    ChangeScene(1);
        //}

        //if (Input.GetKey(KeyCode.Alpha2))
        //{
        //    ChangeScene(2);
        //}
        //if (Input.GetKey(KeyCode.Alpha3))
        //{
        //    //ChangeScene(3);
        //}
    }

    public void SetCutScene()
    {
        Debug.Log("SetCutScene");
        UI.instance.CMDstartcutscene();
    }

    public void ChangeScene(int index)
    {
        ServerChangeScene(sceneNameList[index]);
    }

    public void SetRandomMap()
    {
        GameObject randomScene = Instantiate(randomScenePrefab, transform.position, transform.rotation);
        NetworkServer.Spawn(randomScene);
    }

    #endregion MonoBehaviour

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        NetworkPlayerManager.instance.IntPlayerOnline = numPlayers;
        base.OnServerDisconnect(conn);
    }

    public void StartGame()
    {
        CharacterControls.isLobbyScene = false;
        ChangeScene(1);
    }
}