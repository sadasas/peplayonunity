using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class ObstacleManager : NetworkBehaviour
{
    public static ObstacleManager instance;
    public bool setObstacleMap3 = false, setObstacleMap2 = false, setItem = false, pointAdded = true, setWin = false;

    public string[] sceneNameListtt;

    //MAP3
    public GameObject[] obstacleMap3;

    public GameObject deadZonePrefab;

    public int matchPlayed = 0;

    //MAP2
    public GameObject[] obstacle1Map2Prefab, obstacle2Map2Prefab, obstacle3Map2Prefab, obstacle4Map2Prefab;

    //ITEM
    public GameObject[] itemPrefab;

    public List<Transform> itemPoint = new List<Transform>();

    //WIN
    public GameObject[] winPrefab;

    public string[] listScene;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (matchPlayed > 2)
        {
            matchPlayed = 0;
        }
        Scene activeScene = SceneManager.GetActiveScene();

        if (isServer)
        {
            if (setWin)
            {
                setWin = false;
                SetWinCond();
            }
            if (setItem == true)
            {
                Debug.Log("11111111111111111111111111111111111EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
                setItem = false;
                GameObject[] item = UnityEngine.Object.FindObjectsOfType<GameObject>();
                if (!pointAdded)
                {
                    pointAdded = true;

                    foreach (GameObject ca in item)
                    {
                        if (ca.CompareTag("SpawnItemPoint"))
                        {
                            itemPoint.Add(ca.transform);
                        }
                    }
                }
                Spawnitemrandom();
            }

            if (setObstacleMap3 == true)
            {
                setObstacleMap3 = false;

                SetObstacleMap3();
            }

            if (setObstacleMap2)
            {
                setObstacleMap2 = false;

                SetObstacle1Map2();
            }
        }
    }

    [Server]
    private void SetWinCond()
    {
        Scene activeScene = SceneManager.GetActiveScene();

        Debug.Log("WINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN");
        Transform pos = GameObject.FindGameObjectWithTag("WinPoint").transform;
        if (matchPlayed == 0)
        {
            matchPlayed++;
            if (activeScene.name == listScene[0])
            {
                GameObject win = Instantiate(winPrefab[0], pos.position, pos.rotation);
                NetworkServer.Spawn(win);
            }
            else if (activeScene.name == listScene[1])
            {
                GameObject win = Instantiate(winPrefab[1], pos.position, pos.rotation);
                NetworkServer.Spawn(win);
            }
        }
        else if (matchPlayed == 1)
        {
            matchPlayed++;
            GameObject win = Instantiate(winPrefab[2], pos.position, pos.rotation);
            NetworkServer.Spawn(win);
        }
    }

    [Server]
    private void SetObstacleMap3()
    {
        Transform point = GameObject.FindGameObjectWithTag("Map3").transform;
        Transform point2 = GameObject.FindGameObjectWithTag("30").transform;

        GameObject go = Instantiate(obstacleMap3[0], point.position, Quaternion.identity);
        GameObject dz = Instantiate(deadZonePrefab, point2.position, point2.rotation);
        NetworkServer.Spawn(go);
        NetworkServer.Spawn(dz);
    }

    [Server]
    public void SetObstacle1Map2()

    {
        Debug.Log("11111111111111111111111111111111111111111111111111111111111111dddddddddddddd111111");
        Transform point = GameObject.FindGameObjectWithTag("1").transform;
        Transform point2 = GameObject.FindGameObjectWithTag("2").transform;
        Transform point3 = GameObject.FindGameObjectWithTag("3").transform;
        Transform point4 = GameObject.FindGameObjectWithTag("4").transform;
        Transform point5 = GameObject.FindGameObjectWithTag("5").transform;
        Transform point6 = GameObject.FindGameObjectWithTag("6").transform;
        Transform point7 = GameObject.FindGameObjectWithTag("7").transform;
        Transform point8 = GameObject.FindGameObjectWithTag("8").transform;
        Transform point9 = GameObject.FindGameObjectWithTag("9").transform;
        Transform point10 = GameObject.FindGameObjectWithTag("10").transform;
        Transform point11 = GameObject.FindGameObjectWithTag("11").transform;
        Transform point12 = GameObject.FindGameObjectWithTag("12").transform;
        Transform point13 = GameObject.FindGameObjectWithTag("13").transform;
        Transform point14 = GameObject.FindGameObjectWithTag("14").transform;
        Transform point15 = GameObject.FindGameObjectWithTag("15").transform;
        Transform point16 = GameObject.FindGameObjectWithTag("16").transform;
        Transform point17 = GameObject.FindGameObjectWithTag("21").transform;
        Transform point18 = GameObject.FindGameObjectWithTag("22").transform;
        Transform point19 = GameObject.FindGameObjectWithTag("23").transform;
        Transform point20 = GameObject.FindGameObjectWithTag("24").transform;
        Transform point21 = GameObject.FindGameObjectWithTag("25").transform;
        Transform point22 = GameObject.FindGameObjectWithTag("26").transform;

        GameObject row1 = Instantiate(obstacle1Map2Prefab[0], point.position, point.rotation);
        GameObject row2 = Instantiate(obstacle1Map2Prefab[1], point2.position, point.rotation);
        GameObject row3 = Instantiate(obstacle1Map2Prefab[2], point3.position, point.rotation);
        GameObject row4 = Instantiate(obstacle1Map2Prefab[3], point4.position, point.rotation);
        GameObject row5 = Instantiate(obstacle1Map2Prefab[4], point5.position, point.rotation);
        GameObject row6 = Instantiate(obstacle1Map2Prefab[5], point6.position, point.rotation);
        GameObject row7 = Instantiate(obstacle1Map2Prefab[6], point7.position, point.rotation);
        GameObject row8 = Instantiate(obstacle1Map2Prefab[7], point8.position, point.rotation);
        GameObject Obstacle1 = Instantiate(obstacle2Map2Prefab[0], point9.position, point9.rotation);
        GameObject Obstacle2 = Instantiate(obstacle2Map2Prefab[1], point10.position, point10.rotation);
        GameObject Obstacle3 = Instantiate(obstacle2Map2Prefab[2], point11.position, point11.rotation);
        GameObject Obstacle4 = Instantiate(obstacle3Map2Prefab[0], point12.position, point12.rotation);
        GameObject Obstacle5 = Instantiate(obstacle3Map2Prefab[1], point13.position, point13.rotation);
        GameObject Obstacle6 = Instantiate(obstacle3Map2Prefab[2], point14.position, point14.rotation);
        GameObject Obstacle7 = Instantiate(obstacle3Map2Prefab[3], point15.position, point15.rotation);
        GameObject Obstacle8 = Instantiate(obstacle3Map2Prefab[4], point16.position, point16.rotation);
        GameObject Obstacle9 = Instantiate(obstacle4Map2Prefab[0], point17.position, point17.rotation);
        GameObject Obstacle10 = Instantiate(obstacle4Map2Prefab[1], point18.position, point18.rotation);
        GameObject Obstacle11 = Instantiate(obstacle4Map2Prefab[2], point19.position, point19.rotation);
        GameObject Obstacle12 = Instantiate(obstacle4Map2Prefab[0], point20.position, point20.rotation);
        GameObject Obstacle13 = Instantiate(obstacle4Map2Prefab[1], point21.position, point21.rotation);
        GameObject Obstacle14 = Instantiate(obstacle4Map2Prefab[2], point22.position, point22.rotation);

        NetworkServer.Spawn(row1);
        NetworkServer.Spawn(row2);
        NetworkServer.Spawn(row3);
        NetworkServer.Spawn(row4);
        NetworkServer.Spawn(row5);
        NetworkServer.Spawn(row6);
        NetworkServer.Spawn(row7);
        NetworkServer.Spawn(row8);
        NetworkServer.Spawn(Obstacle1);
        NetworkServer.Spawn(Obstacle2);
        NetworkServer.Spawn(Obstacle3);
        NetworkServer.Spawn(Obstacle4);
        NetworkServer.Spawn(Obstacle5);
        NetworkServer.Spawn(Obstacle6);
        NetworkServer.Spawn(Obstacle7);
        NetworkServer.Spawn(Obstacle8);
        NetworkServer.Spawn(Obstacle9);
        NetworkServer.Spawn(Obstacle10);
        NetworkServer.Spawn(Obstacle11);
        NetworkServer.Spawn(Obstacle12);
        NetworkServer.Spawn(Obstacle13);
        NetworkServer.Spawn(Obstacle14);
    }

    [Server]
    private void Spawnitemrandom()
    {
        for (int a = 0; a <= 5; a++)
        {
            int b = Random.Range(0, 5);

            GameObject itemRandom = Instantiate(itemPrefab[b], itemPoint[a].position, itemPrefab[b].transform.rotation);
            NetworkServer.Spawn(itemRandom);
        }
    }
}