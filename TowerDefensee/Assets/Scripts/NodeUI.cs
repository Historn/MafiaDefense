using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;


    public GameObject ui;


    [Header("Upgrade")]
    public Text upgradeCost;
    public Button upgradeButon;

    [Header("Sell")]
    public Text sellAmount;

    [Header("SFX")]
    public AudioSource sfxSound;
    public AudioClip botones;

    public void SetTarget(Node _target)
    {
        target = _target;
        transform.position = target.GetBuildPosotion();

        if(!target.isUpgradeed)
        {
            upgradeCost.text = target.turretBlueprint.upgradeCost + " $";
            upgradeButon.interactable = true;
        }
        else
        {
            upgradeCost.text = "Max.";
            upgradeButon.interactable = false;
        }

        sellAmount.text = target.turretBlueprint.GetSellAmount() + " $";


        ui.SetActive(true);
    }

    public void hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
        sfxSound.PlayOneShot(botones);
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
        sfxSound.PlayOneShot(botones);
    }
}
