using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class AuthoryManager : NetworkBehaviour
{
    public static AuthoryManager instance;

    [SerializeField]
    private string[] mapScene;

    private Scene checkScene;

    public override void OnStartAuthority()
    {
        checkScene = SceneManager.GetActiveScene();
        base.OnStartAuthority();

        if (mapScene[0] == checkScene.name || mapScene[1] == checkScene.name)
        {
        }
    }

    private void Awake()
    {
        instance = this;
    }

    #region Get Authority

    public void getauthority(NetworkIdentity item, NetworkIdentity player)

    {
        if (!hasAuthority) return;
        changeAuthory(item, player);
        UnityEngine.Debug.Log("wahyu");
    }

    [Command]
    public void changeAuthory(NetworkIdentity itemd, NetworkIdentity played)
    {
        itemd.AssignClientAuthority(played.connectionToClient);
        UnityEngine.Debug.Log("add authory");
    }

    #endregion Get Authority

    #region Remove Authority

    public void removeAutority(NetworkIdentity item)

    {
        if (!hasAuthority) return;
        setremoveAutority(item);
        UnityEngine.Debug.Log("wahyu");
    }

    [Command]
    public void setremoveAutority(NetworkIdentity itemd)
    {
        itemd.RemoveClientAuthority();
        UnityEngine.Debug.Log("remove authory");
    }

    #endregion Remove Authority

    #region Change Tag Player

    [Command]
    public void CMDChangeTag()
    {
        ClientRPCchange();
    }

    [ClientRpc]
    public void ClientRPCchange()
    {
        GameObject[] player = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject ca in player)
        {
            if (ca.CompareTag("GameManager"))
            {
                NetworkIdentity networkIdentity = ca.GetComponent<NetworkIdentity>();
                if (!networkIdentity.hasAuthority)
                {
                    Debug.Log("jj");
                    ca.tag = "MultiplayerGameManager";
                }
            }
        }

        foreach (GameObject ba in player)
        {
            if (ba.CompareTag("Deadzone"))
            {
                NetworkIdentity nn = ba.GetComponent<NetworkIdentity>();
                if (!nn.hasAuthority)
                {
                    ba.tag = "MultiplayerDeadzone";
                    ba.SetActive(false);
                }
            }
        }
        foreach (GameObject ga in player)
        {
            if (ga.CompareTag("PlayerCamera"))
            {
                NetworkTransform netBev = ga.GetComponent<NetworkTransform>();
                NetworkIdentity nn = ga.GetComponent<NetworkIdentity>();
                if (netBev.hasAuthority && nn.hasAuthority)
                {
                    ga.tag = "PlayerCamera";
                    GameObject cam = ga.gameObject.transform.GetChild(0).GetChild(0).gameObject;
                    cam.tag = "MainCamera";
                }
                if (!netBev.hasAuthority || !nn.hasAuthority)
                {
                    ga.tag = "MultiplayerCamera";
                    ga.SetActive(false);
                }
            }
        }

        foreach (GameObject go in player)
        {
            if (go.CompareTag("Player"))
            {
                NetworkTransform netBev = go.GetComponent<NetworkTransform>();
                NetworkIdentity nn = go.GetComponent<NetworkIdentity>();
                if (netBev.hasAuthority && nn.hasAuthority)
                {
                    go.tag = "Player";
                    GameObject ind = go.gameObject.transform.GetChild(6).gameObject;
                    ind.tag = "IndicatorItemSpawn";
                }
                if (!netBev.hasAuthority || !nn.hasAuthority)
                {
                    go.tag = "Owner";
                }
            }
        }
        foreach (GameObject fffff in player)
        {
            if (fffff.CompareTag("Owner"))
            {
                NetworkTransform netBev = fffff.GetComponent<NetworkTransform>();
                NetworkIdentity nn = fffff.GetComponent<NetworkIdentity>();
                if (!netBev.hasAuthority || !nn.hasAuthority)
                {
                    Debug.Log("owner");
                    GameObject df = fffff.gameObject.transform.GetChild(6).gameObject;
                    df.tag = "MultiplayerItemSpawn";
                }
            }
        }

        /* istrue = true;*/
    }

    #endregion Change Tag Player
}