using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    [Header("Main Title variables")]
    public GameObject optionsMenuCanvas;


    [Header("Paused variables")]
    public GameObject pauseCanvas;
    public GameObject optionsPauseCanvas;

    [Header("Options variables")]
    public AudioMixer master;
    public Dropdown cambioRes;
    Resolution[] resolutions;

    [Header("EndGame Variables")]
    public GameObject gameOverUI;
    public float timerGameOver;
    public bool activeTimer;
    public bool death;

    [Header("Next Level Variables")]
    public string levelToLoad = "MainLevel";
    public GameObject WinLevelUI;


    [Header("Fader")]
    public SceneFader sceneFader;

    [Header("Sounds")]
    public AudioClip botones;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioSource sfxSound;


    public static bool GameIsOver;


    // Start is called before the first frame update
    void Start()
    {
        GameIsOver = false;

        resolutions = Screen.resolutions;

        cambioRes.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        cambioRes.AddOptions(options);
        cambioRes.value = currentResolutionIndex;
        cambioRes.RefreshShownValue();

        timerGameOver = 60f;

    }

    // Update is called once per frame
    public void Update()
    {
        Debug.Log("" + Time.timeScale);

        if (activeTimer)
        {
            timerGameOver--;
        }
        else
        {
            timerGameOver = 60f;
        }

        if (PlayerStats.Lives <= 0)
        {
            death = true;
            
        }
        else
        {
            death = false;
        }

        if (death)
        {
            EndGame();
        }

        if (timerGameOver <= 0f)
        {
            Time.timeScale = 0;
            Debug.Log("Acabo");
            
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            Pause();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Retry();
        }
    }


    public void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
        sfxSound.PlayOneShot(loseSound);
        activeTimer = true;
    }


    public void WinLevel()
    {
        GameIsOver = true;
        WinLevelUI.SetActive(true);
        sfxSound.PlayOneShot(winSound);
    }


    //Main title functions
    public void PlayGame()
    {
        sceneFader.FadeTo(levelToLoad);
        SceneManager.LoadScene("Tutorial");
        Time.timeScale = 1f;
        sfxSound.PlayOneShot(botones);
    }

    public void Quit()
    {
        Debug.Log("Exit Succesfull");
        Application.Quit();
        sfxSound.PlayOneShot(botones);
    }
    public void OptionsMenu()
    {
        optionsMenuCanvas.SetActive(true);
        sfxSound.PlayOneShot(botones);
    }

    public void GoToMenu()
    {
        //Toggle();
        sceneFader.FadeTo(levelToLoad);
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        sfxSound.PlayOneShot(botones);
    }

    //In game functions
    public void Pause()   // Pausar el juego
    {
        pauseCanvas.SetActive(!pauseCanvas.activeSelf);

        if (pauseCanvas.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        sfxSound.PlayOneShot(botones);
    }

    public void Resume()     //Continuar con el juego
    {
        pauseCanvas.SetActive(false);
        activeTimer = false;
        death = false;
        Time.timeScale = 1f;

        sfxSound.PlayOneShot(botones);
    }

    public void Retry()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Carga de nuevo la escena actual, sin importar el nivel en que se encuentra

        sfxSound.PlayOneShot(botones);
    }

    public void OptionsInGame()    //Opciones del juego
    {
        pauseCanvas.SetActive(false);
        optionsPauseCanvas.SetActive(true);

        sfxSound.PlayOneShot(botones);
    }

    public void BacktoPause()
    {
        optionsPauseCanvas.SetActive(false);
        pauseCanvas.SetActive(true);

        sfxSound.PlayOneShot(botones);
    }

    public void OToMenu()
    {
        optionsMenuCanvas.SetActive(false);

        sfxSound.PlayOneShot(botones);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level1");

        sfxSound.PlayOneShot(botones);
    }

    //OPTIONS Functions
    public void MasterVolume (float masterLvl)
    {
        master.SetFloat("MasterVolume", Mathf.Log10(masterLvl) * 20);

        sfxSound.PlayOneShot(botones);
    }

    public void MusicVolume (float musicLvl)
    {
        master.SetFloat("MusicVolume", Mathf.Log10(musicLvl) * 20);

        sfxSound.PlayOneShot(botones);
    }

    public void SFXVolume (float sfxLvl)
    {
        master.SetFloat("SFXVolume", Mathf.Log10(sfxLvl) * 20);

        sfxSound.PlayOneShot(botones);
    } 

    public void SetScreenRes(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);


        sfxSound.PlayOneShot(botones);

    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;

        sfxSound.PlayOneShot(botones);
    }


}
