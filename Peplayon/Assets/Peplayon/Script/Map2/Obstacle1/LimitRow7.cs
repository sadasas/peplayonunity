using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRow7 : NetworkBehaviour
{
    public static LimitRow7 instance;

    public bool isRow7Add = false, isAdded = false, limitAdded = false;

    public List<Row1> listRow7 = new List<Row1>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (isServer && limitAdded)
        {
            if (listRow7[0].trap == true && listRow7[1].trap == true && listRow7[2].trap == true && listRow7[3].trap == true)
            {
                if (isRow7Add)
                {
                    setTrue();
                }
            }
            else if (listRow7[0].trap == false && listRow7[1].trap == false && listRow7[2].trap == false && listRow7[3].trap == false)
            {
                if (isRow7Add)
                {
                    setFalse();
                }
            }
            else
            {
                if (!isAdded)
                {
                    /* isAdded = true;
                     BrainRow6.instance.isLimitRow6 = true;
                     BrainRow6.instance.colapseRow6 = false;*/
                }
            }
        }
    }

    [Server]
    private void setFalse()
    {
        Debug.Log("ALL ROW 7 FALSE");
        isRow7Add = false;
        BrainRow7.instance.spawnObject();
    }

    [Server]
    private void setTrue()
    {
        Debug.Log("ALL ROW 7 TRUE");
        isRow7Add = false;
        BrainRow7.instance.spawnObject();
    }

    [Server]
    private void AllowNextRow()
    {
        isAdded = true;
        BrainRow7.instance.isLimitRow7 = true;
        BrainRow7.instance.colapseRow7 = false;
    }
}