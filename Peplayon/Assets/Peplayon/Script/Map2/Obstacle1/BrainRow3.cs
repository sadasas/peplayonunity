using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainRow3 : NetworkBehaviour
{
    public static BrainRow3 instance;

    public List<int> indexListTrapRow3 = new List<int>();

    public List<Row1> row3 = new List<Row1>();

    public List<Row1> row3Selected = new List<Row1>();

    public bool colapseRow3 = false;
    public bool isLimitRow3 = false;

    public Transform[] point;

    public GameObject[] ob;
    public List<GameObject> spawned = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!colapseRow3 && isLimitRow3 && isServer)
        {
            spawnObject();
        }
    }

    [Server]
    public void spawnObject()
    {
        row3.Clear();
        row3Selected.Clear();
        BrainRow4.instance.row4.Clear();
        LimitRow3.instance.listRow3.Clear();
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

        row3.Add(ao);
        row3.Add(bo);
        row3.Add(co);
        row3.Add(fo);
        LimitRow3.instance.listRow3.Add(ao);
        LimitRow3.instance.listRow3.Add(bo);
        LimitRow3.instance.listRow3.Add(co);
        LimitRow3.instance.listRow3.Add(fo);
        setkkk(a, b, c, d);
        SetSelectedRow3();
        LimitRow3.instance.limitAdded = true;
    }

    [ClientRpc]
    private void setkkk(GameObject a, GameObject b, GameObject c, GameObject d)

    {
        row3.Clear();
        Debug.Log("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
        Row1 ao = a.GetComponent<Row1>();
        Row1 bo = b.GetComponent<Row1>();
        Row1 co = c.GetComponent<Row1>();
        Row1 fo = d.GetComponent<Row1>();

        row3.Add(ao);
        row3.Add(bo);
        row3.Add(co);
        row3.Add(fo);
    }

    #region ROW 3

    [Server]
    private void SetSelectedRow3()
    {
        int countListTrap = indexListTrapRow3.Count - 1;
        for (int i = 0; i <= countListTrap; i++)
        {
            colapseRow3 = true;
            int a = indexListTrapRow3[i];

            row3Selected.Add(row3[a]);
            if (indexListTrapRow3[i] == 0)
            {
                row3Selected.Add(row3[indexListTrapRow3[i] + 1]);
            }
            else if (indexListTrapRow3[i] == 1)
            {
                row3Selected.Add(row3[indexListTrapRow3[i] - 1]);
                row3Selected.Add(row3[indexListTrapRow3[i] + 1]);
            }
            else if (indexListTrapRow3[i] == 2)
            {
                row3Selected.Add(row3[indexListTrapRow3[i] - 1]);
                row3Selected.Add(row3[indexListTrapRow3[i] + 1]);
            }
            else if (indexListTrapRow3[i] == 3)
            {
                row3Selected.Add(row3[indexListTrapRow3[i] - 1]);
            }
        }
        SetRow3();
    }

    [Server]
    public void SetRow3()
    {
        for (int i = 0; i <= 3; i++)
        {
            BoxCollider obs = row3[i].gameObject.GetComponent<BoxCollider>();
            obs.isTrigger = true;
            row3[i].trap = false;
            ResetTrap(i);
        }
        BrainRow4.instance.indexListTrapRow4.Clear();
        BrainRow4.instance.row4Selected.Clear();
        LimitRow3.instance.isRow3Add = true;

        int countRowSelected = row3Selected.Count - 1;
        for (int i = 0; i <= countRowSelected; i++)
        {
            BoxCollider obs = row3Selected[i].gameObject.GetComponent<BoxCollider>();
            int randomvalue = UnityEngine.Random.Range(0, 2);
            if (randomvalue == 0)
            {
                SetTrapClient(row3Selected[i].index);
                row3Selected[i].trap = true;
                row3Selected[i].colapse3 = false;
                obs.isTrigger = false;
            }
            else
            {
                SetNonTrapClient(row3Selected[i].index);
                row3Selected[i].trap = false;
                row3Selected[i].colapse3 = true;
                obs.isTrigger = true;
            }
        }
        LimitRow3.instance.isAdded = false;
        for (int i = 0; i <= 3; i++)
        {
            if (row3[i].trap == true)
            {
                BrainRow4.instance.indexListTrapRow4.Add(row3[i].index);
            }
        }
    }

    [ClientRpc]
    private void ResetTrap(int i)
    {
        BoxCollider obs = row3[i].gameObject.GetComponent<BoxCollider>();
        obs.isTrigger = true;
        row3[i].trap = false;
    }

    [ClientRpc]
    private void SetTrapClient(int i)
    {
        BoxCollider obs = row3[i].gameObject.GetComponent<BoxCollider>();
        row3[i].trap = true;
        row3[i].colapse = false;

        obs.isTrigger = false;
    }

    [ClientRpc]
    private void SetNonTrapClient(int i)
    {
        BoxCollider obs = row3[i].gameObject.GetComponent<BoxCollider>();
        row3[i].trap = false;

        obs.isTrigger = true;
    }

    #endregion ROW 3
}