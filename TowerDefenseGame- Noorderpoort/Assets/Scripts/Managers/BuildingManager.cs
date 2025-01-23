using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [Header("Tower Objects")]
    //The object the player is currently placing
    public GameObject pendingObject;
    private MeshRenderer pendingObjRenderer;

    private Vector3 pos;

    [Header("Turret empty")] 
    [SerializeField] public GameObject turretEmpty;

    [Header("Materials and Layers")]
    [SerializeField] private Material[] materials;
    public LayerMask detectLayersMask;

    private RaycastHit hit;

    [Header("Is the Tower Placeable")]
    public bool canPlace;
    public bool isPlacementMode = false;

    [Header("Misc")]
    [SerializeField] private GameObject placeParticle;
    [SerializeField] private GameObject cancelButton;

    private Selection selector;
    private Material originalMat;


    public static BuildingManager instance;
    private void Start()
    {
        selector = FindObjectOfType<Selection>();
        instance = this;
    }

    void Update()
    {
        //if you are placing, make it follow your mouse and tell tower its being placed
        if (pendingObject != null) {
            
            pendingObject.transform.position = pos;
            pendingObject.GetComponent<GeneralTowerScript>().isBeingPlaced = true;
            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceObject();
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            CancelPlace();
        }

        UpdateMaterials();
    }

    void UpdateMaterials()
    {
        //Updates tower material based on if it can be placed or not
        if (canPlace && pendingObject != null)
        {
            pendingObjRenderer.GetComponent<MeshRenderer>().material = materials[0];
        }

        if (!canPlace)
        {
            pendingObjRenderer.GetComponent<MeshRenderer>().material = materials[1];
        }
    }

    public void PlaceObject()
    {
        //cant place if: Game paused, Mouse over UI or Too poor
        if (PauseClass.instance.isPaused) { return; }
        if (Selection.IsPointerOverUIElement()) { return; }
        if (!Bitscript.instance.RemoveBits(pendingObject.GetComponent<GeneralTowerScript>().towerStats.cost)) { return; }

        //Make the tower know its placed
        pendingObject.GetComponent<GeneralTowerScript>().isBeingPlaced = false;
        pendingObjRenderer.GetComponent<MeshRenderer>().material = originalMat;
        pendingObject.transform.parent = turretEmpty.transform;

        //Change name for debugging
        int num = Random.Range(0, 1000);
        pendingObject.name = pendingObject.GetComponent<GeneralTowerScript>().towerStats.towerName + ": " + num;

        //Spawn particle
        Instantiate(placeParticle, pendingObject.transform.position, pendingObject.transform.rotation);

        //Deselect any other objects
        selector.DeSelect();

        //Removes unplaced tower range
        if (pendingObject.GetComponent<RangeScript>())
        {
            pendingObject.GetComponent<RangeScript>().ShowRange(false, false);
        }
        //Select new object
        selector.Select(pendingObject);

        //Make manager forget the object
        pendingObject = null;
        pendingObjRenderer = null;
        isPlacementMode = false;
        cancelButton.SetActive(false);
    }

    private void FixedUpdate()
    {
        //Gets the position the mouse is aiming
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, detectLayersMask))
        {
            pos = hit.point;
        }
    }

    public void StartPlacingObject(TowerScriptable tower)
    {
        isPlacementMode = true;
        pendingObject = Instantiate(tower.prefab, pos, transform.rotation);
        pendingObject.GetComponent<GeneralTowerScript>().SetStats(tower);

        //Shows if you can place in that spot
        pendingObjRenderer = pendingObject.transform.Find("Visual").GetComponent<MeshRenderer>();
        originalMat = pendingObjRenderer.GetComponent<MeshRenderer>().material;


        cancelButton.SetActive(true);
        if (pendingObject.GetComponent<RangeScript>())
        {
            pendingObject.GetComponent<RangeScript>().ShowRange(true, true);
        }
    }

    public void CancelPlace()
    {
        //Cant cancel if: Game paused, Not placing an object
        if (PauseClass.instance.isPaused) { return; }
        cancelButton.SetActive(false);
        pendingObjRenderer = null;
        if(pendingObject == null) { return; }

        //Get rid of the object
        selector.DeSelect();
        Destroy(pendingObject);
        pendingObject = null;
        isPlacementMode = false;
    }
}