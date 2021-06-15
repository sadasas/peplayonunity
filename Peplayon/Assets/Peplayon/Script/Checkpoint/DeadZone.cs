using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : NetworkBehaviour
{
    public static DeadZone instance;
    private DetectChild dt;
    private CharacterControls cr;

    private ItemPickup tp;

    public Vector3 currenctCheckPoint;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        dt = GameObject.FindGameObjectWithTag("IndicatorItemSpawn").GetComponent<DetectChild>();
        cr = other.gameObject.GetComponent<CharacterControls>();

        tp = GameObject.FindGameObjectWithTag("Item").GetComponent<ItemPickup>();
        if (!hasAuthority) return;
        if (other.CompareTag("Player"))
        {
            other.transform.position = currenctCheckPoint;
            tp.StopAllCoroutines();
            dt.Child();
            cr.SetDefaultValue();
            tp.DestroyTransculent();
            tp.destroyEffect();
            Debug.Log("transform");
        }
    }
}