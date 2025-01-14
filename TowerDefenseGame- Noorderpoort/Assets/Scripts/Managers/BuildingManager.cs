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
    private MeshRenderer pendingObjRenderer;

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

    private Material originalMat; 
    private void Start()
    {
        selector = FindObjectOfType<Selection>();
    }
    void Update()
    {
        if (pendingObject != null) {
            this.pendingObject.transform.parent = this.turretObj.transform;
            
            pendingObject.transform.position = pos;
            pendingObject.GetComponent<GeneralTowerScript>().isBeingPlaced = true;
            selector.previousPending = pendingObject;
            selector.timeSincePlace = 0;
            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                pendingObject.GetComponent<GeneralTowerScript>().isBeingPlaced = false;
                PlaceObject();
            }
        }

        UpdateMaterials();
    }

    void UpdateMaterials()
    {
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
        if (PauseClass.instance.isPaused) { return; }
        foreach (Collider coll in towerTriggers)
        {
            if (coll != null && coll.gameObject.GetComponent<RangeScript>() != null)
            {
                coll.gameObject.GetComponent<RangeScript>().ShowRange(false);
            }
        }
        pendingObjRenderer.GetComponent<MeshRenderer>().material = originalMat;
        towerTriggers.Add(pendingObject.GetComponent<Collider>());
        print(selector.name);
        selector.Select(pendingObject);
        Instantiate(placeParticle, pendingObject.transform.position, pendingObject.transform.rotation);
        pendingObject = null;
        pendingObjRenderer = null;
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
        isPlacementMode = true;
        foreach (Collider coll in towerTriggers)
        {
            if(coll != null && coll.gameObject.GetComponent<RangeScript>() != null)
            {
                coll.gameObject.GetComponent<RangeScript>().ShowRange(true);
                selector.selectedObject = coll.gameObject;
                selector.DeSelect();
            }
        }
        pendingObject = Instantiate(tower.prefab, pos, transform.rotation);
        towerReference = pendingObject.GetComponent<TowerBehaviour>();
        pendingObject.GetComponent<GeneralTowerScript>().SetStats(tower);
        pendingObjRenderer = pendingObject.transform.Find("Visual").GetComponent<MeshRenderer>();
        originalMat = pendingObjRenderer.GetComponent<MeshRenderer>().material;
    }
}