using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopReferences : MonoBehaviour
{
    public static ShopReferences Instance;
    [SerializeField] private TMP_Text[] towerCosts;
    [SerializeField] private TowerScriptable[] towersInSpots;
    private Bitscript money;
    private void Start()
    {
        Instance = this;
    }
    public void UpdateCosts()
    {
        for(int i = 0;i < towerCosts.Length; i++)
        {
            towerCosts[i].text = Bitscript.instance.CalculateWithDiscount(towersInSpots[i].cost).ToString();
        }
    }
}
