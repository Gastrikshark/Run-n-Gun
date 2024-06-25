using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class SceneLoader : MonoBehaviour
{
    // laad een scene gebaseerd op de naam deze kan zelf ingevuld worden in de editor
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Exit()
    {// sluit de Applicatie af
        Debug.Log("Exit");
        Application.Quit();
    }
}