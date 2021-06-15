using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Maiii : MonoBehaviour
{
    private Scene scene;

    public void MainMenu()
    {
        var parameters = new LoadSceneParameters(LoadSceneMode.Single);

        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }
}