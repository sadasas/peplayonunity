using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[System.Serializable]
public struct PlayerShared
{
    public string PlayerID;
    public string Name;
    public string UserName;
    public bool isLead;
    public int Level;
}
namespace Networking
{
    [System.Serializable]
    public class PlayerNetwork : NetworkBehaviour
    {
        // ADA yang lebih simple
        //[SyncVar] [SerializeField] string PlayerID;
        //[SyncVar] [SerializeField] string UserName;
        //[SyncVar] [SerializeField] string Name;
        //[SyncVar] [SerializeField] bool isLead;
        //[SyncVar] [SerializeField] int Level;
        [SyncVar] public PlayerShared SyncPlayerShared;

        public GameManager gameManager;
        public NetworkPlayerManager networkPlayerManager;

        private void Start()
        {
            if (!isLocalPlayer) return;

            gameManager = GameObject.FindObjectOfType<GameManager>();

            PlayerShared playerShared;
            playerShared.PlayerID = gameManager.ID;
            playerShared.UserName = gameManager.UserName;
            playerShared.Name = gameManager.Name;
            playerShared.Level = gameManager.Level;
            playerShared.isLead = gameManager.DataRoom.IsLeadPlayer;

            CmdPlayerSetUp(playerShared);
        }

        [Command]
        public void CmdPlayerSetUp(PlayerShared playerShared)
        {
            //Debug.Log("cmdd");
            SyncPlayerShared = playerShared;
            ServerPlayerSetUp(playerShared);
            networkPlayerManager = GameObject.FindObjectOfType<NetworkPlayerManager>();
            networkPlayerManager.playerOnlineStr.Add(playerShared.UserName);
        }

        [Server]
        public void ServerPlayerSetUp(PlayerShared playerShared)
        {
            //NetworkPlayerManager.instance.playerOnlineStr.Add(playerShared.UserName);
        }

        [Command]
        public void StartGameBtn()
        {
            NetworkManagerTesting networkManagerTesting = GameObject.FindObjectOfType<NetworkManagerTesting>();
            networkManagerTesting.StartGame();
        }

    }
}
