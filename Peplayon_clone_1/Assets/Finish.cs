using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : NetworkBehaviour
{
    public string[] sceneList;
    public bool colapse = false;

    /* private void Update()
     {
         if (isServer && !colapse)
         {
             Scene activeScene = SceneManager.GetActiveScene();
             if (activeScene.name == sceneList[0])
             {
                 if ()
                     colapse = true;
                 NetworkManagerTesting.instance.ServerChangeScene(NetworkManagerTesting.instance.sceneNameList[4]);
             }
         }
     }*/
}