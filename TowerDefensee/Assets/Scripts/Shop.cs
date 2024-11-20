using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("SFX")]
    public AudioSource sfxSound;
    public AudioClip botones;

    [Header("Base Turrets")]
    public TurretBlueprint torretaSniper;
    public TurretBlueprint torretaSpeaker;
    public TurretBlueprint torretaTesla;


    BuildManager tutorialBuildManager;

    private void Start()
    {
        tutorialBuildManager = BuildManager.instance;
    }
    public void SelecionaSniper()
    {
        Debug.Log("Sniper Comprada");
        tutorialBuildManager.SelectTurretToBuild(torretaSniper);
        sfxSound.PlayOneShot(botones);
    }
    public void SelecionaSpeaker()
    {
        Debug.Log("Speaker Comprado");
        tutorialBuildManager.SelectTurretToBuild(torretaSpeaker);
        sfxSound.PlayOneShot(botones);
    }
    public void SelecionaTesla()
    {
        Debug.Log("Tesla Comprado");
        tutorialBuildManager.SelectTurretToBuild(torretaTesla);
        sfxSound.PlayOneShot(botones);
    }

}
