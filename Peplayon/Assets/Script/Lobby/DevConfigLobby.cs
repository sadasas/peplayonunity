using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeplayonLobby
{
    [AddComponentMenu("")]
    public class DevConfigLobby : DevConfig 
    {
        public bool StartAsServerOnly = false;
        public GameManager gameManager;
        public Room room;
        
        void Start()
        {
            if (isDevMode)
            {
                if (StartAsServerOnly)
                {
                    var net = Instantiate(networkManagerPrefab);
                    net.GetComponent<NetworkManagerTesting>().StartServer();
                }
                GameObject go = Instantiate(gameManagerPrefab);
                gameManager = go.GetComponent<GameManager>();
                gameManager.DataRoom = room;
            }
            else
            {
                gameManager = GameObject.FindObjectOfType<GameManager>();
                room = gameManager.DataRoom;
            }

        }
    }
}
