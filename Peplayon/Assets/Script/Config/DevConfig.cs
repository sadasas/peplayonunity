using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("Dev")]
public class DevConfig : MonoBehaviour
{
    public bool isDevMode = true;
    public GameObject gameManagerPrefab;
    public GameObject networkManagerPrefab;

    public void Start()
    {
        if (isDevMode)
        {
            Debug.Log("Running in Dev Mode");
        }
        else
        {
            Debug.Log("Running in Production Mode");
        }
    }
}
