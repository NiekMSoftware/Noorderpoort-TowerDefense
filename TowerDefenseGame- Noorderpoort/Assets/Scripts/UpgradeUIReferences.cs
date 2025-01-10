using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIReferences : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
