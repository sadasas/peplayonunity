using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : NetworkBehaviour
{
    public static Limit instance;
    public bool isRandomRangeAdd = false;

    public List<Obstacle3> listStart = new List<Obstacle3>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (isServer && isRandomRangeAdd)
        {
            if (listStart[0].trap == false && listStart[1].trap == false && listStart[2].trap == false && listStart[3].trap == false)
            {
                if (isRandomRangeAdd == true)
                {
                    setFalse();
                }
            }
            else if (listStart[0].trap == true && listStart[1].trap == true && listStart[2].trap == true && listStart[3].trap == true)
            {
                if (isRandomRangeAdd == true)
                {
                    setTrue();
                }
            }
            else
            {
                AllowNextRow();
            }
        }
    }

    [Server]
    private void setFalse()
    {
        Debug.Log("ALL false");
        isRandomRangeAdd = false;
        Obstaclemap2.instance.spawnObject();
    }

    [Server]
    private void setTrue()
    {
        Debug.Log("ALL True");
        isRandomRangeAdd = false;
        Obstaclemap2.instance.spawnObject();
    }

    [Server]
    private void AllowNextRow()
    {
        Brain.instance.isLimit = true;
    }
}