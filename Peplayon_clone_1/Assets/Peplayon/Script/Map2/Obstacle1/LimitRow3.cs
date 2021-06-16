using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRow3 : NetworkBehaviour
{
    public static LimitRow3 instance;

    public bool isRow3Add = false, isAdded = false, limitAdded = false;

    public List<Row1> listRow3 = new List<Row1>();

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
            if (listRow3[0].trap == true && listRow3[1].trap == true && listRow3[2].trap == true && listRow3[3].trap == true)
            {
                if (isRow3Add == true)
                {
                    setTrue();
                }
            }
            else if (listRow3[0].trap == false && listRow3[1].trap == false && listRow3[2].trap == false && listRow3[3].trap == false)
            {
                if (isRow3Add == true)
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
        Debug.Log("ALL ROW 3 FALSE");
        isRow3Add = false;
        BrainRow3.instance.spawnObject();
    }

    [Server]
    private void setTrue()
    {
        Debug.Log("ALL ROW 3 TRUE");
        isRow3Add = false;
        BrainRow3.instance.spawnObject();
    }

    [Server]
    private void AllowNextRow()
    {
        isAdded = true;
        BrainRow4.instance.isLimitRow4 = true;
        BrainRow4.instance.colapseRow4 = false;
    }
}