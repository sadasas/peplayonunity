using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.SceneManagement;
using System.Net.Http;
using TMPro;

namespace Matchmaker
{
    [RequireComponent(typeof(UI_MatchMaking))]
    public class UI_MatchMaking : MonoBehaviour
    {

        [Header("UI State")]
        public bool isContainerActive = false;
        public bool isLoading = false;
        public GameObject GameMode;
        public GameObject CreateGame;
        public GameObject JoinGame;
        public GameObject Loading;

        [SerializeField] GameObject MatchmakingUIContainer;
        [SerializeField] MatchmakerServices matchmakerServices;

        public TMP_InputField JoinField;
        //[Header("RoomMode")]
        //[SerializeField] Button CreateRoomBtn;
        //[SerializeField] Button JoinRoomBtn;
        //[SerializeField] Button QuickMatchBtn;

        //[Header("Create Room")]
        //[SerializeField] Button publicBtn;
        //[SerializeField] Button privateBtn;

        //[Header("Quick Match")]
        //[SerializeField]

        //[Header("Button")]
        //[SerializeField] Button JoinBtn;
        //[SerializeField] Button AutoBtn;
        //[SerializeField] TMP_InputField JoinField;

        [Header("Lobby Scene")]
        [Scene] [SerializeField] string LobbyScene;

        [Header("Matchmaker")]
        [SerializeField] string baseURL = "http://localhost:3000/matchmaker";
        HttpClient client = new HttpClient();

        public NetworkManagerTesting networkManagerTesting;


        private void Awake()
        {
            
        }

        public void StartContainer()
        {
            MatchmakingUIContainer.SetActive(true);
            GameMode.SetActive(true);
        }

        public void StartCancel()
        {
            ClearInstance();
            MatchmakingUIContainer.SetActive(false);
        }

        #region ROOM MODE
        public void CreateRoom()
        {
            GameMode.SetActive(false);
            CreateGame.SetActive(true);
        }

        public void JoinRoom()
        {
            GameMode.SetActive(false);
            JoinGame.SetActive(true);
        }

        public void QuickMatch()
        {
            LoadingInit();
            Debug.Log($"Start Auto");
            matchmakerServices.ReqMatchPrivate(baseURL);
        }
        #endregion

        #region CREATE ROOM
        public void CR_Back()
        {
            CreateGame.SetActive(false);
            GameMode.SetActive(true);
        }

        public void CR_Public()
        {
            Debug.Log($"Start Public");
            LoadingInit();

            if (DevConfigMainMenu.instance.isDevMode)
            {
                if (DevConfigMainMenu.instance.SetAsServer)
                {
                    networkManagerTesting.StartHost();
                }
                else
                {
                    networkManagerTesting.StartClient();

                }
            }
            else
            {
                Room result = matchmakerServices.ReqMatchPublic(baseURL);
                GameManager.Instance.DataRoom = result;
            }
        }

        public void CR_Private()
        {
            Debug.Log($"Start Private");
            LoadingInit();
            Room result = matchmakerServices.ReqMatchPrivate(baseURL);
            GameManager.Instance.DataRoom = result;
        }
        #endregion

        #region JOIN ROOM
        public void JR_Back()
        {
            JoinGame.SetActive(false);
            GameMode.SetActive(true);
        }
        public void JR_JoinRoom()
        {
            Debug.Log($"{JoinField.text}");
            Debug.Log($"Start Join");
            matchmakerServices.ReqMatchJoin(baseURL, JoinField.text);
        }
        #endregion

        public void LoadingInit()
        {
            ClearInstance();
            Loading.SetActive(true);
        }

        private void ClearInstance()
        {
            GameMode.SetActive(false);
            CreateGame.SetActive(false);
            JoinGame.SetActive(false);
        }

        public void LoadGame(Room _room)
        {
            SceneManager.LoadSceneAsync(LobbyScene, LoadSceneMode.Single);
        }
    }
}