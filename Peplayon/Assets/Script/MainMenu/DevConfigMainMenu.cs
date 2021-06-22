using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Peplayon_MainMenu;

[AddComponentMenu("")]
public class DevConfigMainMenu : DevConfig
{
    public static DevConfigMainMenu instance;
    // Start is called before the first frame update
    public GameManager gameManager;
    public UI_MainMenu uI_MainMenu;
    public bool SetAsServer = false;

    [Header("Custom Player")]
    public string Name;
    public string ID;
    public string UserName;
    public int Level;

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
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (isDevMode)
        {
            if (GameObject.FindObjectOfType<GameManager>()) return;

            Instantiate(gameManagerPrefab);
            gameManager = gameManagerPrefab.GetComponent<GameManager>();
            gameManager.ID = ID;
            gameManager.Name = Name;
            gameManager.UserName = UserName;
            gameManager.Level = Level;
            uI_MainMenu.SetUp();
        }   
    }
}
