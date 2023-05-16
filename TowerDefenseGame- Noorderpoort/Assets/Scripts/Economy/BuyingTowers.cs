using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingTowers : MonoBehaviour
{
    //Reference other Scripts
    Bitscript bitscript;
    BuildingManager buildingManager;
    
    //Insert tower
    int tower = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //Call the other scripts
        bitscript = FindObjectOfType<Bitscript>();
        buildingManager = FindObjectOfType<BuildingManager>();
    }
    
    public void Tower(int number)
    {
        tower = number;
    }
    
    public void BuyTower(int cost)
    {
        if (bitscript.BitIndex >= cost)
        {
            bitscript.BitIndex -= cost;
            buildingManager.SelectObject(tower);
        }
    }
}
