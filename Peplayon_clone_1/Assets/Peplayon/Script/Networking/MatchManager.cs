using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    public void Play()
    {
        NetworkManagerTesting.instance.StartClient();
        NetworkManagerTesting.instance.networkAddress = "localhost";
    }
}