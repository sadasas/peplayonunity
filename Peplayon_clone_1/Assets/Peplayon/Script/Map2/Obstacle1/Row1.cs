using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row1 : NetworkBehaviour
{
    public int index, indexRow;
    public bool trap, colapse = false, colapse2 = false, colapse3 = false, colapse4 = false, colapse5 = false, colapse6 = false, GET;
    public GameObject effectPrefab, effect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!trap)
            {
                CharacterControls cr = other.gameObject.GetComponent<CharacterControls>();
                cr.speed = 0f;
                NetworkIdentity player = GameObject.FindGameObjectWithTag("Player").GetComponent<NetworkIdentity>();
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