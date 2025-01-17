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

    [Header("Towers and Upgrades")]
    public GameObject[] teslaTowers;
    public GameObject[] turrets;
    [SerializeField] private int[] teslaUpgradeCost;
    [SerializeField] private int[] turretUpgradeCost;
    [SerializeField] private int[] MMMUpgradeCost;
    Bitscript bits;

    [SerializeField] GameObject turretEmpty;

    private int upgradeIndex;
    private void Start()
    {
        bits = FindObjectOfType<Bitscript>();
    }
    private void Update()
    {
    }

    public void Upgrade()
    {
        if (PauseClass.instance.isPaused) { return; }
        GameObject tower = _selection.selectedObject;
        GeneralTowerScript generalTowerScript = tower.GetComponent<GeneralTowerScript>();

        if (!generalTowerScript.isBeingPlaced && generalTowerScript.towerStats.canUpgrade)
        {
            if (bits.RemoveBits(generalTowerScript.towerStats.upgradeScriptable.cost))
            {
                Quaternion qua = tower.transform.rotation;
                GameObject newtower = Instantiate(generalTowerScript.towerStats.upgradeScriptable.prefab);
                newtower.transform.position = tower.transform.position;
                newtower.transform.rotation = qua;

                newtower.transform.parent = this.turretEmpty.transform;

                newtower.GetComponent<GeneralTowerScript>().SetStats(generalTowerScript.towerStats.upgradeScriptable);

                _selection.Select(newtower);
                Destroy(tower);
            }
        }

        /*if (tower.GetComponent<CheckPlacement>().towerType == CheckPlacement.allTowerTypes.MMM)
        {
            if (tower.GetComponent<MMMTower>().upgrade1 && bits.bitIndex >= MMMUpgradeCost[0])
            {
                tower.GetComponent<MMMTower>().upgrade1 = false;
                tower.GetComponent<MMMTower>().upgrade2 = true;
                bits.RemoveBits(MMMUpgradeCost[0]);
            }
            else if (tower.GetComponent<MMMTower>().upgrade2 && bits.bitIndex >= MMMUpgradeCost[1])
            {
                tower.GetComponent<MMMTower>().upgrade2 = false;
                tower.GetComponent<MMMTower>().upgrade3 = true;
                _selection.DeSelect();
                bits.RemoveBits(MMMUpgradeCost[1]);
            }
        }
        if (tower.GetComponent<TowerAttacking>().isBeingPlaced == false)
        {
            if (tower.GetComponent<CheckPlacement>().towerType == CheckPlacement.allTowerTypes.Tesla)
            {
                if (tower.GetComponent<CheckPlacement>().towerUpgrade == 0 && bits.bitIndex >= teslaUpgradeCost[0])
                {
                    bool bought = bits.RemoveBits(teslaUpgradeCost[0]);
                    if (bought)
                    {
                        GameObject newtower = Instantiate(teslaTowers[0]);
                        newtower.transform.position = tower.transform.position;

                        newtower.transform.parent = this.turretEmpty.transform;
                        
                        _selection.Select(newtower);
                        Destroy(tower);
                    }
                }
                else if (tower.GetComponent<CheckPlacement>().towerUpgrade == 1&& bits.bitIndex >= teslaUpgradeCost[1])
                {
                    bool bought = bits.RemoveBits(teslaUpgradeCost[1]);
                    if (bought)
                    {
                        GameObject newtower = Instantiate(teslaTowers[1]);
                        newtower.transform.position = tower.transform.position;
                        
                        newtower.transform.parent = this.turretEmpty.transform;
                        
                        _selection.DeSelect();
                        Destroy(tower);
                    }
                }
            }
            if (tower.GetComponent<CheckPlacement>().towerType == CheckPlacement.allTowerTypes.Turret)
            {
                if (tower.GetComponent<CheckPlacement>().towerUpgrade == 0&& bits.bitIndex >= turretUpgradeCost[0])
                {
                    bool bought = bits.RemoveBits(turretUpgradeCost[0]);
                    if (bought)
                    {
                        GameObject newtower = Instantiate(turrets[0]);
                        newtower.transform.position = tower.transform.position;
                        
                        newtower.transform.parent = this.turretEmpty.transform;
                        
                        _selection.Select(newtower);
                        Destroy(tower);
                    }
                }
                else if (tower.GetComponent<CheckPlacement>().towerUpgrade == 1&& bits.bitIndex >= turretUpgradeCost[1])
                {
                    bool bought = bits.RemoveBits(turretUpgradeCost[1]);
                    if (bought)
                    {
                        GameObject newtower = Instantiate(turrets[1]);
                        newtower.transform.position = tower.transform.position;
                        
                        newtower.transform.parent = this.turretEmpty.transform;
                        
                        _selection.DeSelect();
                        Destroy(tower);
                    }
                }
            }
        }*/
    }
}