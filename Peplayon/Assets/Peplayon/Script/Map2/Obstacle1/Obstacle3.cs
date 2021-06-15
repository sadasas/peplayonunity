using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle3 : NetworkBehaviour
{
    public int index;

    public bool trap, colapse, GET;

    public int indexlist;

    public GameObject effectPrefab, effect;

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
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.enabled = false;
        effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
    }
}