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
        if (activeScene.name == sceneNameList[3] || activeScene.name == sceneNameList[2])
        {
            ObstacleManager.instance.setItem = true;
            ObstacleManager.instance.pointAdded = false;
        }

        if (activeScene.name == sceneNameList[3])
        {
            ObstacleManager.instance.setObstacleMap2 = true;
        }
        if (activeScene.name == sceneNameList[4])
        {
            ObstacleManager.instance.setObstacleMap3 = true;
        }
        if (activeScene.name == sceneNameList[1])
        {
            GameObject randomScene = Instantiate(randomScenePrefab, transform.position, transform.rotation);
            NetworkServer.Spawn(randomScene);
        }

        base.OnServerSceneChanged(sceneName);
        Debug.Log("SERVER  SCENE CHANGED");
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == sceneNameList[2] || activeScene.name == sceneNameList[3])
        {
            SpawnManager.instance.SetCharacter(conn);
        }
        if (activeScene.name == sceneNameList[4])
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
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name != sceneNameList[0] && activeScene.name != sceneNameList[1] && activeScene.name != sceneNameList[4])
        {
            Debug.Log("ADD CHARACTER AND CAMERA");
            isCharacterOne = PlayerPrefs.GetInt("CharacterOne");
            isCharacterTwo = PlayerPrefs.GetInt("CharacterTwo");
            isCharacterThree = PlayerPrefs.GetInt("CharacterThree");
        }

        localPlayer = Instantiate(localPlayerPrefab.gameObject, NetworkManager.startPositions[1].position, Quaternion.identity);

        localPlayer.name = $"{localPlayerPrefab.gameObject.name} [connId={conn.connectionId}]";

        NetworkServer.AddPlayerForConnection(conn, localPlayer);
    }

    private void SetObstacle1Map2()

    {
        Transform point = GameObject.FindGameObjectWithTag("1").transform;
        Transform point2 = GameObject.FindGameObjectWithTag("2").transform;
        Transform point3 = GameObject.FindGameObjectWithTag("3").transform;
        Transform point4 = GameObject.FindGameObjectWithTag("4").transform;
        Transform point5 = GameObject.FindGameObjectWithTag("5").transform;
        Transform point6 = GameObject.FindGameObjectWithTag("6").transform;
        Transform point7 = GameObject.FindGameObjectWithTag("7").transform;
        Transform point8 = GameObject.FindGameObjectWithTag("8").transform;
        Transform point9 = GameObject.FindGameObjectWithTag("9").transform;
        Transform point10 = GameObject.FindGameObjectWithTag("10").transform;
        Transform point11 = GameObject.FindGameObjectWithTag("11").transform;
        Transform point12 = GameObject.FindGameObjectWithTag("12").transform;
        Transform point13 = GameObject.FindGameObjectWithTag("13").transform;
        Transform point14 = GameObject.FindGameObjectWithTag("14").transform;
        Transform point15 = GameObject.FindGameObjectWithTag("15").transform;
        Transform point16 = GameObject.FindGameObjectWithTag("16").transform;
        GameObject row1 = Instantiate(obstacle1Map2Prefab[0], point.position, point.rotation);
        GameObject row2 = Instantiate(obstacle1Map2Prefab[1], point2.position, point.rotation);
        GameObject row3 = Instantiate(obstacle1Map2Prefab[2], point3.position, point.rotation);
        GameObject row4 = Instantiate(obstacle1Map2Prefab[3], point4.position, point.rotation);
        GameObject row5 = Instantiate(obstacle1Map2Prefab[4], point5.position, point.rotation);
        GameObject row6 = Instantiate(obstacle1Map2Prefab[5], point6.position, point.rotation);
        GameObject row7 = Instantiate(obstacle1Map2Prefab[6], point7.position, point.rotation);
        GameObject row8 = Instantiate(obstacle1Map2Prefab[7], point8.position, point.rotation);
        GameObject Obstacle1 = Instantiate(obstacle2Map2Prefab[0], point9.position, point9.rotation);
        GameObject Obstacle2 = Instantiate(obstacle2Map2Prefab[1], point10.position, point10.rotation);
        GameObject Obstacle3 = Instantiate(obstacle2Map2Prefab[2], point11.position, point11.rotation);
        GameObject Obstacle4 = Instantiate(obstacle3Map2Prefab[0], point12.position, point12.rotation);
        GameObject Obstacle5 = Instantiate(obstacle3Map2Prefab[1], point13.position, point13.rotation);
        GameObject Obstacle6 = Instantiate(obstacle3Map2Prefab[2], point14.position, point14.rotation);
        GameObject Obstacle7 = Instantiate(obstacle3Map2Prefab[3], point15.position, point15.rotation);
        GameObject Obstacle8 = Instantiate(obstacle3Map2Prefab[4], point16.position, point16.rotation);

        NetworkServer.Spawn(row1);
        NetworkServer.Spawn(row2);
        NetworkServer.Spawn(row3);
        NetworkServer.Spawn(row4);
        NetworkServer.Spawn(row5);
        NetworkServer.Spawn(row6);
        NetworkServer.Spawn(row7);
        NetworkServer.Spawn(row8);
        NetworkServer.Spawn(Obstacle1);
        NetworkServer.Spawn(Obstacle2);
        NetworkServer.Spawn(Obstacle3);
        NetworkServer.Spawn(Obstacle4);
        NetworkServer.Spawn(Obstacle5);
        NetworkServer.Spawn(Obstacle6);
        NetworkServer.Spawn(Obstacle7);
        NetworkServer.Spawn(Obstacle8);
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
        Debug.Log(numPlayers);
        if (numPlayers >= 1)
        {
            if (!colapse)
            {
                colapse = true;
                ChangeScene(1);
                Debug.Log("START MATCH");
            }
        }
        else
        {
            Debug.Log("WAIT OTHER PLAYER JOIN");
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            ChangeScene(2);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            ChangeScene(3);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            ChangeScene(1);
        }

        if (Input.GetKey(KeyCode.Alpha6))
        {
            ChangeScene(4);
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

    #endregion MonoBehaviour
}