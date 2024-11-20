using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    public string menusSceneName = "MainMenu";

    public string nextLevel = "Level1";
    public int levelToUnlock = 2;


    public SceneFader sceneFader;

    public void Continue()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name); 
    }

    public void Menu()
    {
        sceneFader.FadeTo(menusSceneName);    
    }
}
