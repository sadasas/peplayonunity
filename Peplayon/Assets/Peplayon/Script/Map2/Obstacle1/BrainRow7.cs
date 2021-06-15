using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainRow7 : NetworkBehaviour
{
    public static BrainRow7 instance;

    public List<int> indexListTrapRow7 = new List<int>();

    public List<Row1> row7 = new List<Row1>();

    public List<Row1> row7Selected = new List<Row1>();

    public bool colapseRow7 = false;
    public bool isLimitRow7 = false;

    public Transform[] point;

    public GameObject[] ob;
    public List<GameObject> spawned = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!colapseRow7 && isLimitRow7 && isServer)
        {
            spawnObject();
        }
    }

    [Server]
    public void spawnObject()
    {
        row7.Clear();
        row7Selected.Clear();
        LimitRow7.instance.listRow7.Clear();
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
        Row1 ao = a.GetComponent<Row1>();
        Row1 bo = b.GetComponent<Row1>();
        Row1 co = c.GetComponent<Row1>();
        Row1 fo = d.GetComponent<Row1>();

        row7.Add(ao);
        row7.Add(bo);
        row7.Add(co);
        row7.Add(fo);
        LimitRow7.instance.listRow7.Add(ao);
        LimitRow7.instance.listRow7.Add(bo);
        LimitRow7.instance.listRow7.Add(co);
        LimitRow7.instance.listRow7.Add(fo);
        setkkk(a, b, c, d);
        SetSelectedRow7();
        LimitRow7.instance.limitAdded = true;
    }

    [ClientRpc]
    private void setkkk(GameObject a, GameObject b, GameObject c, GameObject d)

    {
        row7.Clear();
        Debug.Log("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
        Row1 ao = a.GetComponent<Row1>();
        Row1 bo = b.GetComponent<Row1>();
        Row1 co = c.GetComponent<Row1>();
        Row1 fo = d.GetComponent<Row1>();

        row7.Add(ao);
        row7.Add(bo);
        row7.Add(co);
        row7.Add(fo);
    }

    #region ROW 6

    [Server]
    private void SetSelectedRow7()
    {
        int countListTrap = indexListTrapRow7.Count - 1;
        for (int i = 0; i <= countListTrap; i++)
        {
            colapseRow7 = true;
            int a = indexListTrapRow7[i];

            row7Selected.Add(row7[a]);
            if (indexListTrapRow7[i] == 0)
            {
                row7Selected.Add(row7[indexListTrapRow7[i] + 1]);
            }
            else if (indexListTrapRow7[i] == 1)
            {
                row7Selected.Add(row7[indexListTrapRow7[i] - 1]);
                row7Selected.Add(row7[indexListTrapRow7[i] + 1]);
            }
            else if (indexListTrapRow7[i] == 2)
            {
                row7Selected.Add(row7[indexListTrapRow7[i] - 1]);
                row7Selected.Add(row7[indexListTrapRow7[i] + 1]);
            }
            else if (indexListTrapRow7[i] == 3)
            {
                row7Selected.Add(row7[indexListTrapRow7[i] - 1]);
            }
        }
        SetRow7();
    }

    [Server]
    public void SetRow7()
    {
        for (int i = 0; i <= 3; i++)
        {
            BoxCollider obs = row7[i].gameObject.GetComponent<BoxCollider>();
            obs.isTrigger = true;
            row7[i].trap = false;
            ResetTrap(i);
        }
        LimitRow7.instance.isRow7Add = true;
        int countRowSelected = row7Selected.Count - 1;
        for (int i = 0; i <= countRowSelected; i++)
        {
            BoxCollider obs = row7Selected[i].gameObject.GetComponent<BoxCollider>();
            int randomvalue = UnityEngine.Random.Range(0, 2);
            if (randomvalue == 0)
            {
                SetTrapClient(row7Selected[i].index);
                row7Selected[i].trap = true;
                row7Selected[i].colapse4 = false;
                obs.isTrigger = false;
            }
            else
            {
                SetNonTrapClient(row7Selected[i].index);
                row7Selected[i].trap = false;
                row7Selected[i].colapse4 = true;
                obs.isTrigger = true;
            }
        }
    }

    #endregion ROW 6

    [ClientRpc]
    private void ResetTrap(int i)
    {
        BoxCollider obs = row7[i].gameObject.GetComponent<BoxCollider>();
        obs.isTrigger = true;
        row7[i].trap = false;
    }

    [ClientRpc]
    private void SetTrapClient(int i)
    {
        BoxCollider obs = row7[i].gameObject.GetComponent<BoxCollider>();
        row7[i].trap = true;
        row7[i].colapse = false;

        obs.isTrigger = false;
    }

    [ClientRpc]
    private void SetNonTrapClient(int i)
    {
        BoxCollider obs = row7[i].gameObject.GetComponent<BoxCollider>();
        row7[i].trap = false;

        obs.isTrigger = true;
    }
}