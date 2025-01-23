using UnityEngine;
using TMPro;

public class ShopReferences : MonoBehaviour
{
    //So you can update the costs from anywhere
    public static ShopReferences Instance;

    //The value texts and their respective scriptables
    [SerializeField] private TMP_Text[] towerCosts;
    [SerializeField] private TowerScriptable[] towersInSpots;
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
