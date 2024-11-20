using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance !=null)
        {
            Debug.Log("More than one BuildManager in scene!");
                return;
        }
        instance = this;
    }

    [Header("Turrets Prefab")]
    public GameObject sniperPrefab;          //standardTurretPrefab
    public GameObject speakerPrefab;        //anotherTurretPrefab
    public GameObject teslaPrefab;

    [Header("Turrets Prefab")]
    public GameObject upgradedSniper;          //standardTurretPrefab
    public GameObject upgradedSpeaker;        //anotherTurretPrefab
    public GameObject upgradedTesla;

    [Header("Effects")]
    public GameObject buildEffect;
    public GameObject sellEffect;


    private TurretBlueprint torretaPorConstruir;         //Turretobuild
    private Node selectedNode;

    public NodeUI nodeUI;

    public bool CanBuild { get { return torretaPorConstruir != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= torretaPorConstruir.cost; } }


    public void SelectNode (Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        torretaPorConstruir = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.hide();
    }
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        torretaPorConstruir = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return torretaPorConstruir;
    }
}
