using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingTowers : MonoBehaviour
{
    //Reference other Scripts
    Bitscript bitscript;
    BuildingManager buildingManager;
    Selection selection;
    
    // Start is called before the first frame update
    void Start()
    {
        //Call the other scripts
        selection = FindObjectOfType<Selection>();
        bitscript = FindObjectOfType<Bitscript>();
        buildingManager = FindObjectOfType<BuildingManager>();
    }
    
    public void BuyTower(TowerScriptable tower)
    {
        if (buildingManager.pendingObject == null)
        {
            if (bitscript.bitIndex >= tower.cost)
            {
                bitscript.RemoveBits(tower.cost);
                buildingManager.SelectObject(tower);
                selection.DeSelect();
            }
        }
    }
}
