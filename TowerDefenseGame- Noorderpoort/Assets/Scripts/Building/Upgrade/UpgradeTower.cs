using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UpgradeTower : MonoBehaviour
{
    [Header("Reference to the Selection Script")]
    public Selection _selection;

    [Header("Canvas")]
    public GameObject upgradeUI;

    private Bitscript bits;

    [Space]
    [SerializeField] private GameObject turretEmpty;
    private void Start()
    {
        bits = FindObjectOfType<Bitscript>();
    }

    public void Upgrade()
    {
        //You cant upgrade if the game is paused
        if (PauseClass.instance.isPaused) { return; }

        //Gets the selected object and their stats
        GameObject tower = _selection.selectedObject;
        GeneralTowerScript generalTowerScript = tower.GetComponent<GeneralTowerScript>();

        if (!generalTowerScript.isBeingPlaced && generalTowerScript.towerStats.canUpgrade)
        {
            //Remove your money based on the cost of the upgradescriptable
            if (bits.RemoveBits(generalTowerScript.towerStats.upgradeScriptable.cost))
            {

                Quaternion qua = tower.transform.rotation;

                //Gets a new turret and puts in the exact position and rotation as the current turret
                GameObject newtower = Instantiate(generalTowerScript.towerStats.upgradeScriptable.prefab, tower.transform.position,qua,turretEmpty.transform);

                newtower.GetComponent<GeneralTowerScript>().SetStats(generalTowerScript.towerStats.upgradeScriptable);


                _selection.Select(newtower);
                Destroy(tower);
            }
        }
    }
}