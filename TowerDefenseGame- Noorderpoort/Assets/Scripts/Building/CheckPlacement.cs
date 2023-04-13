using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Update = UnityEngine.PlayerLoop.Update;

public class CheckPlacement : MonoBehaviour
{
    [SerializeField] private BuildingManager buildingManager;

    [SerializeField] private LayerMask layerMask = 3;

    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    // Check if the building is properly above terrain it can be placed on
    private void Update()
    {
        if (buildingManager.isPlacementMode)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), 1000, layerMask))
            {
                buildingManager.canPlace = false;
            }
            else
            {
                buildingManager.canPlace = true;
            }
        }
    }

    // Code to check whether or not we are too close to a different Tower.

    #region Tower Placement check

    private void OnTriggerEnter(Collider other)
    {
        buildingManager.isPlacementMode = false;
        if (buildingManager.towerTriggers.Contains(other))
        {
            print("Unable to place");
            buildingManager.canPlace = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (buildingManager.towerTriggers.Contains(other))
        {

            buildingManager.isPlacementMode = false;            
            print("Unable to place");
            buildingManager.canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        buildingManager.isPlacementMode = true;
        if (buildingManager.towerTriggers.Contains(other))
        {
            print("Able to place");
            buildingManager.canPlace = true;
        }
    }

    #endregion
}