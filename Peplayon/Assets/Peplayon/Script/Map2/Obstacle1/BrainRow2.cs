using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainRow2 : NetworkBehaviour
{
    public static BrainRow2 instance;

    public List<int> indexListTrapRow2 = new List<int>();

    public List<Row1> row2 = new List<Row1>();

    public List<Row1> row2Selected = new List<Row1>();

    public bool colapseRow2 = false;
    public bool isLimitRow2 = false;
    public Transform[] point;

    public GameObject[] ob;
    public List<GameObject> spawned = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!colapseRow2 && isLimitRow2 && isServer)
        {
            spawnObject();
        }
    }

    [Server]
    public void spawnObject()
    {
        row2Selected.Clear();
        row2.Clear();

        BrainRow3.instance.row3.Clear();
        LimitRow2.instance.listRow2.Clear();
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

        row2.Add(ao);
        row2.Add(bo);
        row2.Add(co);
        row2.Add(fo);
        LimitRow2.instance.listRow2.Add(ao);
        LimitRow2.instance.listRow2.Add(bo);
        LimitRow2.instance.listRow2.Add(co);
        LimitRow2.instance.listRow2.Add(fo);
        setkkk(a, b, c, d);
        SetSelectedRow2();
        LimitRow2.instance.limitAdded = true;
    }

    [ClientRpc]
    private void setkkk(GameObject a, GameObject b, GameObject c, GameObject d)

    {
        row2.Clear();
        Debug.Log("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
        Row1 ao = a.GetComponent<Row1>();
        Row1 bo = b.GetComponent<Row1>();
        Row1 co = c.GetComponent<Row1>();
        Row1 fo = d.GetComponent<Row1>();

        row2.Add(ao);
        row2.Add(bo);
        row2.Add(co);
        row2.Add(fo);
    }

    #region ROW 2

    [Server]
    private void SetSelectedRow2()
    {
        int countListTrap = indexListTrapRow2.Count - 1;
        for (int i = 0; i <= countListTrap; i++)
        {
            colapseRow2 = true;
            int a = indexListTrapRow2[i];

            row2Selected.Add(row2[a]);
            if (indexListTrapRow2[i] == 0)
            {
                row2Selected.Add(row2[indexListTrapRow2[i] + 1]);
            }
            else if (indexListTrapRow2[i] == 1)
            {
                row2Selected.Add(row2[indexListTrapRow2[i] - 1]);
                row2Selected.Add(row2[indexListTrapRow2[i] + 1]);
            }
            else if (indexListTrapRow2[i] == 2)
            {
                row2Selected.Add(row2[indexListTrapRow2[i] - 1]);
                row2Selected.Add(row2[indexListTrapRow2[i] + 1]);
            }
            else if (indexListTrapRow2[i] == 3)
            {
                row2Selected.Add(row2[indexListTrapRow2[i] - 1]);
            }
        }
        SetRow2();
    }

    [Server]
    public void SetRow2()
    {
        for (int i = 0; i <= 3; i++)
        {
            BoxCollider obs = row2[i].gameObject.GetComponent<BoxCollider>();
            obs.isTrigger = true;
            row2[i].trap = false;
            ResetTrap(i);
        }
        BrainRow3.instance.indexListTrapRow3.Clear();
        BrainRow3.instance.row3Selected.Clear();
        LimitRow2.instance.isRow2Add = true;

        int countRowSelected = row2Selected.Count - 1;
        for (int i = 0; i <= countRowSelected; i++)
        {
            BoxCollider obs = row2Selected[i].gameObject.GetComponent<BoxCollider>();
            int randomvalue = UnityEngine.Random.Range(0, 2);
            if (randomvalue == 0)
            {
                SetTrapClient(row2Selected[i].index);
                row2Selected[i].colapse2 = false;
                row2Selected[i].trap = true;
                obs.isTrigger = false;
            }
            else
            {
                SetNonTrapClient(row2Selected[i].index);

                row2Selected[i].colapse2 = true;
                row2Selected[i].trap = false;
                obs.isTrigger = true;
            }
        }
        LimitRow2.instance.isAdded = false;

        for (int i = 0; i <= 3; i++)
        {
            if (row2[i].trap == true)
            {
                BrainRow3.instance.indexListTrapRow3.Add(row2[i].index);
            }
        }
    }

    [ClientRpc]
    private void ResetTrap(int i)
    {
        BoxCollider obs = row2[i].gameObject.GetComponent<BoxCollider>();
        obs.isTrigger = true;
        row2[i].trap = false;
    }

    [ClientRpc]
    private void SetTrapClient(int i)
    {
        BoxCollider obs = row2[i].gameObject.GetComponent<BoxCollider>();
        row2[i].trap = true;
        row2[i].colapse = false;

        obs.isTrigger = false;
    }

    [ClientRpc]
    private void SetNonTrapClient(int i)
    {
        BoxCollider obs = row2[i].gameObject.GetComponent<BoxCollider>();
        row2[i].trap = false;

        obs.isTrigger = true;
    }

    #endregion ROW 2
}