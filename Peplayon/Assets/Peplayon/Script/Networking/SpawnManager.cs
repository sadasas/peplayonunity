using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : NetworkBehaviour
{
    public bool isLobbyScene = false;
    public static SpawnManager instance;
    private int isCharacterOne, isCharacterTwo, isCharacterThree;
    private Transform player;
    public GameObject dustTriggerPrefab, killZonePrefab, cameraPrefab, cameraPrefab1, plyr, cameraPlayer;
    public int startpos = 0;
    public GameObject[] characterPrefab, characterPrefabMap3;
    public bool characterAdded = false, characterAdded1 = false;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if (isLocalPlayer)
        {
            isCharacterOne = PlayerPrefs.GetInt("CharacterOne");
            isCharacterTwo = PlayerPrefs.GetInt("CharacterTwo");
            isCharacterThree = PlayerPrefs.GetInt("CharacterThree");
        }
    }

    //[Server]
    public void SetCharLobby(NetworkConnection conn)
    {
        Debug.Log("SetCharLobby");
        NetworkServer.Spawn(GetChar(), conn);
        NetworkServer.Spawn(SetCam(), conn);
        characterAdded = true;
        isLobbyScene = true;
        startpos++;
    }

    //[Server]
    public GameObject GetChar()
    {
        if (isCharacterOne == 1)
        {
            plyr = Instantiate(characterPrefab[0], NetworkManager.startPositions[startpos].position, transform.rotation);
        }
        else if (isCharacterTwo == 1)
        {
            plyr = Instantiate(characterPrefab[1], NetworkManager.startPositions[startpos].position, transform.rotation);
        }
        else if (isCharacterThree == 1)
        {
            plyr = Instantiate(characterPrefab[2], NetworkManager.startPositions[startpos].position, transform.rotation);
        }
        else
        {
            plyr = Instantiate(characterPrefab[0], NetworkManager.startPositions[startpos].position, transform.rotation);
        }
        return plyr;
    }

    //[Server]
    public GameObject SetCam()
    {
        cameraPlayer = Instantiate(cameraPrefab, NetworkManager.startPositions[startpos].position, transform.rotation);
        return cameraPlayer;
    }

    //[Server]
    public void SetCharacter(NetworkConnection conn)
    {
        Transform killZonePoint_0 = GameObject.Find("KillZone_0").transform;

        GameObject setKillZone_0 = Instantiate(killZonePrefab, killZonePoint_0.position, Quaternion.identity);

        NetworkServer.Spawn(GetChar(), conn);
        NetworkServer.Spawn(SetCam(), conn);
        NetworkServer.Spawn(setKillZone_0, conn);
        characterAdded = true;
        startpos++;
    }

    [Server]
    public void SetCharactermAP3(NetworkConnection conn)

    {
        if (isCharacterOne == 1)
        {
            plyr = Instantiate(characterPrefab[0], NetworkManager.startPositions[startpos].position, transform.rotation);
        }
        else if (isCharacterTwo == 1)
        {
            plyr = Instantiate(characterPrefab[1], NetworkManager.startPositions[startpos].position, transform.rotation);
        }
        else if (isCharacterThree == 1)
        {
            plyr = Instantiate(characterPrefab[2], NetworkManager.startPositions[startpos].position, transform.rotation);
        }
        else
        {
            plyr = Instantiate(characterPrefab[0], NetworkManager.startPositions[startpos].position, transform.rotation);
        }

        cameraPlayer = Instantiate(cameraPrefab, NetworkManager.startPositions[startpos].position, transform.rotation);

        NetworkServer.Spawn(plyr, conn);
        NetworkServer.Spawn(cameraPlayer, conn);

        characterAdded = true;
        startpos++;
    }

    [ClientRpc]
    private void add()
    {
        Debug.Log("SPAWN DUST");
        characterAdded = true;
    }

    private void Update()
    {
        if (characterAdded)
        {
            if (isServer)
            {
                add();
                characterAdded = false;
            }
            if (isLocalPlayer)
            {
                AuthoryManager.instance.CMDChangeTag();
                AuthoryManager.instance.colapse1 = false;
                characterAdded = false;
                if (isLobbyScene)
                {
                    CharacterControls.isLobbyScene = true;
                    isLobbyScene = false;
                } else
                {
//Default wahyu true
                    NetworkManagerTesting.instance.SetCutScene();
                    CharacterControls.cutsceneawal = true;
                }

                CMDspawnDust();
            }
        }
    }

    [Command]
    public void CMDspawnDust()
    {
        ClienRPCSpawnDust();
    }

    [ClientRpc]
    public void ClienRPCSpawnDust()
    {
        MovableObs.ready = true;
        player = null;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (isCharacterOne == 1)
        {
            Transform point = player.transform.GetChild(5).transform;
            Vector3 ss = new Vector3(player.position.x, 2f, player.position.z);
            GameObject cc = Instantiate(dustTriggerPrefab, point.position, player.rotation, player);
            NetworkIdentity gb = cc.GetComponent<NetworkIdentity>();
        }
        else if (isCharacterTwo == 1)
        {
            Transform point = player.transform.GetChild(7).transform;
            Vector3 ss = new Vector3(player.position.x, 2.4f, player.position.z);
            GameObject cc = Instantiate(dustTriggerPrefab, point.position, player.rotation, player);
            NetworkIdentity gb = cc.GetComponent<NetworkIdentity>();
        }
        else
        {
            Transform point = player.transform.GetChild(5).transform;
            Vector3 pos = new Vector3(player.position.x, 2.75f, player.position.z);
            GameObject fuss = Instantiate(dustTriggerPrefab, point.position, player.rotation, player);
            NetworkIdentity gb = fuss.GetComponent<NetworkIdentity>();
        }
    }
}