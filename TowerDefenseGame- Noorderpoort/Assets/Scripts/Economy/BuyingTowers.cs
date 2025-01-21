using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingTowers : MonoBehaviour
{
    //References other Scripts
    private BuildingManager buildingManager;
    private Selection selection;

    [SerializeField] private TowerScriptable debugTower;
    
    void Start()
    {
        //Call the other scripts
        selection = FindObjectOfType<Selection>();
        buildingManager = FindObjectOfType<BuildingManager>();
    }

    private void Update()
    {
        //If you are in the editor you can use the debug tower for all your debugging needs
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.V))
        {
            BuyTower(debugTower);
        }
        #endif
    }

    public void BuyTower(TowerScriptable tower)
    {
        //You cant buy if the game is paused or if you are already placing a tower
        if (PauseClass.instance.isPaused) { return; }
        if (buildingManager.pendingObject == null)
        {
            //Select any previously selected object and start placing the new tower
            selection.DeSelect();
            buildingManager.SelectObject(tower);
        }
    }
}
