using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Peplayon_MainMenu
{
    public class UI_MainMenu : MonoBehaviour
    {
        public TMP_Text Name;
        public TMP_Text Level;
        public GameManager gameManager;

        public void SetUp()
        {
            gameManager = GameObject.FindObjectOfType<GameManager>();
            Name.text = gameManager.Name;
            Level.text = gameManager.Level.ToString();
        }

    }
}
