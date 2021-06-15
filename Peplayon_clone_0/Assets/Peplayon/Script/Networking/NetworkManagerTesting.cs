using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using Peplayon;

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
    private int playerCount;

    public static NetworkManagerTesting instance;

    private bool colapse = false;

    public GameObject serverOrClient, localPlayer, player = null, cameraPrefab, killZonePrefab, cameraPlayer, randomScenePrefab, spawnManagerPrefab;
    public GameObject[] characterPrefab, itemPrefab, obstacle1Map2Prefab, obstacle2Map2Prefab, obstacle3Map2Prefab;

    public List<Transform> spawnPointItem = new List<Transform>();

    public string[] sceneNameList;
    private int isCharacterOne, isCharacterTwo, isCharacterThree;

    [SerializeField]
    private NetworkIdentity localPlayerPrefab;

    #region Network Manager

    public override void OnStartServer()
    {
        Debug.Log("SERVER ACTIVE");
        base.OnStartServer();
        serverOrClient.name = ("SERVER");

        GameObject go = Instantiate(spawnManagerPrefab, NetworkManager.startPositions[startPositionIndex].position, Quaternion.identity);
        NetworkServer.Spawn(go);
    }

    public override void OnStartClient()
    {
        Debug.Log("CLIENT ACTIVE");
        base.OnStartClient();
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
        ObstacleManager.instance.itemPoint.Clear();
        base.OnServerChangeScene(newSceneName);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == sceneNameList[2] || activeScene.name == sceneNameList[1])
        {
            ObstacleManager.instance.setItem = true;
            ObstacleManager.instance.setWin = true;
            ObstacleManager.instance.pointAdded = false;
        }

        if (activeScene.name == sceneNameList[2])
        {
            ObstacleManager.instance.setObstacleMap2 = true;
        }
        if (activeScene.name == sceneNameList[3])
        {
            ObstacleManager.instance.setObstacleMap3 = true;
        }

        base.OnServerSceneChanged(sceneName);
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == sceneNameList[1] || activeScene.name == sceneNameList[2])
        {
            SpawnManager.instance.SetCharacter(conn);
        }
        if (activeScene.name == sceneNameList[3])
        {
            SpawnManager.instance.SetCharactermAP3(conn);
        }

        playerCount++;
        if (activeScene.name == sceneNameList[0])
        {
            AddLocalPlayer(conn);
        }

        base.OnServerReady(conn);
    }

    public override void OnClientChangeScene(string newSceneName, SceneOperation sceneOperation, bool customHandling)
    {
        base.OnClientChangeScene(newSceneName, sceneOperation, customHandling);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        NetworkClient.Ready();
        Scene activeScene = SceneManager.GetActiveScene();

        base.OnClientSceneChanged(conn);
    }

    #endregion Network Manager

    #region Network Message

    private void AddLocalPlayer(NetworkConnection conn)
    {
        localPlayer = Instantiate(localPlayerPrefab.gameObject, NetworkManager.startPositions[1].position, Quaternion.identity);

        localPlayer.name = $"{localPlayerPrefab.gameObject.name} [connId={conn.connectionId}]";

        NetworkServer.AddPlayerForConnection(conn, localPlayer);
    }

    #endregion Network Message

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

    [Server]
    private void Update()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == sceneNameList[3])
        {
            if (numPlayers == 1)
            {
                Debug.Log("win");
            }
        }
        Debug.Log(numPlayers);
        if (numPlayers >= 1)
        {
            if (!colapse)
            {
                colapse = true;
                SetRandomMap();
                Debug.Log("START MATCH");
            }
        }
        else
        {
            Debug.Log("WAIT OTHER PLAYER JOIN");
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            ChangeScene(1);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            ChangeScene(2);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            ChangeScene(3);
        }
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
}