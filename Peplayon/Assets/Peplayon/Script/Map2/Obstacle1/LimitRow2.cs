using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRow2 : NetworkBehaviour
{
    public static LimitRow2 instance;

    public bool isRow2Add = false, isAdded = false, limitAdded = false;

    public List<Row1> listRow2 = new List<Row1>();

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
            if (listRow2[0].trap == true && listRow2[1].trap == true && listRow2[2].trap == true && listRow2[3].trap == true)
            {
                if (isRow2Add == true)
                {
                    setTrue();
                }
            }
            else if (listRow2[0].trap == false && listRow2[1].trap == false && listRow2[2].trap == false && listRow2[3].trap == false)
            {
                if (isRow2Add == true)
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
        Debug.Log("ALL ROW 2 FALSE");
        isRow2Add = false;
        BrainRow2.instance.spawnObject();
    }

    [Server]
    private void setTrue()
    {
        Debug.Log("ALL ROW 2 TRUE");
        isRow2Add = false;
        BrainRow2.instance.spawnObject();
    }

    [Server]
    private void AllowNextRow()
    {
        isAdded = true;
        BrainRow3.instance.isLimitRow3 = true;
        BrainRow3.instance.colapseRow3 = false;
    }
}