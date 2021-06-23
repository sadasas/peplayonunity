using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using kcp2k;

namespace PeplayonLobby
{
    [AddComponentMenu("")]
    public class DevConfigLobby : DevConfig 
    {
        public bool StartAsServerOnly = false;
        public bool startAsClientOnly = false;
        public bool startAsHost = false;
        public ushort port = 7777;
        public GameManager gameManager;
        public Room room;
        
        void Start()
        {
            if (isDevMode)
            {
                if (StartAsServerOnly)
                {
                    var net = Instantiate(networkManagerPrefab);
                    net.GetComponent<NetworkManagerTesting>().GetComponent<KcpTransport>().Port = port;
                    net.GetComponent<NetworkManagerTesting>().StartServer();
                } else if (startAsClientOnly)
                {
                    var net = Instantiate(networkManagerPrefab);
                    net.GetComponent<NetworkManagerTesting>().GetComponent<KcpTransport>().Port = port;
                    net.GetComponent<NetworkManagerTesting>().StartClient();
                } else if(startAsHost)
                {
                    var net = Instantiate(networkManagerPrefab);
                    net.GetComponent<NetworkManagerTesting>().GetComponent<KcpTransport>().Port = port;
                    net.GetComponent<NetworkManagerTesting>().StartHost();
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
