using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using PeplayonLobby;
using System;

namespace Networking
{
    public class NetworkPlayerManager : NetworkBehaviour
    {
        [Scene] [SerializeField] string lobbyScene;
        public static NetworkPlayerManager instance;
        public SyncList<string> playerOnlineStr = new SyncList<string>();

        [SyncVar(hook = nameof(UpdateUI))]
        public int IntPlayerOnline = 0;

        public UI_Lobby uI_Lobby;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public void UpdateUI(int _oldVal, int _newVal)
        {
            Debug.Log("Update UI");
            if (SceneManager.GetActiveScene().path == lobbyScene)
            {
                uI_Lobby = GameObject.FindObjectOfType<UI_Lobby>();
                uI_Lobby.UpdatePlayerCounter(_newVal);
            }

        }

        //[ClientRpc]
        //private void RpcMethod(int _newVal)
        //{

        //}
    }
}
