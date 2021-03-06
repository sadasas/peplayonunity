using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] public string UserName;
    [SerializeField] public string Name;
    [SerializeField] public string ID;
    [SerializeField] public int Level;
    [SerializeField] public Room DataRoom;

    // Set Online Player
    public string NetID;

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

    public static GameManager Instance
    {
        get => instance;
    }
}
