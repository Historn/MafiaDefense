using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WavesSpawner : MonoBehaviour
{
    [Header("Sounds")]
    public AudioClip botones;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioSource sfxSound;

    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public Text waveCountdownText;

    public GameManager gameManager;

    public static int waveIndex = 0;

    private void Start()
    {
        waveIndex = 0;
    }

    void Update()
    {

        if (waveIndex == waves.Length)
        {
            countdown = 0;
            if (EnemiesAlive <= 0)
            {
                Debug.Log("Esto funciona y has ganado");
                gameManager.WinLevel();
                this.enabled = false;
            }
            /*Debug.Log("Esto funciona y has ganado");
            gameManager.WinLevel();
            this.enabled = false;*/
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            //waveIndex++;
            countdown = timeBetweenWaves;
            return;
        }

        if (waveIndex >= 4)
        {
            waveIndex = 4;
        }

        countdown -= Time.deltaTime;
        
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00}", countdown);             //"{0:00.00}" formato del tiempo


    }

    public IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;

    }

    public void SpawnButton()
    {
        /*if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            //waveIndex++;
            countdown = timeBetweenWaves;
            Debug.Log("Oleada Nueva");
            return;
        }*/
        StartCoroutine(SpawnWave());
        waveIndex++;
        countdown = timeBetweenWaves;
        sfxSound.PlayOneShot(botones);
        //return;*/
    }


   public void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }

}
