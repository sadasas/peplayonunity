using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerConfiguration : MonoBehaviour
{
    public bool isServerConfig;
    private void Start()
    {
        if (isServerConfig)
        {
            Debug.Log("Running in server mode");
        }
    }
}
