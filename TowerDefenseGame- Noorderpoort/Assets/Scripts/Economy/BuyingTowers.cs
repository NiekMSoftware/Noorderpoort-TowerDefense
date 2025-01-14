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
        if (PauseClass.instance.isPaused) { return; }
        if (buildingManager.pendingObject == null)
        {
            if (bitscript.RemoveBits(tower.cost))
            {
                buildingManager.SelectObject(tower);
                selection.DeSelect();
            }
        }
    }
}
