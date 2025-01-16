using System;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{

    [Header("Tower Upgrade Level")]
    public int towerUpgrade = 0;
    
    [Header("Building Manager and Layer")]
    [SerializeField] private BuildingManager buildingManager;


    [SerializeField] int layerNumber;
    private LayerMask canPlaceOnLayerMask;
    [SerializeField] LayerMask problemLayers;

    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        canPlaceOnLayerMask = layerNumber;
    }

    // Check if the building is properly above terrain it can be placed on
    private void Update()
    {
        if (buildingManager.isPlacementMode)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, buildingManager.detectLayersMask))
            {
                if (hit.collider.gameObject.layer == canPlaceOnLayerMask)
                {
                    Collider[] colliders = Physics.OverlapBox(buildingManager.pendingObject.transform.position, buildingManager.pendingObject.transform.lossyScale, Quaternion.identity, problemLayers);
                    if(colliders.Length < 2)
                    {
                        buildingManager.canPlace = true;

                    }
                    else
                    {
                        for(int i = 0; i < colliders.Length; i++)
                        {
                            print(colliders[i].gameObject.name);
                        }
                        buildingManager.canPlace = false;
                    }
                }
                else
                {
                    buildingManager.canPlace = false;
                }
            }

        }
    }

    // Code to check whether or not we are too close to a different Tower.

    #region Tower Placement check

    private void OnTriggerEnter(Collider other)
    {
        if (buildingManager.towerTriggers.Contains(other) || other.tag == "Tower" || other.gameObject.tag == "Trap")
        {
            //buildingManager.isPlacementMode = false;
            //buildingManager.canPlace = false;
            //print("Enter object: " + other.name);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (buildingManager.towerTriggers.Contains(other) || other.tag == "Tower" || other.gameObject.tag == "Trap")
        {
            //buildingManager.isPlacementMode = false;
            //buildingManager.canPlace = false;
            //print("In object: " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (buildingManager.towerTriggers.Contains(other) || other.tag == "Tower" || other.tag == "Trap")
        {
            //buildingManager.isPlacementMode = true;
            //buildingManager.canPlace = true;
            //print("Exit object: " + other.name);
        }
    }

    #endregion
}