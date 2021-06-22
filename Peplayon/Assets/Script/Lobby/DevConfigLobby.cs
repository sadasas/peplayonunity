using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeplayonLobby
{
    [AddComponentMenu("")]
    public class DevConfigLobby : DevConfig
    {
        public GameManager gameManager;
        public Room room;

        void Start()
        {
            if (isDevMode)
            {
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
