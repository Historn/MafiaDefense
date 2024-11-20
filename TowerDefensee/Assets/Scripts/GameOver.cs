using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;  


    public void Retry ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Carga de nuevo la escena actual, sin importar el nivel en que se encuentra
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }

}
