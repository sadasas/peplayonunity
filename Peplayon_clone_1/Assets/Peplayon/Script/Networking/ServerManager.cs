using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerManager : MonoBehaviour
{
    public int index;

    public GameObject AllEnvironment, AllCameraView, AllHUD, AllObstacle, AllPointSpawn, AllAudio, AllPointSpawn2, HUDServer;

    public GameObject HUDServer1;

    public bool colapse = false;

    private void Update()
    {
        if (NetworkServer.active && !colapse)
        {
            Debug.Log("ssssssssssssssssssffffffffffffffffffffffffffffffff");
            colapse = true;
            if (index == 1)
            {
                ServerMainMenu();
            }
            else if (index == 2)
            {
                ServerMap1();
            }
        }
    }

    [Server]
    private void ServerMainMenu()
    {
        HUDServer1.SetActive(true);
        AllHUD.SetActive(false);
    }

    [Server]
    private void ServerMap1()
    {
        Debug.Log("YOO");
        AllEnvironment.SetActive(false);

        AllHUD.SetActive(false);
        AllObstacle.SetActive(false);
        AllPointSpawn.SetActive(false);
        AllPointSpawn2.SetActive(false);
        AllCameraView.SetActive(false);
        AllAudio.SetActive(false);
        HUDServer.SetActive(true);
    }
}