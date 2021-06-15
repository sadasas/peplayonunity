using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaclemap22 : NetworkBehaviour
{
    public GameObject Stone;
    public Transform[] point;
    public float Countdown;
    private float fixCoundown;

    private void Start()
    {
        if (isServer)
            fixCoundown = Countdown;
    }

    private void Update()
    {
        if (isServer)
        {
            Countdown = Countdown - Time.deltaTime;
            if (Countdown <= 0)
            {
                int random = Random.Range(0, 3);
                SetStone(random);
                Countdown = fixCoundown;
                return;
            }
        }
    }

    [Server]
    private void SetStone(int i)
    {
        GameObject stone = Instantiate(Stone, point[i].position, Quaternion.identity);
        NetworkServer.Spawn(stone);
    }
}