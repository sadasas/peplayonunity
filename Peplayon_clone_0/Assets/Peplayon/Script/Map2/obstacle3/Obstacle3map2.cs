using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Obstacle3map2 : NetworkBehaviour
{
    public static Obstacle3map2 instance;

    [SerializeField]
    private List<Obstacle33> listOsbtacle3 = new List<Obstacle33>();

    public int randomvalue;
    private bool colapse;
    public List<GameObject> spawned = new List<GameObject>();
    public Transform[] point;

    public GameObject[] ob;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!colapse && isServer)
        {
            spawnObject();
            colapse = true;
        }
    }

    [Server]
    public void spawnObject()
    {
        listOsbtacle3.Clear();
        Row2Obstacle3.instance.listOsbtacleSelected.Clear();
        LimitRow1Obstacle3.instance.listOsbtacle.Clear();
        LimitRow1Obstacle3.instance.colapse = false;
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

        listOsbtacle3.Add(ao);
        listOsbtacle3.Add(bo);
        listOsbtacle3.Add(co);
        listOsbtacle3.Add(fo);
        LimitRow1Obstacle3.instance.listOsbtacle.Add(ao);
        LimitRow1Obstacle3.instance.listOsbtacle.Add(bo);
        LimitRow1Obstacle3.instance.listOsbtacle.Add(co);
        LimitRow1Obstacle3.instance.listOsbtacle.Add(fo);

        setkkk(a, b, c, d);
        SetTrap();
    }

    [ClientRpc]
    private void setkkk(GameObject a, GameObject b, GameObject c, GameObject d)

    {
        listOsbtacle3.Clear();

        Debug.Log("ssssssssssssssssssssssssssssssssssssssssssssss");
        Obstacle33 ao = a.GetComponent<Obstacle33>();
        Obstacle33 bo = b.GetComponent<Obstacle33>();
        Obstacle33 co = c.GetComponent<Obstacle33>();
        Obstacle33 fo = d.GetComponent<Obstacle33>();

        listOsbtacle3.Add(ao);
        listOsbtacle3.Add(bo);
        listOsbtacle3.Add(co);
        listOsbtacle3.Add(fo);
    }

    [Server]
    public void SetTrap()
    {
        Row2Obstacle3.instance.listOsbtacleSelected.Clear();
        for (int i = 0; i <= 3; i++)
        {
            listOsbtacle3[i].trap = false;
            ResetTrap(i);
        }
        for (int i = 0; i <= listOsbtacle3.Count - 1; i++)
        {
            BoxCollider obs = listOsbtacle3[i].gameObject.GetComponent<BoxCollider>();

            randomvalue = UnityEngine.Random.Range(0, 2);
            if (randomvalue == 0)
            {
                Row2Obstacle3.instance.listOsbtacleSelected.Add(listOsbtacle3[i]);
                SetTrapClient(i);
                listOsbtacle3[i].trap = true;
                listOsbtacle3[i].colapse = false;
            }
            else
            {
            }
        }
        LimitRow1Obstacle3.instance.trapAdded = true;
    }

    [ClientRpc]
    private void ResetTrap(int i)
    {
        listOsbtacle3[i].trap = false;
    }

    [ClientRpc]
    private void SetTrapClient(int i)
    {
        listOsbtacle3[i].trap = true;
        listOsbtacle3[i].colapse = false;
    }
}