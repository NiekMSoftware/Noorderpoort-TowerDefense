using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    private GameObject pendingObject;

    private Vector3 pos;

    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;

    public bool canPlace;
    
    void Update()
    {
        if (pendingObject != null)
        {
            pendingObject.transform.position = pos;

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceObject();
            }
        }
    }

    public void PlaceObject()
    {
        pendingObject = null;
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    public void SelectObject(int index)
    {
        pendingObject = Instantiate(objects[index], pos, transform.rotation);
    }
}
