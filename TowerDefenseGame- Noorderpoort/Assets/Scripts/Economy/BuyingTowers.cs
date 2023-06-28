using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingTowers : MonoBehaviour
{
    //Reference other Scripts
    Bitscript bitscript;
    BuildingManager buildingManager;
    Selection selection;
    
    //Insert tower
    int tower = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //Call the other scripts
        selection = FindObjectOfType<Selection>();
        bitscript = FindObjectOfType<Bitscript>();
        buildingManager = FindObjectOfType<BuildingManager>();
    }
    
    public void Tower(int number) {
        print("Fucking Work, Joe");
        tower = number;
    }
    
    public void BuyTower(int cost)
    {
        if (buildingManager.pendingObject == null)
        {
            if (bitscript.BitIndex >= cost)
            {
                bitscript.BitIndex -= cost;
                buildingManager.SelectObject(tower);
            }
        }
    }
}
