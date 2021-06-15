using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainRow6 : NetworkBehaviour
{
    public static BrainRow6 instance;

    public List<int> indexListTrapRow6 = new List<int>();

    public List<Row1> row6 = new List<Row1>();

    public List<Row1> row6Selected = new List<Row1>();

    public bool colapseRow6 = false;
    public bool isLimitRow6 = false;
    public Transform[] point;

    public GameObject[] ob;
    public List<GameObject> spawned = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!colapseRow6 && isLimitRow6 && isServer)
        {
            spawnObject();
        }
    }

    [Server]
    public void spawnObject()
    {
        row6.Clear();
        row6Selected.Clear();
        LimitRow6.instance.listRow6.Clear();
        BrainRow7.instance.row7.Clear();
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

        row6.Add(ao);
        row6.Add(bo);
        row6.Add(co);
        row6.Add(fo);
        LimitRow6.instance.listRow6.Add(ao);
        LimitRow6.instance.listRow6.Add(bo);
        LimitRow6.instance.listRow6.Add(co);
        LimitRow6.instance.listRow6.Add(fo);
        setkkk(a, b, c, d);
        SetSelectedRow6();
        LimitRow6.instance.limitAdded = true; ;
    }

    [ClientRpc]
    private void setkkk(GameObject a, GameObject b, GameObject c, GameObject d)

    {
        row6.Clear();
        Debug.Log("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
        Row1 ao = a.GetComponent<Row1>();
        Row1 bo = b.GetComponent<Row1>();
        Row1 co = c.GetComponent<Row1>();
        Row1 fo = d.GetComponent<Row1>();

        row6.Add(ao);
        row6.Add(bo);
        row6.Add(co);
        row6.Add(fo);
    }

    #region ROW 6

    [Server]
    private void SetSelectedRow6()
    {
        int countListTrap = indexListTrapRow6.Count - 1;
        for (int i = 0; i <= countListTrap; i++)
        {
            colapseRow6 = true;
            int a = indexListTrapRow6[i];

            row6Selected.Add(row6[a]);
            if (indexListTrapRow6[i] == 0)
            {
                row6Selected.Add(row6[indexListTrapRow6[i] + 1]);
            }
            else if (indexListTrapRow6[i] == 1)
            {
                row6Selected.Add(row6[indexListTrapRow6[i] - 1]);
                row6Selected.Add(row6[indexListTrapRow6[i] + 1]);
            }
            else if (indexListTrapRow6[i] == 2)
            {
                row6Selected.Add(row6[indexListTrapRow6[i] - 1]);
                row6Selected.Add(row6[indexListTrapRow6[i] + 1]);
            }
            else if (indexListTrapRow6[i] == 3)
            {
                row6Selected.Add(row6[indexListTrapRow6[i] - 1]);
            }
        }
        SetRow6();
    }

    [Server]
    public void SetRow6()
    {
        for (int i = 0; i <= 3; i++)
        {
            ResetTrap(i);
            BoxCollider obs = row6[i].gameObject.GetComponent<BoxCollider>();
            obs.isTrigger = true;
            row6[i].trap = false;
        }
        BrainRow7.instance.indexListTrapRow7.Clear();
        BrainRow7.instance.row7Selected.Clear();
        LimitRow6.instance.isRow6Add = true;
        int countRowSelected = row6Selected.Count - 1;
        for (int i = 0; i <= countRowSelected; i++)
        {
            BoxCollider obs = row6Selected[i].gameObject.GetComponent<BoxCollider>();
            int randomvalue = UnityEngine.Random.Range(0, 2);
            if (randomvalue == 0)
            {
                SetTrapClient(row6Selected[i].index);
                row6Selected[i].trap = true;
                row6Selected[i].colapse6 = false;
                obs.isTrigger = false;
            }
            else
            {
                SetNonTrapClient(row6Selected[i].index);
                row6Selected[i].trap = false;
                row6Selected[i].colapse6 = true;
                obs.isTrigger = true;
            }
        }
        LimitRow6.instance.isAdded = false;

        for (int i = 0; i <= 3; i++)
        {
            if (row6[i].trap == true)
            {
                BrainRow7.instance.indexListTrapRow7.Add(row6[i].index);
            }
        }
    }

    #endregion ROW 6

    [ClientRpc]
    private void ResetTrap(int i)
    {
        BoxCollider obs = row6[i].gameObject.GetComponent<BoxCollider>();
        obs.isTrigger = true;
        row6[i].trap = false;
    }

    [ClientRpc]
    private void SetTrapClient(int i)
    {
        BoxCollider obs = row6[i].gameObject.GetComponent<BoxCollider>();
        row6[i].trap = true;
        row6[i].colapse = false;

        obs.isTrigger = false;
    }

    [ClientRpc]
    private void SetNonTrapClient(int i)
    {
        BoxCollider obs = row6[i].gameObject.GetComponent<BoxCollider>();
        row6[i].trap = false;

        obs.isTrigger = true;
    }
}