using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color colorSinDinero;
    public Vector3 positionOffset;

    private Renderer rend;
    private Color colorInicial;

    [HideInInspector]
    public GameObject torreta;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgradeed = false;

    [Header("Sonidos De Construcción")]
    public AudioSource sfx;
    public AudioClip sonidoDinero;
    public AudioClip sonidoBuild;


    BuildManager tutorialBuildManager;

     void Start()
    {
        rend = GetComponent<Renderer>();
        colorInicial = rend.material.GetColor("_BaseColor");

        tutorialBuildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosotion ()
    {
        return transform.position + positionOffset;
    }

     void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())          //Detecta los elementos de UI i hace que no puedas colocar torretas al hacer click.
            return;

       if (torreta != null)
        {
            tutorialBuildManager.SelectNode(this);
            return;
        }
           
       if (!tutorialBuildManager.CanBuild)
            return;

        BuildTurret(tutorialBuildManager.GetTurretToBuild());  /**/
    }


    void BuildTurret (TurretBlueprint blueprint)
    {

        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("No hay suficiente dinero");
            return;

        }

        PlayerStats.Money -= blueprint.cost;
        sfx.PlayOneShot(sonidoDinero);
        sfx.PlayOneShot(sonidoBuild);

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosotion(), Quaternion.identity);
        torreta = _turret;

        turretBlueprint = blueprint;


        GameObject effect = (GameObject)Instantiate(tutorialBuildManager.buildEffect, GetBuildPosotion(), Quaternion.identity);
        Destroy(effect, 1f);

        Debug.Log("Torreta construida!");

        if (_turret.CompareTag("Upgraded"))
        {
            isUpgradeed = true;
        }
    }

    public void  UpgradeTurret()
    {

        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("No se puede mejorar!");
            return;

        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;
        sfx.PlayOneShot(sonidoDinero);
        sfx.PlayOneShot(sonidoBuild);

        //Borrar la torreta anterior
        Destroy(torreta);
        
        //Construye la nueva
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosotion(), Quaternion.identity);
        torreta = _turret;


        GameObject upgradeEffect = (GameObject)Instantiate(tutorialBuildManager.buildEffect, GetBuildPosotion(), Quaternion.identity);
        Destroy(upgradeEffect, 1f);

        isUpgradeed = true;

        Debug.Log("Torreta mejorada!");
    }

     public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();
        sfx.PlayOneShot(sonidoDinero);

        GameObject upgradeEffect = (GameObject)Instantiate(tutorialBuildManager.sellEffect, GetBuildPosotion(), Quaternion.identity);
        Destroy(upgradeEffect, 1f);

        Destroy(torreta);
        turretBlueprint = null;

        isUpgradeed = false;
    }



     void OnMouseEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())          //Detecta los elementos de UI i hace que no puedas colocar torretas al hacer click.
            return;


        if (!tutorialBuildManager.CanBuild)
            return;

        if (tutorialBuildManager.HasMoney)
        {
            rend.material.SetColor("_BaseColor", hoverColor);

        }else
        {
            rend.material.SetColor("_BaseColor", colorSinDinero);
        }

    }

     void OnMouseExit()
    {
        rend.material.SetColor("_BaseColor", colorInicial);
    }

}
