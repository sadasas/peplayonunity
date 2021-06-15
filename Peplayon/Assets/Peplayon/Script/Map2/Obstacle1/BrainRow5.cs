using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainRow5 : NetworkBehaviour
{
    public static BrainRow5 instance;

    public List<int> indexListTrapRow5 = new List<int>();

    public List<Row1> row5 = new List<Row1>();

    public List<Row1> row5Selected = new List<Row1>();

    public bool colapseRow5 = false;
    public bool isLimitRow5 = false;
    public Transform[] point;

    public GameObject[] ob;
    public List<GameObject> spawned = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!colapseRow5 && isLimitRow5 && isServer)
        {
            spawnObject();
        }
    }

    [Server]
    public void spawnObject()
    {
        BrainRow6.instance.row6.Clear();
        row5.Clear();
        row5Selected.Clear();
        LimitRow5.instance.listRow5.Clear();
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

        row5.Add(ao);
        row5.Add(bo);
        row5.Add(co);
        row5.Add(fo);
        LimitRow5.instance.listRow5.Add(ao);
        LimitRow5.instance.listRow5.Add(bo);
        LimitRow5.instance.listRow5.Add(co);
        LimitRow5.instance.listRow5.Add(fo);
        setkkk(a, b, c, d);
        SetSelectedRow5();
        LimitRow5.instance.limitAdded = true;
    }

    [ClientRpc]
    private void setkkk(GameObject a, GameObject b, GameObject c, GameObject d)

    {
        row5.Clear();
        Debug.Log("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
        Row1 ao = a.GetComponent<Row1>();
        Row1 bo = b.GetComponent<Row1>();
        Row1 co = c.GetComponent<Row1>();
        Row1 fo = d.GetComponent<Row1>();

        row5.Add(ao);
        row5.Add(bo);
        row5.Add(co);
        row5.Add(fo);
    }

    #region ROW 5

    [Server]
    private void SetSelectedRow5()
    {
        int countListTrap = indexListTrapRow5.Count - 1;
        for (int i = 0; i <= countListTrap; i++)
        {
            colapseRow5 = true;
            int a = indexListTrapRow5[i];

            row5Selected.Add(row5[a]);
            if (indexListTrapRow5[i] == 0)
            {
                row5Selected.Add(row5[indexListTrapRow5[i] + 1]);
            }
            else if (indexListTrapRow5[i] == 1)
            {
                row5Selected.Add(row5[indexListTrapRow5[i] - 1]);
                row5Selected.Add(row5[indexListTrapRow5[i] + 1]);
            }
            else if (indexListTrapRow5[i] == 2)
            {
                row5Selected.Add(row5[indexListTrapRow5[i] - 1]);
                row5Selected.Add(row5[indexListTrapRow5[i] + 1]);
            }
            else if (indexListTrapRow5[i] == 3)
            {
                row5Selected.Add(row5[indexListTrapRow5[i] - 1]);
            }
        }
        SetRow5();
    }

    [Server]
    public void SetRow5()
    {
        for (int i = 0; i <= 3; i++)
        {
            BoxCollider obs = row5[i].gameObject.GetComponent<BoxCollider>();
            obs.isTrigger = true;
            row5[i].trap = false;
            ResetTrap(i);
        }
        BrainRow6.instance.indexListTrapRow6.Clear();
        BrainRow6.instance.row6Selected.Clear();

        LimitRow5.instance.isRow5Add = true;
        int countRowSelected = row5Selected.Count - 1;
        for (int i = 0; i <= countRowSelected; i++)
        {
            BoxCollider obs = row5Selected[i].gameObject.GetComponent<BoxCollider>();
            int randomvalue = UnityEngine.Random.Range(0, 2);
            if (randomvalue == 0)
            {
                SetTrapClient(row5Selected[i].index);
                row5Selected[i].trap = true;
                row5Selected[i].colapse5 = false;
                obs.isTrigger = false;
            }
            else
            {
                SetNonTrapClient(row5Selected[i].index);
                row5Selected[i].trap = false;
                row5Selected[i].colapse5 = true;
                obs.isTrigger = true;
            }
        }
        LimitRow5.instance.isAdded = false;
        for (int i = 0; i <= 3; i++)
        {
            if (row5[i].trap == true)
            {
                BrainRow6.instance.indexListTrapRow6.Add(row5[i].index);
            }
        }
    }

    #endregion ROW 5

    [ClientRpc]
    private void ResetTrap(int i)
    {
        BoxCollider obs = row5[i].gameObject.GetComponent<BoxCollider>();
        obs.isTrigger = true;
        row5[i].trap = false;
    }

    [ClientRpc]
    private void SetTrapClient(int i)
    {
        BoxCollider obs = row5[i].gameObject.GetComponent<BoxCollider>();
        row5[i].trap = true;
        row5[i].colapse = false;

        obs.isTrigger = false;
    }

    [ClientRpc]
    private void SetNonTrapClient(int i)
    {
        BoxCollider obs = row5[i].gameObject.GetComponent<BoxCollider>();
        row5[i].trap = false;

        obs.isTrigger = true;
    }
}