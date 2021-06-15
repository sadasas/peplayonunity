using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LimitRow1 : NetworkBehaviour
{
    public static LimitRow1 instance;
    public bool isRow1Add = false, isAdded = false, limitAdded = false;

    public List<Row1> listRow1 = new List<Row1>();

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
            if (listRow1[0].trap == true && listRow1[1] == true && listRow1[2] == true && listRow1[3] == true)
            {
                if (isRow1Add == true)
                {
                    setTrue();
                }
            }
            else if (listRow1[0].trap == false && listRow1[1].trap == false && listRow1[2].trap == false && listRow1[3].trap == false)
            {
                if (isRow1Add == true)
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
        Debug.Log("ALL ROW 1 FALSE");
        isRow1Add = false;
        Brain.instance.spawnObject();
    }

    [Server]
    private void setTrue()
    {
        Debug.Log("ALL ROW 1 TRUE");
        isRow1Add = false;
        Brain.instance.spawnObject();
    }

    [Server]
    private void AllowNextRow()
    {
        isAdded = true;
        BrainRow2.instance.isLimitRow2 = true;
        BrainRow2.instance.colapseRow2 = false;
    }
}