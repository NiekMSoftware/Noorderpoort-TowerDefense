using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Update = UnityEngine.PlayerLoop.Update;

public class CheckPlacement : MonoBehaviour
{
    [SerializeField]
    private BuildingManager buildingManager;


    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
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
            print("Unable to place");
            buildingManager.canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (buildingManager.towerTriggers.Contains(other))
        {
            print("Able to place");
            buildingManager.canPlace = true;
        }
    }
}