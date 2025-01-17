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
    }
}