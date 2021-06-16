using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle1Map3 : NetworkBehaviour
{
    public static Obstacle1Map3 instance;
    public static bool active = false;
    private bool GET;
    public GameObject effect, effectPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active)
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

        Destroy(this.gameObject, 0.5f);
        effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
    }
}