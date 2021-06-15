using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map3Manager : NetworkBehaviour
{
    public GameObject[] obstaclePrefab;
    public Transform[] aa, ab, ac, ad, ae, af, ag, ah, ai, aj, ak;
    public Transform[] ba, bb, bc, bd, be, bf, bg, bh, bi, bj, bk;
    public Transform[] ca, cb, cc, cd, ce, cf, cg, ch, ci, cj, ck;
    public Transform[] da, db, dc, dd, de, df, dg, dh, di, dj, dk;
    public Transform[] ea, eb, ec, ed, ee, ef, eg, eh, ei, ej, ek;

    private void Start()
    {
        if (isServer)
        {
            SetObstacle();
        }
    }

    [Server]
    private void SetObstacle()
    {
        //1
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[0], aa[i].position, obstaclePrefab[0].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[0], ab[i].position, obstaclePrefab[0].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[0], ac[i].position, obstaclePrefab[0].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[0], ad[i].position, obstaclePrefab[0].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[0], ae[i].position, obstaclePrefab[0].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[0], af[i].position, obstaclePrefab[0].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[0], ag[i].position, obstaclePrefab[0].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[0], ah[i].position, obstaclePrefab[0].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[0], ai[i].position, obstaclePrefab[0].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[0], aj[i].position, obstaclePrefab[0].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[0], ak[i].position, obstaclePrefab[0].transform.rotation);
            NetworkServer.Spawn(go);
        }

        //2

        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[1], ba[i].position, obstaclePrefab[1].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[1], bb[i].position, obstaclePrefab[1].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[1], bc[i].position, obstaclePrefab[1].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[1], bd[i].position, obstaclePrefab[1].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[1], be[i].position, obstaclePrefab[1].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[1], bf[i].position, obstaclePrefab[1].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[1], bg[i].position, obstaclePrefab[1].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[1], bh[i].position, obstaclePrefab[1].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[1], bi[i].position, obstaclePrefab[1].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[1], bj[i].position, obstaclePrefab[1].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[1], bk[i].position, obstaclePrefab[1].transform.rotation);
            NetworkServer.Spawn(go);
        }

        //3

        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[2], ca[i].position, obstaclePrefab[2].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[2], cb[i].position, obstaclePrefab[2].transform.rotation);
            NetworkServer.Spawn(go);
        }

        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[2], cc[i].position, obstaclePrefab[2].transform.rotation);
            NetworkServer.Spawn(go);
        }

        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[2], cd[i].position, obstaclePrefab[2].transform.rotation);
            NetworkServer.Spawn(go);
        }

        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[2], ce[i].position, obstaclePrefab[2].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[2], cf[i].position, obstaclePrefab[2].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[2], cg[i].position, obstaclePrefab[2].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[2], ch[i].position, obstaclePrefab[2].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[2], ci[i].position, obstaclePrefab[2].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[2], cj[i].position, obstaclePrefab[2].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[2], ck[i].position, obstaclePrefab[2].transform.rotation);
            NetworkServer.Spawn(go);
        }

        //4
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[3], da[i].position, obstaclePrefab[3].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[3], db[i].position, obstaclePrefab[3].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[3], dc[i].position, obstaclePrefab[3].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[3], dd[i].position, obstaclePrefab[3].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[3], de[i].position, obstaclePrefab[3].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[3], df[i].position, obstaclePrefab[3].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[3], dg[i].position, obstaclePrefab[3].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[3], dh[i].position, obstaclePrefab[3].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[3], di[i].position, obstaclePrefab[3].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[3], dj[i].position, obstaclePrefab[3].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[3], dk[i].position, obstaclePrefab[3].transform.rotation);
            NetworkServer.Spawn(go);
        }

        //5

        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[4], ea[i].position, obstaclePrefab[4].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[4], eb[i].position, obstaclePrefab[4].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[4], ec[i].position, obstaclePrefab[4].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[4], ed[i].position, obstaclePrefab[4].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[4], ee[i].position, obstaclePrefab[4].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[4], ef[i].position, obstaclePrefab[4].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[4], eg[i].position, obstaclePrefab[4].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[4], eh[i].position, obstaclePrefab[4].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[4], ei[i].position, obstaclePrefab[4].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[4], ej[i].position, obstaclePrefab[4].transform.rotation);
            NetworkServer.Spawn(go);
        }
        for (int i = 0; i <= 10; i++)
        {
            GameObject go = Instantiate(obstaclePrefab[4], ek[i].position, obstaclePrefab[4].transform.rotation);
            NetworkServer.Spawn(go);
        }
    }
}