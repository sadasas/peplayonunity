using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainRow4 : NetworkBehaviour
{
    public static BrainRow4 instance;

    public List<int> indexListTrapRow4 = new List<int>();

    public List<Row1> row4 = new List<Row1>();

    public List<Row1> row4Selected = new List<Row1>();

    public bool colapseRow4 = false;
    public bool isLimitRow4 = false;

    public Transform[] point;

    public GameObject[] ob;
    public List<GameObject> spawned = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!colapseRow4 && isLimitRow4 && isServer)
        {
            spawnObject();
        }
    }

    [Server]
    public void spawnObject()
    {
        BrainRow5.instance.row5.Clear();
        row4.Clear();
        row4Selected.Clear();
        LimitRow4.instance.listRow4.Clear();
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

        row4.Add(ao);
        row4.Add(bo);
        row4.Add(co);
        row4.Add(fo);
        LimitRow4.instance.listRow4.Add(ao);
        LimitRow4.instance.listRow4.Add(bo);
        LimitRow4.instance.listRow4.Add(co);
        LimitRow4.instance.listRow4.Add(fo);
        setkkk(a, b, c, d);
        SetSelectedRow4();
        LimitRow4.instance.limitAdded = true;
    }

    [ClientRpc]
    private void setkkk(GameObject a, GameObject b, GameObject c, GameObject d)

    {
        row4.Clear();
        Debug.Log("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
        Row1 ao = a.GetComponent<Row1>();
        Row1 bo = b.GetComponent<Row1>();
        Row1 co = c.GetComponent<Row1>();
        Row1 fo = d.GetComponent<Row1>();

        row4.Add(ao);
        row4.Add(bo);
        row4.Add(co);
        row4.Add(fo);
    }

    #region ROW 4

    [Server]
    private void SetSelectedRow4()
    {
        int countListTrap = indexListTrapRow4.Count - 1;
        for (int i = 0; i <= countListTrap; i++)
        {
            colapseRow4 = true;
            int a = indexListTrapRow4[i];

            row4Selected.Add(row4[a]);
            if (indexListTrapRow4[i] == 0)
            {
                row4Selected.Add(row4[indexListTrapRow4[i] + 1]);
            }
            else if (indexListTrapRow4[i] == 1)
            {
                row4Selected.Add(row4[indexListTrapRow4[i] - 1]);
                row4Selected.Add(row4[indexListTrapRow4[i] + 1]);
            }
            else if (indexListTrapRow4[i] == 2)
            {
                row4Selected.Add(row4[indexListTrapRow4[i] - 1]);
                row4Selected.Add(row4[indexListTrapRow4[i] + 1]);
            }
            else if (indexListTrapRow4[i] == 3)
            {
                row4Selected.Add(row4[indexListTrapRow4[i] - 1]);
            }
        }
        SetRow4();
    }

    [Server]
    public void SetRow4()
    {
        for (int i = 0; i <= 3; i++)
        {
            BoxCollider obs = row4[i].gameObject.GetComponent<BoxCollider>();
            obs.isTrigger = true;
            row4[i].trap = false;
            ResetTrap(i);
        }
        BrainRow5.instance.indexListTrapRow5.Clear();
        BrainRow5.instance.row5Selected.Clear();
        LimitRow4.instance.isRow4Add = true;

        int countRowSelected = row4Selected.Count - 1;
        for (int i = 0; i <= countRowSelected; i++)
        {
            BoxCollider obs = row4Selected[i].gameObject.GetComponent<BoxCollider>();
            int randomvalue = UnityEngine.Random.Range(0, 2);
            if (randomvalue == 0)
            {
                SetTrapClient(row4Selected[i].index);
                row4Selected[i].trap = true;
                row4Selected[i].colapse4 = false;
                obs.isTrigger = false;
            }
            else
            {
                SetNonTrapClient(row4Selected[i].index);
                row4Selected[i].trap = false;
                row4Selected[i].colapse4 = true;
                obs.isTrigger = true;
            }
        }

        LimitRow4.instance.isAdded = false;
        for (int i = 0; i <= 3; i++)
        {
            if (row4[i].trap == true)
            {
                BrainRow5.instance.indexListTrapRow5.Add(row4[i].index);
            }
        }
    }

    [ClientRpc]
    private void ResetTrap(int i)
    {
        BoxCollider obs = row4[i].gameObject.GetComponent<BoxCollider>();
        obs.isTrigger = true;
        row4[i].trap = false;
    }

    [ClientRpc]
    private void SetTrapClient(int i)
    {
        BoxCollider obs = row4[i].gameObject.GetComponent<BoxCollider>();
        row4[i].trap = true;
        row4[i].colapse = false;

        obs.isTrigger = false;
    }

    [ClientRpc]
    private void SetNonTrapClient(int i)
    {
        BoxCollider obs = row4[i].gameObject.GetComponent<BoxCollider>();
        row4[i].trap = false;

        obs.isTrigger = true;
    }

    #endregion ROW 4
}