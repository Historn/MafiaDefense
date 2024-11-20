﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
        {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
