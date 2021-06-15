using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRow1Obstacle3 : NetworkBehaviour
{
    public static LimitRow1Obstacle3 instance;

    public List<Obstacle33> listOsbtacle = new List<Obstacle33>();

    public bool trapAdded = false, colapse = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (isServer && trapAdded)
        {
            if (listOsbtacle[0].trap == true && listOsbtacle[1].trap == true && listOsbtacle[2].trap == true && listOsbtacle[3].trap == true)
            {
                if (trapAdded)
                {
                    setTrue();
                }
            }
            else if (listOsbtacle[0].trap == false && listOsbtacle[1].trap == false && listOsbtacle[2].trap == false && listOsbtacle[3].trap == false)
            {
                if (trapAdded)
                {
                    setFalse();
                }
            }
            else
            {
                if (!colapse)
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
        trapAdded = false;
        Debug.Log("TRAP ROW I OBSTACLE 3 ALL FALSE");
        Obstacle3map2.instance.spawnObject();
    }

    [Server]
    private void setTrue()
    {
        trapAdded = false;
        Debug.Log("TRAP ROW I OBSTACLE 3 ALL TRUE");
        Obstacle3map2.instance.spawnObject();
    }

    [Server]
    private void AllowNextRow()
    {
        colapse = true;
        Row2Obstacle3.instance.trapAdded = true;
    }
}