using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyStone : MonoBehaviour
{
    public GameObject effectPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            MeshRenderer mr = other.GetComponent<MeshRenderer>();

            Instantiate(effectPrefab, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}