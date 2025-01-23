using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    [Header("Building Manager and Layers")]
    [SerializeField] private BuildingManager buildingManager;


    [SerializeField] int layerNumber;
    [SerializeField] LayerMask problemLayers;
    private LayerMask canPlaceOnLayerMask;

    void Start()
    {
        buildingManager = BuildingManager.instance;
        canPlaceOnLayerMask = layerNumber;
    }

    private void Update()
    {
        //Checks if its placing
        if (buildingManager.isPlacementMode)
        {
            RaycastHit hit;
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, buildingManager.detectLayersMask)){
                //if the ray hit nothing you cant place
                buildingManager.canPlace = false;
            }
            else
            {
                if (hit.collider.gameObject.layer != canPlaceOnLayerMask){
                    //if the ray hits the wrong layer you cant place
                    buildingManager.canPlace = false;
                }
                else
                {
                    Collider[] colliders = Physics.OverlapBox(buildingManager.pendingObject.transform.position, buildingManager.pendingObject.transform.lossyScale, Quaternion.identity, problemLayers);
                    //if the colliders are higher than 1 it doesnt allow you to place. It detects its own range as the 1 object.
                    if (colliders.Length < 2)
                    {
                        buildingManager.canPlace = true;
                    }
                    else
                    {
                        buildingManager.canPlace = false;
                    }
                }
            }

        }
    }
}