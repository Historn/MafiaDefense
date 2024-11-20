using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }


    IEnumerator FadeIn ()
    {
        float t = 1f;
        while (t > 0f)
        {
          t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color (0.0745372f, 0.08216223f, 0.2358491f, a);
         yield return 0; //Skip to next frame  and wait

         }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;
        while (t > 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(19f, 21f, 60f, a);
            yield return 0; //Skip to next frame  and wait

        }

        SceneManager.LoadScene(scene);
    }
}
