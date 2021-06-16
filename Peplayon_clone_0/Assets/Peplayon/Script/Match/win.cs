using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Peplayon;

public class win : NetworkBehaviour
{
    public static win instance;

    [SerializeField]
    private GameObject HUDLose;

    public int index;
    private GameObject currentQualified;

    public int playerlolos, spaceindex = 1, kualifikasi;
    public string[] sceneList;

    public GameObject canvasDisplayQualified, canvasDisplayChangeCamera, CameraSee;
    private GameObject camera1;
    private bool tru, lolos;

    public float TweenTimeQualified;

    private Transform modeView1, modeView2, modeView3;

    private Vector3 currentModeView;
    private bool gg;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        camera1 = GameObject.FindGameObjectWithTag("PlayerCamera").gameObject;
        modeView1 = GameObject.FindGameObjectWithTag("ModeView1").transform;
        modeView2 = GameObject.FindGameObjectWithTag("ModeView2").transform;
        modeView3 = GameObject.FindGameObjectWithTag("ModeView3").transform;
    }

    private void Update()
    {
        if (tru)
        {
            ClientSetWin();

            Debug.Log("lolos");
        }

        if (lolos && hasAuthority)
        {
            if (playerlolos == kualifikasi && !gg)
            {
                NetworkClient.Ready();
                gg = true;
                NextMap();
            }

            canvasDisplayChangeCamera.SetActive(true);
            LeanTween.scale(canvasDisplayChangeCamera, Vector3.one, TweenTimeQualified);
            if (Input.GetKeyDown(KeyCode.Space) && spaceindex == 1)
            {
                spaceindex++;

                CameraSee.SetActive(true);

                Debug.Log("changeCamera");
                currentModeView = CameraSee.transform.position;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && spaceindex == 2)
            {
                spaceindex++;
                Debug.Log("changeCamera1");
                CameraSee.transform.position = modeView1.position;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && spaceindex == 3)
            {
                spaceindex++;
                Debug.Log("changeCamera2");
                CameraSee.transform.position = modeView2.position;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && spaceindex == 4)
            {
                spaceindex++;

                CameraSee.transform.position = modeView3.position;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && spaceindex == 5)
            {
                spaceindex = 1;

                CameraSee.transform.position = currentModeView;
            }
        }
        else if (!lolos)
        {
            if (playerlolos == kualifikasi)
            {
                AuthoryManager am = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AuthoryManager>();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                camera1.SetActive(true);
                am.Lose = true;
                Debug.Log("LALAHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject != currentQualified)
            {
                tru = true;
                AuthoryManager am = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AuthoryManager>();
                NetworkIdentity player = other.GetComponent<NetworkIdentity>();
                NetworkIdentity item = GetComponent<NetworkIdentity>();
                am.removeAutority(item);
                am.getauthority(item, player);
                currentQualified = other.gameObject;
                Debug.Log("TriggerWin");
            }
        }
    }

    public void ClientSetWin()
    {
        if (!hasAuthority) return;
        tru = false;
        GameObject localplayer = NetworkClient.localPlayer.gameObject;
        NetworkIdentity get = localplayer.GetComponent<NetworkIdentity>();
        string nm = get.netId.ToString();
        Debug.Log(nm);

        CMDWin(nm);
    }

    [Command]
    public void CMDWin(string kl)
    {
        CliensRPCsetWin(kl);
    }

    [ClientRpc]
    public void CliensRPCsetWin(string hj)
    {
        playerlolos++;
        GameObject klkl = ClientScene.localPlayer.gameObject;
        NetworkIdentity ff = klkl.GetComponent<NetworkIdentity>();
        string hh = ff.netId.ToString();
        Debug.Log(hh);

        canvasDisplayQualified.SetActive(true);
        LeanTween.scale(canvasDisplayQualified, Vector3.one, TweenTimeQualified);

        GameObject score = canvasDisplayQualified.transform.GetChild(2).gameObject;
        score.GetComponent<Text>().text = playerlolos.ToString();
        if (hh == hj)
        {
            lolos = true;
            Debug.Log("Win");

            CharacterControls cr = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControls>();

            cr.lolos();
        }
        else
        {
        }
    }

    [Command]
    private void NextMap()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        Debug.Log("GGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG");

        if (index == 1)
        {
            NetworkManagerTesting.instance.ServerChangeScene(NetworkManagerTesting.instance.sceneNameList[2]);
        }
        else if (index == 2)
        {
            NetworkManagerTesting.instance.ServerChangeScene(NetworkManagerTesting.instance.sceneNameList[1]);
        }
        else if (index == 3)
        {
            NetworkManagerTesting.instance.ServerChangeScene(NetworkManagerTesting.instance.sceneNameList[3]);
        }
    }
}