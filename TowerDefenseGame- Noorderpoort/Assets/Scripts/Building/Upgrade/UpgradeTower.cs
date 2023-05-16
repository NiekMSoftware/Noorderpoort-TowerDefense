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

    private int upgradeIndex;

    private void Update()
    {
        if (_selection.selectedTower)
        {
            //Turn on the game object
            upgradeUI.SetActive(enabled);
        }
        else
        {
            upgradeUI.SetActive(false);
        }
    }

    public void Upgrade()
    {
        Bitscript bits = FindObjectOfType<Bitscript>();
        GameObject tower = _selection.selectedObject;

        if (tower.GetComponent<CheckPlacement>().towerType == CheckPlacement.allTowerTypes.Tesla)
        {
            if (tower.GetComponent<CheckPlacement>().towerUpgrade == 0 && bits.BitIndex >= teslaUpgradeCost[0])
            {
                GameObject newtower = Instantiate(teslaTowers[0]);
                newtower.transform.position = tower.transform.position;

                bits.RemoveBits(teslaUpgradeCost[0]);
                _selection.Select(newtower);
                Destroy(tower);
            }
            else if (tower.GetComponent<CheckPlacement>().towerUpgrade == 1&& bits.BitIndex >= teslaUpgradeCost[1])
            {
                GameObject newtower = Instantiate(teslaTowers[1]);
                newtower.transform.position = tower.transform.position;
                bits.RemoveBits(teslaUpgradeCost[1]);
                _selection.DeSelect();
                Destroy(tower);
            }
        }
        if (tower.GetComponent<CheckPlacement>().towerType == CheckPlacement.allTowerTypes.Turret)
        {
            if (tower.GetComponent<CheckPlacement>().towerUpgrade == 0&& bits.BitIndex >= turretUpgradeCost[0])
            {
                GameObject newtower = Instantiate(turrets[0]);
                newtower.transform.position = tower.transform.position;
                bits.RemoveBits(turretUpgradeCost[0]);
                _selection.Select(newtower);
                Destroy(tower);
            }
            else if (tower.GetComponent<CheckPlacement>().towerUpgrade == 1&& bits.BitIndex >= turretUpgradeCost[1])
            {
                GameObject newtower = Instantiate(turrets[1]);
                newtower.transform.position = tower.transform.position;
                bits.RemoveBits(turretUpgradeCost[1]);
                _selection.DeSelect();
                Destroy(tower);
            }
        }
        if (tower.GetComponent<CheckPlacement>().towerType == CheckPlacement.allTowerTypes.MMM)
        {
            if (tower.GetComponent<MMMTower>().upgrade1 && bits.BitIndex >= MMMUpgradeCost[0])
            {
                tower.GetComponent<MMMTower>().upgrade1 = false;
                tower.GetComponent<MMMTower>().upgrade2 = true;
                bits.RemoveBits(MMMUpgradeCost[0]);
            }
            else if (tower.GetComponent<MMMTower>().upgrade2 && bits.BitIndex >= MMMUpgradeCost[1])
            {
                tower.GetComponent<MMMTower>().upgrade2 = false;
                tower.GetComponent<MMMTower>().upgrade3 = true;
                _selection.DeSelect();
                bits.RemoveBits(MMMUpgradeCost[1]);
            }
        }
    }
}