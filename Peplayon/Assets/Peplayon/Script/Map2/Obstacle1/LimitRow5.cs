using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRow5 : NetworkBehaviour
{
    public static LimitRow5 instance;

    public bool isRow5Add = false, isAdded = false, limitAdded = false;

    public List<Row1> listRow5 = new List<Row1>();

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
            if (listRow5[0].trap == true && listRow5[1].trap == true && listRow5[2].trap == true && listRow5[3].trap == true)
            {
                if (isRow5Add)
                {
                    setTrue();
                }
            }
            else if (listRow5[0].trap == false && listRow5[1].trap == false && listRow5[2].trap == false && listRow5[3].trap == false)
            {
                if (isRow5Add)
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
        Debug.Log("ALL ROW 5 FALSE");
        isRow5Add = false;
        BrainRow4.instance.spawnObject();
    }

    [Server]
    private void setTrue()
    {
        Debug.Log("ALL ROW 5 TRUE");
        isRow5Add = false;
        BrainRow4.instance.spawnObject();
    }

    [Server]
    private void AllowNextRow()
    {
        isAdded = true;
        BrainRow6.instance.isLimitRow6 = true;
        BrainRow6.instance.colapseRow6 = false;
    }
}