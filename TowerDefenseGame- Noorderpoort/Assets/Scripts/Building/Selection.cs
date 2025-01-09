using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    [Header("Did we Select an Object?")]
    public GameObject selectedObject;
    public bool selectedTower;
    [SerializeField] private BuildingManager builderman;
    public GameObject previousPending;
    public float timeSincePlace;

    public UpgradeUIReferences upgradeUI;
    private void Start()
    {
        builderman = FindObjectOfType<BuildingManager>();
    }
    // Update is called once per frame
    void Update()
    {
        timeSincePlace += Time.deltaTime;
        //Check the input for the mouse
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            //Compare if we selected a Tower tag
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Tower"))
                {
                    if (hit.collider.gameObject != previousPending && hit.collider.gameObject != builderman.pendingObject)
                    {
                        selectedTower = true;
                        Select(hit.collider.gameObject);
                    }
                    else
                    {
                        if (timeSincePlace > 0.5f)
                        {
                            selectedTower = true;
                            Select(hit.collider.gameObject);
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButton(1) && selectedObject != null)
        {
            selectedTower = false;
            DeSelect();
        }
    }

    public void Select(GameObject obj)
    {
        //Check if the object we pressed is equal
        if (obj == selectedObject) 
            return;

        if (selectedObject != null)
            DeSelect();

        Outline outline = obj.GetComponent<Outline>();

        print("HIIII");
        if (obj.GetComponent<RangeScript>()) { obj.GetComponent<RangeScript>().ShowRange(); }
        
        //Check if the object needs an outline
        if (outline == null)
            obj.AddComponent<Outline>();
        else 
            outline.enabled = true;
        
        selectedObject = obj;
        UpdateUpgradeUI(obj.GetComponent<GeneralTowerScript>().towerStats);
    }

    private void UpdateUpgradeUI(TowerScriptable towerType)
    {
        int currentStat = 0;
        TowerScriptable upgrade;
        if (towerType.canUpgrade) {
            upgrade = towerType.upgradeScriptable;
            upgradeUI.statIcons[currentStat].color = new Color(1,1,1,1);
            if (towerType.damage < upgrade.damage)
            {
                upgradeUI.currentStatValues[currentStat].text = towerType.damage.ToString();
                upgradeUI.upgradeStatValues[currentStat].text = upgrade.damage.ToString();
                currentStat++;
            }
            if (towerType.firerate > upgrade.firerate)
            {
                upgradeUI.currentStatValues[currentStat].text = towerType.firerate.ToString();
                upgradeUI.upgradeStatValues[currentStat].text = upgrade.firerate.ToString();
                currentStat++;
            }
            if (towerType.range < upgrade.range)
            {
                upgradeUI.currentStatValues[currentStat].text = towerType.range.ToString();
                upgradeUI.upgradeStatValues[currentStat].text = upgrade.range.ToString();
                currentStat++;
            }
        }
        else
        {
            for(int i = 0;i < upgradeUI.currentStatValues.Length; i++)
            {
                upgradeUI.currentStatValues[i].text = " ";
                upgradeUI.upgradeStatValues[i].text = " ";
                upgradeUI.statIcons[i].color = new Color(0, 0, 0, 0);
            }
        }
        upgradeUI.towerName.text = towerType.towerName;
        upgradeUI.towerImage.sprite = towerType.towerIcon;
        upgradeUI.levelSlider.maxValue = towerType.maxTier;
        upgradeUI.levelSlider.value = towerType.currentTier;
        upgradeUI.levelText.text = towerType.currentTier.ToString();
    }

    public void DeSelect()
    {
        if(selectedObject == null) return;
        //Disable the Outline
        selectedObject.GetComponent<Outline>().enabled = false;
        if (selectedObject.GetComponent<RangeScript>()) { selectedObject.GetComponent<RangeScript>().ShowRange(false); }
            
        print("BYEEEEE" + selectedObject.name);
        selectedObject = null;
    }
}
