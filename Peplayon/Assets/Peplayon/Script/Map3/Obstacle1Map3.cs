using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle1Map3 : NetworkBehaviour
{
    private bool GET;
    public GameObject effect, effectPrefab;

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
        Debug.Log("DDDDDDDDDDDDDDDDDDDDDDDDDDDFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");

        Destroy(this.gameObject, 100);
        effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
    }
}