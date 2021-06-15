using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRow4 : NetworkBehaviour
{
    public static LimitRow4 instance;

    public bool isRow4Add = false, isAdded = false, limitAdded = false;

    public List<Row1> listRow4 = new List<Row1>();

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
            if (listRow4[0].trap == true && listRow4[1].trap == true && listRow4[2].trap == true && listRow4[3].trap == true)
            {
                if (isRow4Add)
                {
                    setTrue();
                }
            }
            else if (listRow4[0].trap == false && listRow4[1].trap == false && listRow4[2].trap == false && listRow4[3].trap == false)
            {
                if (isRow4Add)
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
        Debug.Log("ALL ROW 4 FALSE");
        isRow4Add = false;
        BrainRow4.instance.spawnObject();
    }

    [Server]
    private void setTrue()
    {
        Debug.Log("ALL ROW 4 TRUE");
        isRow4Add = false;
        BrainRow4.instance.spawnObject();
    }

    [Server]
    private void AllowNextRow()
    {
        isAdded = true;
        BrainRow5.instance.isLimitRow5 = true;
        BrainRow5.instance.colapseRow5 = false;
    }
}