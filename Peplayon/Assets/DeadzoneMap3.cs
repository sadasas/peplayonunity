using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DeadzoneMap3 : NetworkBehaviour
{
    private bool GET;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NetworkIdentity player = other.gameObject.GetComponent<NetworkIdentity>();
            Debug.Log("triggerRRRRRRRRRRRRRRRRRRRRRRRRRRR");
            NetworkIdentity item = GetComponent<NetworkIdentity>();
            AuthoryManager aM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AuthoryManager>();

            aM.getauthority(item, player);
            GET = true;
        }
    }

    private void Update()
    {
        if (GET)
        {
            if (hasAuthority)
            {
                GET = false;
                AuthoryManager am = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AuthoryManager>();
                am.Lose = true;
            }
        }
    }
}