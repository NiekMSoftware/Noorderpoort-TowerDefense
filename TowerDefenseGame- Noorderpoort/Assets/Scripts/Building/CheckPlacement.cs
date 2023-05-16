using System;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    //An Enum to save all the Towers
    public enum allTowerTypes
    {
        NotAFuckingTower,
        Turret,
        Tesla,
        MMM
    };

    [Header("Tower Upgrade Level")]
    public int towerUpgrade = 0;
    
    [Header("Tower Types")]
    public allTowerTypes towerType;
    
    [Header("Building Manager and Layer")]
    [SerializeField] private BuildingManager buildingManager;


    [SerializeField] int layerNumber;
    private LayerMask canPlaceOnLayerMask;

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
                    buildingManager.canPlace = true;
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
        if (buildingManager.towerTriggers.Contains(other) || other.tag == "Tower")
        {
            buildingManager.isPlacementMode = false;
            buildingManager.canPlace = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (buildingManager.towerTriggers.Contains(other) || other.tag == "Tower")
        {
            buildingManager.isPlacementMode = false;
            buildingManager.canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (buildingManager.towerTriggers.Contains(other) || other.tag == "Tower")
        {
            buildingManager.isPlacementMode = true;
            buildingManager.canPlace = true;
        }
    }

    #endregion
}