using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public List<Collider> towerTriggers;

    public GameObject[] objects;
    private GameObject pendingObject;

    private Vector3 pos;

    [SerializeField] private Material[] materials;

    private RaycastHit hit;
    public LayerMask detectLayersMask;

    public bool canPlace;
    public bool isPlacementMode = false;

    public static TowerBehaviour towerReference;
    
    void Update()
    {
        if (pendingObject != null)
        {
            pendingObject.transform.position = pos;
            if (pendingObject.GetComponent<TowerAttacking>() == true)
            {
                pendingObject.GetComponent<TowerAttacking>().isBeingPlaced = true;
            }
            if (pendingObject.GetComponent<MMMTower>() == true)
            {
                pendingObject.GetComponent<MMMTower>().isBeingPlaced = true;
            }
            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                if (pendingObject.GetComponent<TowerAttacking>() == true)
                {
                    pendingObject.GetComponent<TowerAttacking>().isBeingPlaced = false;
                }
                if (pendingObject.GetComponent<MMMTower>() == true)
                {
                    pendingObject.GetComponent<MMMTower>().isBeingPlaced = false;
                }
                PlaceObject();
            }
        }

        UpdateMaterials();
    }

    void UpdateMaterials()
    {
        if (canPlace && pendingObject != null)
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[0];
        }

        if (!canPlace)
        {
            pendingObject.GetComponent<MeshRenderer>().material = materials[1];
        }
    }

    public void PlaceObject()
    {
        pendingObject.GetComponent<MeshRenderer>().material = materials[2];
        towerTriggers.Add(pendingObject.GetComponent<Collider>());
        pendingObject = null;
        isPlacementMode = false;
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, detectLayersMask))
        {
            pos = hit.point;
        }
    }

    public void SelectObject(int index)
    {
        pendingObject = Instantiate(objects[index], pos, transform.rotation);
        towerReference = pendingObject.GetComponent<TowerBehaviour>();
        isPlacementMode = true;
    }
}