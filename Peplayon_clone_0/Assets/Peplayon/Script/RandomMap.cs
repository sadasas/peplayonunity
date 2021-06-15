using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomMap : NetworkBehaviour
{
    public GameObject[] imageMap;
    public string[] sceneName;
    public bool colapse = false;

    private void Update()
    {
        if (isServer)
        {
            if (!colapse)
            {
                colapse = true;
                SetRandom();
                Debug.Log("SET RANDOM");
            }
        }
    }

    [Server]
    private void SetRandom()
    {
        int random = Random.Range(0, 2);

        if (random == 0)
        {
            Debug.Log("000000000000000000");

            StartCoroutine(ChangeScene(1));
            ClientSetDisplay(0);
        }
        else
        {
            Debug.Log("1111111111111111111111111111");

            StartCoroutine(ChangeScene(2));
            ClientSetDisplay(1);
        }
    }

    [Server]
    private IEnumerator ChangeScene(int i)
    {
        yield return new WaitForSeconds(7f);
        NetworkManagerTesting.instance.ServerChangeScene(NetworkManagerTesting.instance.sceneNameList[i]);
    }

    [ClientRpc]
    public void ClientSetDisplay(int i)
    {
        if (i == 0)
        {
            StartCoroutine(DisplaayRandom0());
        }
        else if (i == 1)
        {
            StartCoroutine(DisplaayRandom1());
        }
    }

    private IEnumerator DisplaayRandom0()
    {
        imageMap[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[1].SetActive(false);
        imageMap[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);

        imageMap[0].SetActive(false);
        imageMap[2].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[2].SetActive(false);
        imageMap[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[1].SetActive(false);
        imageMap[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);

        imageMap[0].SetActive(false);
        imageMap[2].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[2].SetActive(false);
        imageMap[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[1].SetActive(false);
        imageMap[0].SetActive(true);
        yield return new WaitForSeconds(5f);
    }

    private IEnumerator DisplaayRandom1()
    {
        imageMap[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[0].SetActive(false);
        imageMap[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[1].SetActive(false);
        imageMap[2].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[2].SetActive(false);
        imageMap[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[0].SetActive(false);
        imageMap[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[1].SetActive(false);
        imageMap[2].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[2].SetActive(false);
        imageMap[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[0].SetActive(false);
        imageMap[1].SetActive(true);
    }

    private IEnumerator DisplaayRandom2()
    {
        imageMap[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[1].SetActive(false);
        imageMap[2].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[2].SetActive(false);
        imageMap[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[1].SetActive(false);
        imageMap[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[0].SetActive(false);
        imageMap[2].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[2].SetActive(false);
        imageMap[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[1].SetActive(false);
        imageMap[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        imageMap[0].SetActive(false);
        imageMap[2].SetActive(true);
    }
}