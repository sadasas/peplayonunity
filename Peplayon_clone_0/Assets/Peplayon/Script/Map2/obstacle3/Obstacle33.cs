using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle33 : NetworkBehaviour
{
    public int index;

    // public int indexList, indexRow;
    public bool trap = false, colapse = false, colapse2 = false, colapse3 = false, colapse4 = false, colapse5 = false, GET;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!trap)

            {
                NetworkIdentity player = other.gameObject.GetComponent<NetworkIdentity>();
                NetworkIdentity item = GetComponent<NetworkIdentity>();
                AuthoryManager aM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AuthoryManager>();
                aM.getauthority(item, player);
                GET = true;
            }
        }
    }

    private void Update()
    {
        if (GET)
        {
            if (hasAuthority)
            {
                GET = false;
                SetCMD();
            }
        }
    }

    [Command]
    private void SetCMD()
    {
        SetTrap();
    }

    [ClientRpc]
    public void SetTrap()

    {
        LeanTween.moveLocalY(gameObject, -3.1f, 1);
    }
}