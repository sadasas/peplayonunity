using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRow3Obstacle3 : NetworkBehaviour
{
    public static LimitRow3Obstacle3 instance;

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
                    colapse = true;
                    AllowNextRow();
                }
            }
        }
    }

    [Server]
    private void setFalse()
    {
        trapAdded = false;
        Debug.Log("TRAP ROW 3 OBSTACLE 3 ALL FALSE");
        Row3Obstacle3.instance.SetRow3();
    }

    [Server]
    private void setTrue()
    {
        trapAdded = false;
        Debug.Log("TRAP ROW 1 OBSTACLE 3 ALL TRUE");
        Row3Obstacle3.instance.SetRow3();
    }

    [Server]
    private void AllowNextRow()
    {
        colapse = true;
        Row4Obstacle3.instance.trapAdded = true;
    }
}