using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row5Obstacle3 : NetworkBehaviour
{
    public static Row5Obstacle3 instance;
    public bool trapAdded = false, obstacleAdded = false, colapse = false;

    [SerializeField]
    private List<Obstacle33> listOsbtacle = new List<Obstacle33>();

    public List<Obstacle33> listOsbtacleSelected = new List<Obstacle33>();
    public List<GameObject> spawned = new List<GameObject>();
    public Transform[] point;

    public GameObject[] ob;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    }

    [Server]
    public void spawnObject()
    {
        listOsbtacle.Clear();

        if (spawned.Count > 1)
        {
            foreach (GameObject ababa in spawned)
            {
                Destroy(ababa);
            }
        }
        spawned.Clear();
        Debug.Log("DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD");
        GameObject a = Instantiate(ob[0], point[0].position, ob[0].transform.rotation);
        GameObject b = Instantiate(ob[1], point[1].position, ob[1].transform.rotation);
        GameObject c = Instantiate(ob[2], point[2].position, ob[2].transform.rotation);
        GameObject d = Instantiate(ob[3], point[3].position, ob[3].transform.rotation);
        NetworkServer.Spawn(a);
        NetworkServer.Spawn(b);
        NetworkServer.Spawn(c);
        NetworkServer.Spawn(d);
        spawned.Add(a);
        spawned.Add(b);
        spawned.Add(c);
        spawned.Add(d);

        Obstacle33 ao = a.GetComponent<Obstacle33>();
        Obstacle33 bo = b.GetComponent<Obstacle33>();
        Obstacle33 co = c.GetComponent<Obstacle33>();
        Obstacle33 fo = d.GetComponent<Obstacle33>();

        listOsbtacle.Add(ao);
        listOsbtacle.Add(bo);
        listOsbtacle.Add(co);
        listOsbtacle.Add(fo);

        setkkk(a, b, c, d);
        obstacleAdded = true;
    }

    [ClientRpc]
    private void setkkk(GameObject a, GameObject b, GameObject c, GameObject d)

    {
        listOsbtacle.Clear();

        Debug.Log("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
        Obstacle33 ao = a.GetComponent<Obstacle33>();
        Obstacle33 bo = b.GetComponent<Obstacle33>();
        Obstacle33 co = c.GetComponent<Obstacle33>();
        Obstacle33 fo = d.GetComponent<Obstacle33>();

        listOsbtacle.Add(ao);
        listOsbtacle.Add(bo);
        listOsbtacle.Add(co);
        listOsbtacle.Add(fo);
    }

    private void Update()
    {
        if (isServer && !colapse)
        {
            colapse = true;
            spawnObject();
        }
        if (trapAdded && isServer && obstacleAdded)
        {
            trapAdded = false;
            for (int i = 0; i <= 3; i++)
            {
                ResetTrap(i);
                listOsbtacle[i].trap = false;
            }
            for (int i = 0; i <= listOsbtacleSelected.Count - 1; i++)
            {
                if (listOsbtacleSelected[i].index == 0)
                {
                    SetTrapClient(0);
                    listOsbtacle[0].trap = true;
                    listOsbtacle[0].colapse4 = false;
                }
                else if (listOsbtacleSelected[i].index == 1)
                {
                    SetTrapClient(1);

                    listOsbtacle[1].trap = true;
                    listOsbtacle[0].colapse4 = false;
                }
                else if (listOsbtacleSelected[i].index == 2)
                {
                    SetTrapClient(2);

                    listOsbtacle[2].trap = true;
                    listOsbtacle[0].colapse4 = false;
                }
                else if (listOsbtacleSelected[i].index == 3)
                {
                    SetTrapClient(3);

                    listOsbtacle[3].trap = true;
                    listOsbtacle[0].colapse4 = false;
                }
            }
        }
    }

    [ClientRpc]
    private void ResetTrap(int i)
    {
        listOsbtacle[i].trap = false;
    }

    [ClientRpc]
    private void SetTrapClient(int i)

    {
        listOsbtacle[i].trap = true;
        listOsbtacle[i].colapse2 = false;
    }
}