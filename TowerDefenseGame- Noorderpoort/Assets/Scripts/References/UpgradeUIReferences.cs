using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIReferences : MonoBehaviour
{
    //Easy to get a bunch of objects for showing upgrade stats
    public TMP_Text[] currentStatValues = new TMP_Text[3];
    public TMP_Text[] upgradeStatValues = new TMP_Text[3];
    public GameObject[] statArrows;
    public Image[] statIcons = new Image[3];
    public Image towerImage;
    public TMP_Text towerName;
    public TMP_Text extraInfo;
    public Slider levelSlider;
    public TMP_Text levelText;
    public TMP_Text upgradeCostText;
    public TMP_Text sellText;
}
