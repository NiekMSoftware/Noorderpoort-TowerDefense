using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingTowers : MonoBehaviour
{
    Bitscript bitscript;
    BuildingManager buildingManager;
    int tower = 0;
    // Start is called before the first frame update
    void Start()
    {
        bitscript = FindObjectOfType<Bitscript>();
        buildingManager = FindObjectOfType<BuildingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
