using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public GameObject selectedObject;

    public bool selectedTower;
    
    // Update is called once per frame
    void Update()
    {
        //Check the input for the mouse
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            //Compare if we selected a Tower tag
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Tower"))
                {
                    selectedTower = true;
                    print(selectedTower);
                    Debug.Log("hit");
                    Select(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButton(1) && selectedObject != null)
        {
            selectedTower = false;
            DeSelect();
        }
    }

    public void Select(GameObject obj)
    {
        //Check if the object we pressed is equal
        if (obj == selectedObject) 
            return;

        if (selectedObject != null)
            DeSelect();

        Outline outline = obj.GetComponent<Outline>();
        
        //Check if the object needs an outline
        if (outline == null)
            obj.AddComponent<Outline>();
        else 
            outline.enabled = true;
        
        selectedObject = obj;
    }

    public void DeSelect()
    {
        //Disable the Outline
        selectedObject.GetComponent<Outline>().enabled = false;
        selectedObject = null;
    }
}
