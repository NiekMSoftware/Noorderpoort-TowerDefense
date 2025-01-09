using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildingManager : MonoBehaviour
{
    [Header("Tower Objects")]
    public List<Collider> towerTriggers;

    public GameObject[] objects;
    public GameObject pendingObject;

    private Vector3 pos;

    [FormerlySerializedAs("obj")]
    [Header("Deletus McFeetus")] 
    [SerializeField] public GameObject turretObj;

    [Header("Materials and Layers")]
    [SerializeField] private Material[] materials;

    private RaycastHit hit;
    public LayerMask detectLayersMask;

    [Header("Is the Tower Placeable?")]
    public bool canPlace;
    public bool isPlacementMode = false;

    public static TowerBehaviour towerReference;
    [SerializeField] private GameObject placeParticle;
    Selection selector;
    private void Start()
    {
        selector = FindObjectOfType<Selection>();
    }
    void Update()
    {
        if (pendingObject != null) {
            this.pendingObject.transform.parent = this.turretObj.transform;
            
            pendingObject.transform.position = pos;
            if (pendingObject.GetComponent<TowerAttacking>() == true)
            {
                pendingObject.GetComponent<TowerAttacking>().isBeingPlaced = true;
            }
            if (pendingObject.GetComponent<MMMTower>() == true)
            {
                pendingObject.GetComponent<MMMTower>().isBeingPlaced = true;
            }
            selector.previousPending = pendingObject;
            selector.timeSincePlace = 0;
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
        foreach (Collider coll in towerTriggers)
        {
            if (coll != null)
            {
                coll.gameObject.GetComponent<RangeScript>().ShowRange(false);
            }
        }
        pendingObject.GetComponent<MeshRenderer>().material = materials[2];
        towerTriggers.Add(pendingObject.GetComponent<Collider>());
        selector.Select(pendingObject);
        Instantiate(placeParticle, pendingObject.transform.position, pendingObject.transform.rotation);
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

    public void SelectObject(TowerScriptable tower)
    {
        
        foreach (Collider coll in towerTriggers)
        {
            if(coll != null)
            {
                coll.gameObject.GetComponent<RangeScript>().ShowRange(true);
                selector.selectedObject = coll.gameObject;
                selector.DeSelect();
            }
        }
        pendingObject = Instantiate(tower.prefab, pos, transform.rotation);
        towerReference = pendingObject.GetComponent<TowerBehaviour>();
        pendingObject.GetComponent<TowerAttacking>().SetStats(tower);
        isPlacementMode = true;
    }
}