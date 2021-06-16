using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Obstaclemap2 : NetworkBehaviour
{
    public static Obstaclemap2 instance;

    private bool colapse = false, start = false;

    public int randomvalue;

    [SerializeField]
    private List<Obstacle3> one = new List<Obstacle3>();

    public Transform[] point;

    public GameObject[] ob;
    public List<GameObject> spawned = new List<GameObject>();

    #region MonoBehaviour

    private void Awake()
    {
        Obstaclemap2.instance = this;
    }

    private void Update()
    {
        if (!colapse && isServer)
        {
            colapse = true;
            spawnObject();
        }
    }

    #endregion MonoBehaviour

    [Server]
    public void spawnObject()
    {
        Brain.instance.row1.Clear();
        one.Clear();
        Limit.instance.listStart.Clear();
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

        spawned.Add(a);
        spawned.Add(b);
        spawned.Add(c);
        spawned.Add(d);

        NetworkServer.Spawn(a);
        NetworkServer.Spawn(b);
        NetworkServer.Spawn(c);
        NetworkServer.Spawn(d);

        Obstacle3 ao = a.GetComponent<Obstacle3>();
        Obstacle3 bo = b.GetComponent<Obstacle3>();
        Obstacle3 co = c.GetComponent<Obstacle3>();
        Obstacle3 fo = d.GetComponent<Obstacle3>();

        one.Add(ao);
        one.Add(bo);
        one.Add(co);
        one.Add(fo);
        Limit.instance.listStart.Add(ao);
        Limit.instance.listStart.Add(bo);
        Limit.instance.listStart.Add(co);
        Limit.instance.listStart.Add(fo);
        setkkk(a, b, c, d);
        SetTrap();
    }

    [ClientRpc]
    private void setkkk(GameObject a, GameObject b, GameObject c, GameObject d)

    {
        one.Clear();
        Debug.Log("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
        Obstacle3 ao = a.GetComponent<Obstacle3>();
        Obstacle3 bo = b.GetComponent<Obstacle3>();
        Obstacle3 co = c.GetComponent<Obstacle3>();
        Obstacle3 fo = d.GetComponent<Obstacle3>();
        one.Add(ao);
        one.Add(bo);
        one.Add(co);
        one.Add(fo);
    }

    [Server]
    public void SetTrap()
    {
        Brain.instance.row1Selected.Clear();
        Brain.instance.indexListTrap.Clear();

        for (int i = 0; i <= 3; i++)
        {
            BoxCollider obs = one[i].gameObject.GetComponent<BoxCollider>();
            obs.isTrigger = true;
            one[i].trap = false;
            ResetTrap(i);
        }
        Debug.Log("TEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEASSSSSSSSSSSSSSSSSSSSSSSSS1");
        Brain.instance.colapse = false;
        Limit.instance.isRandomRangeAdd = true;

        for (int i = 0; i < one.Count; i++)
        {
            BoxCollider obs = one[i].gameObject.GetComponent<BoxCollider>();

            randomvalue = UnityEngine.Random.Range(0, 2);
            if (randomvalue == 0)
            {
                Brain.instance.indexListTrap.Add(one[i].index);

                one[i].trap = true;
                one[i].colapse = false;
                SetTrapClient(i);
                one[i].indexlist = i;
                obs.isTrigger = false;
            }
            else
            {
                SetNonTrapClient(i);
                one[i].trap = false;
                one[i].indexlist = i;
                obs.isTrigger = true;
            }
        }
    }

    [ClientRpc]
    private void ResetTrap(int i)
    {
        BoxCollider obs = one[i].gameObject.GetComponent<BoxCollider>();
        obs.isTrigger = true;
        one[i].trap = false;
    }

    [ClientRpc]
    private void SetTrapClient(int i)
    {
        Debug.Log("Ssss");
        BoxCollider obs = one[i].gameObject.GetComponent<BoxCollider>();
        one[i].trap = true;
        one[i].colapse = false;

        one[i].indexlist = i;
        obs.isTrigger = false;
    }

    [ClientRpc]
    private void SetNonTrapClient(int i)
    {
        BoxCollider obs = one[i].gameObject.GetComponent<BoxCollider>();
        one[i].trap = false;
        one[i].indexlist = i;
        obs.isTrigger = true;
    }
}