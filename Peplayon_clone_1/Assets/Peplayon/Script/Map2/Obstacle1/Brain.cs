using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Brain : NetworkBehaviour
{
    public static Brain instance;
    public int index;

    public List<int> indexListTrap = new List<int>();

    public List<Row1> row1 = new List<Row1>();

    public List<Row1> row1Selected = new List<Row1>();

    public bool colapse = false;
    public bool isLimit = false;
    public Transform[] point;

    public GameObject[] ob;
    public List<GameObject> spawned = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!colapse && isLimit && isServer)
        {
            spawnObject();
        }
    }

    [Server]
    public void spawnObject()
    {
        row1Selected.Clear();
        row1.Clear();
        BrainRow2.instance.row2.Clear();
        LimitRow1.instance.listRow1.Clear();
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

        row1.Add(ao);
        row1.Add(bo);
        row1.Add(co);
        row1.Add(fo);
        LimitRow1.instance.listRow1.Add(ao);
        LimitRow1.instance.listRow1.Add(bo);
        LimitRow1.instance.listRow1.Add(co);
        LimitRow1.instance.listRow1.Add(fo);
        setkkk(a, b, c, d);
        SetSelectedRow1();
        LimitRow1.instance.limitAdded = true;
    }

    [ClientRpc]
    private void setkkk(GameObject a, GameObject b, GameObject c, GameObject d)

    {
        row1.Clear();
        Debug.Log("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
        Row1 ao = a.GetComponent<Row1>();
        Row1 bo = b.GetComponent<Row1>();
        Row1 co = c.GetComponent<Row1>();
        Row1 fo = d.GetComponent<Row1>();

        row1.Add(ao);
        row1.Add(bo);
        row1.Add(co);
        row1.Add(fo);
    }

    #region ROW 1

    [Server]
    private void SetSelectedRow1()
    {
        int countListTrap = indexListTrap.Count - 1;
        for (int i = 0; i <= countListTrap; i++)
        {
            colapse = true;
            int a = indexListTrap[i];

            row1Selected.Add(row1[a]);
            if (indexListTrap[i] == 0)
            {
                row1Selected.Add(row1[indexListTrap[i] + 1]);
            }
            else if (indexListTrap[i] == 1)
            {
                row1Selected.Add(row1[indexListTrap[i] - 1]);
                row1Selected.Add(row1[indexListTrap[i] + 1]);
            }
            else if (indexListTrap[i] == 2)
            {
                row1Selected.Add(row1[indexListTrap[i] - 1]);
                row1Selected.Add(row1[indexListTrap[i] + 1]);
            }
            else if (indexListTrap[i] == 3)
            {
                row1Selected.Add(row1[indexListTrap[i] - 1]);
            }
        }
        SetRow1();
    }

    [Server]
    public void SetRow1()
    {
        for (int i = 0; i <= 3; i++)
        {
            BoxCollider obs = row1[i].gameObject.GetComponent<BoxCollider>();
            obs.isTrigger = true;
            row1[i].trap = false;
            ResetTrap(i);
        }
        BrainRow2.instance.indexListTrapRow2.Clear();
        BrainRow2.instance.row2Selected.Clear();

        LimitRow1.instance.isRow1Add = true;

        int countRowSelected = row1Selected.Count - 1;
        for (int i = 0; i <= countRowSelected; i++)
        {
            BoxCollider obs = row1Selected[i].gameObject.GetComponent<BoxCollider>();

            int randomvalue = UnityEngine.Random.Range(0, 2);
            if (randomvalue == 0)
            {
                SetTrapClient(row1Selected[i].index);
                row1Selected[i].trap = true;
                row1Selected[i].colapse = false;
                obs.isTrigger = false;
            }
            else
            {
                SetNonTrapClient(row1Selected[i].index);
                obs.isTrigger = true;
                row1Selected[i].trap = false;
                row1Selected[i].colapse = true;
            }
        }

        LimitRow1.instance.isAdded = false;
        for (int i = 0; i <= 3; i++)
        {
            if (row1[i].trap == true)
            {
                BrainRow2.instance.indexListTrapRow2.Add(row1[i].index);
            }
        }
    }

    [ClientRpc]
    private void ResetTrap(int i)
    {
        BoxCollider obs = row1[i].gameObject.GetComponent<BoxCollider>();
        obs.isTrigger = true;
        row1[i].trap = false;
    }

    [ClientRpc]
    private void SetTrapClient(int i)
    {
        BoxCollider obs = row1[i].gameObject.GetComponent<BoxCollider>();
        row1[i].trap = true;
        row1[i].colapse = false;

        obs.isTrigger = false;
    }

    [ClientRpc]
    private void SetNonTrapClient(int i)
    {
        BoxCollider obs = row1[i].gameObject.GetComponent<BoxCollider>();
        row1[i].trap = false;

        obs.isTrigger = true;
    }

    #endregion ROW 1
}