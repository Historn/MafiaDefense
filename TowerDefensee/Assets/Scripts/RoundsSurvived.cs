using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    public Text roundsText;
    public WavesSpawner wavesSpawner;
     void OnEnable()
    {
        StartCoroutine(AnimateText());

    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";    //Setea el texto a 0 
        int round = 0;   //Variable creada a 0 llamada round

        yield return new WaitForSeconds(.7f);

        roundsText.text = WavesSpawner.waveIndex.ToString();

        /*while (round < PlayerStats.Rounds)    // Si round es menor a 3 suma round
        {
            round++;
            roundsText.text = round.ToString();

            yield return new WaitForSeconds(.05f);
        }*/
    }
}
