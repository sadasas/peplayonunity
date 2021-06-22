using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using Networking;

namespace PeplayonLobby
{
    public class UI_Lobby : MonoBehaviour
    {
        public int MaxPlayerInt = 40;
        public string RoomIDStr = "ASD99";
        public int PlayerCounterInt;
        public bool isLeader = true;

        public TMP_Text PlayerCounter;
        public TMP_Text MaxPlayer;
        public TMP_Text RoomID;
        public GameObject StartBtn;
        public GameManager gameManager;
        public NetworkManagerTesting networkManagerTesting;

        public GameObject localPlayer;

        private void Start()
        {
            gameManager = GameObject.FindObjectOfType<GameManager>();
            updateUI();
        }

        private void Awake()
        {
        }

        [Client]
        private void updateUI()
        {
            MaxPlayer.text = MaxPlayerInt.ToString();
            RoomID.text = gameManager.DataRoom.RoomID;
            PlayerCounter.text = PlayerCounterInt.ToString();
            StartBtn.SetActive(isLeader);
        }

        public void UpdatePlayerCounter(int counter)
        {
            Debug.Log("UpdatePlayerCounter");
            PlayerCounter.text = counter.ToString();
        }

        public void StartGameBtn()
        {
            localPlayer = NetworkClient.localPlayer.gameObject;
            localPlayer.GetComponent<PlayerNetwork>().StartGameBtn();
        }
    }

}
