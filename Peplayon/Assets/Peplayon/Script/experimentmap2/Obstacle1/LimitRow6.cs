using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRow6 : NetworkBehaviour
{
    public static LimitRow6 instance;

    public bool isRow6Add = false, isAdded = false, limitAdded = false;

    public List<Row1> listRow6 = new List<Row1>();

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
            if (listRow6[0].trap == true && listRow6[1].trap == true && listRow6[2].trap == true && listRow6[3].trap == true)
            {
                if (isRow6Add)
                {
                    setTrue();
                }
            }
            else if (listRow6[0].trap == false && listRow6[1].trap == false && listRow6[2].trap == false && listRow6[3].trap == false)
            {
                if (isRow6Add)
                {
                    setFalse();
                }
            }
            else
            {
                if (!isAdded)
                {
                    AllowNextRow();
                }
            }
        }
    }

    [Server]
    private void setFalse()
    {
        Debug.Log("ALL ROW 6 FALSE");
        isRow6Add = false;
        BrainRow6.instance.spawnObject();
    }

    [Server]
    private void setTrue()
    {
        Debug.Log("ALL ROW 6 TRUE");
        isRow6Add = false;
        BrainRow6.instance.spawnObject();
    }

    [Server]
    private void AllowNextRow()
    {
        isAdded = true;
        BrainRow7.instance.isLimitRow7 = true;
        BrainRow7.instance.colapseRow7 = false;
    }
}