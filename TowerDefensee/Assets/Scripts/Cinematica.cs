using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematica : MonoBehaviour
{
    public float waitTime = 5f;

    private void Start()
    {
        StartCoroutine(Wait_for_intro());
    }

    IEnumerator Wait_for_intro()
    {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene("MainMenu");
    }

    public void SkipCinematic()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
