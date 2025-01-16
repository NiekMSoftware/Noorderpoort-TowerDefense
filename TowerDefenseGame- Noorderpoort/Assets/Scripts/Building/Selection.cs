using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    [Header("Did we Select an Object?")]
    public GameObject selectedObject;
    public bool selectedTower;
    [SerializeField] private BuildingManager builderman;
    public GameObject previousPending;
    public float timeSincePlace;

    public UpgradeUIReferences upgradeUI;
    [SerializeField] private Sprite[] statSprites;
    private void Start()
    {
        builderman = FindObjectOfType<BuildingManager>();
    }
    // Update is called once per frame
    void Update()
    {

        timeSincePlace += Time.deltaTime;
        //Check the input for the mouse
        if (Input.GetMouseButtonDown(0))
        {
            if(IsPointerOverUIElement()) { return; }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            //Compare if we selected a Tower tag
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Tower") || hit.collider.gameObject.CompareTag("Trap"))
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
                else if(!IsPointerOverUIElement() && builderman.isPlacementMode == false)
                {
                    selectedTower = false;
                    DeSelect();
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
        if (PauseClass.instance.isPaused) { return; }
        //Check if the object we pressed is equal
        if (obj == selectedObject) 
            return;

        if (selectedObject != null)
            DeSelect();

        Outline outline = obj.GetComponent<Outline>();

        if (obj.GetComponent<RangeScript>()) { obj.GetComponent<RangeScript>().ShowRange(true,false); }
        
        //Check if the object needs an outline
        if (outline == null)
            obj.AddComponent<Outline>();
        else 
            outline.enabled = true;
        
        selectedObject = obj;
        UpdateUpgradeUI(obj.GetComponent<GeneralTowerScript>().towerStats);
    }

    public void SellTower()
    {
        if (PauseClass.instance.isPaused) { return; }
        if (selectedObject != null)
        {
            builderman.towerTriggers.Remove(selectedObject.GetComponent<Collider>());
            Bitscript.instance.AddBits(selectedObject.GetComponent<GeneralTowerScript>().towerStats.sellValue);
            Destroy(selectedObject);
            DeSelect();
        }
    }

    private void UpdateUpgradeUI(TowerScriptable towerType)
    {
        int currentStat = 0;
        TowerScriptable upgrade;
        if (upgradeUI == null) return;
        upgradeUI.gameObject.SetActive(true);
        if (towerType.canUpgrade) {
            upgrade = towerType.upgradeScriptable;
            for (int i = currentStat; i < upgradeUI.currentStatValues.Length; i++)
            {
                upgradeUI.statArrows[i].SetActive(true);
                upgradeUI.currentStatValues[i].text = " ";
                upgradeUI.upgradeStatValues[i].text = " ";
                upgradeUI.statIcons[i].color = new Color(1, 1, 1, 1);
            }
            upgradeUI.upgradeCostText.text = Bitscript.instance.CalculateWithDiscount(upgrade.cost).ToString();
            if (towerType.damage < upgrade.damage)
            {
                upgradeUI.currentStatValues[currentStat].text = towerType.damage.ToString();
                upgradeUI.upgradeStatValues[currentStat].text = upgrade.damage.ToString();
                upgradeUI.statIcons[currentStat].sprite = statSprites[0];
                currentStat++;
            }
            if (towerType.firerate > upgrade.firerate)
            {
                upgradeUI.currentStatValues[currentStat].text = towerType.firerate.ToString() + "s";
                upgradeUI.upgradeStatValues[currentStat].text = upgrade.firerate.ToString() + "s";
                upgradeUI.statIcons[currentStat].sprite = statSprites[1];
                currentStat++;
            }
            if (towerType.range < upgrade.range)
            {
                upgradeUI.currentStatValues[currentStat].text = towerType.range.ToString();
                upgradeUI.upgradeStatValues[currentStat].text = upgrade.range.ToString();
                upgradeUI.statIcons[currentStat].sprite = statSprites[2];
                currentStat++;
            }
            if (towerType.moneyPerWave < upgrade.moneyPerWave)
            {
                upgradeUI.currentStatValues[currentStat].text = towerType.moneyPerWave.ToString();
                upgradeUI.upgradeStatValues[currentStat].text = upgrade.moneyPerWave.ToString();
                upgradeUI.statIcons[currentStat].sprite = statSprites[3];
                currentStat++;
            }

            if (towerType.discount < upgrade.discount)
            {
                upgradeUI.currentStatValues[currentStat].text = towerType.discount.ToString() + "%";
                upgradeUI.upgradeStatValues[currentStat].text = upgrade.discount.ToString() + "%";
                upgradeUI.statIcons[currentStat].sprite = statSprites[4];
                currentStat++;
            }




            for (int i = currentStat; i < upgradeUI.statIcons.Length; i++)
            {
                upgradeUI.currentStatValues[currentStat].text = towerType.range.ToString();
                upgradeUI.upgradeStatValues[currentStat].text = upgrade.range.ToString();
                upgradeUI.statIcons[currentStat].sprite = statSprites[3];
            }

            for (int i = currentStat; i < upgradeUI.currentStatValues.Length; i++)
            {
                upgradeUI.statArrows[i].SetActive(false);
                upgradeUI.currentStatValues[i].text = " ";
                upgradeUI.upgradeStatValues[i].text = " ";
                upgradeUI.statIcons[i].color = new Color(0, 0, 0, 0);
            }
        }
        else
        {
            for(int i = 0;i < upgradeUI.currentStatValues.Length; i++)
            {
                upgradeUI.statArrows[i].SetActive(false);
                upgradeUI.currentStatValues[i].text = " ";
                upgradeUI.upgradeStatValues[i].text = " ";
                upgradeUI.statIcons[i].color = new Color(0, 0, 0, 0);
            }
            upgradeUI.upgradeCostText.text = "MAX";
        }
        upgradeUI.towerName.text = towerType.towerName;
        upgradeUI.towerImage.sprite = towerType.towerIcon;
        upgradeUI.levelSlider.maxValue = towerType.maxTier;
        upgradeUI.levelSlider.value = towerType.currentTier;
        upgradeUI.levelText.text = towerType.currentTier.ToString();
        upgradeUI.sellText.text = towerType.sellValue.ToString();
    }

    public void DeSelect()
    {
        if (PauseClass.instance.isPaused) { return; }
        if (selectedObject == null) return;
        //Disable the Outline
        selectedObject.GetComponent<Outline>().enabled = false;
        if (selectedObject.GetComponent<RangeScript>()) { selectedObject.GetComponent<RangeScript>().ShowRange(false,false); }
            
        upgradeUI.gameObject.SetActive(false);
        selectedObject = null;
    }

    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    private static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == 5)
                return true;
        }
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
