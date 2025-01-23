using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour
{
    public static Selection instance;

    [Header("Selected Object Things")]
    public GameObject selectedObject;
    public bool selectedTower;

    [Space]
    [SerializeField] private BuildingManager buildingManager;

    public UpgradeUIReferences upgradeUI;

    //Another script grabs these so they always use the same icons
    public Sprite[] statSprites;
    [SerializeField] private Bitscript bits;

    private void Start()
    {
        instance = this;
         
        bits = FindObjectOfType<Bitscript>();
        buildingManager = FindObjectOfType<BuildingManager>();
    }

    void Update()
    {
        //Check the input for the mouse
        if (Input.GetMouseButtonDown(0) && buildingManager.isPlacementMode == false)
        {
            //if you are over ui, dont select anything
            if(IsPointerOverUIElement()) { return; }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
             
            if (Physics.Raycast(ray, out hit, 1000))
            {
                //Check if the player selected a tower or trap
                if (hit.collider.gameObject.CompareTag("Tower") || hit.collider.gameObject.CompareTag("Trap"))
                {
                    selectedTower = true;
                    Select(hit.collider.gameObject);
                }
                //if it isnt, and you arent in ui: Deselect
                else if(!IsPointerOverUIElement())
                {
                    selectedTower = false;
                    DeSelect();
                }
            }
        }
         
        //The secret button no one knows about
        if (Input.GetMouseButton(1) && selectedObject != null && buildingManager.isPlacementMode == false)
        {
            selectedTower = false;
            DeSelect();
        }
    }

    /// <summary>
    /// Select a tower
    /// </summary>
    /// <param name="obj"></param>
    public void Select(GameObject obj)
    {
        //Cant select if the game is paused, or if you are pressing a selected object
        if (PauseClass.instance.isPaused) { return; }
        if (obj == selectedObject) {  return; }
         
        if (selectedObject != null)
            DeSelect();
         
        //If you are placing but and arent trying to select your placing object then dont
        if(buildingManager.isPlacementMode && obj != buildingManager.pendingObject) { return; }
         
        //Turn on the tower range
        if (obj.GetComponent<RangeScript>()) { obj.GetComponent<RangeScript>().ShowRange(true,false); }
         
        Outline outline = obj.GetComponent<Outline>();
         
        //Check if the object needs an outline, and turn it on
        if (outline == null) { obj.AddComponent<Outline>(); }
        else
        {
            outline.enabled = true;
        }
         
        selectedObject = obj;
        UpdateUpgradeUI(obj.GetComponent<GeneralTowerScript>().towerStats);
    }

    public void SellTower()
    {
        //You cant sell a tower if the game is paused, or if you dont have a tower selected
        if (PauseClass.instance.isPaused) { return; }
        if (selectedObject != null)
        {
            //Remove any discount it had, and the shop ui according to that
            bits.RemoveDiscount(selectedObject.GetComponent<GeneralTowerScript>().towerStats.discount);
            ShopReferences.Instance.UpdateCosts();
             
            //Give money and destroy
            bits.AddBits(selectedObject.GetComponent<GeneralTowerScript>().towerStats.sellValue);
            Destroy(selectedObject);
             
            DeSelect();
        }
    }

    private void UpdateUpgradeUI(TowerScriptable towerType)
    {
        //The ui only supports 3 different stats
        int currentStat = 0;
         
        //To compare
        TowerScriptable upgrade;
         
        if (upgradeUI == null) { return; }
         
        upgradeUI.gameObject.SetActive(true);
         
        //Compare stats if the tower can upgrade
        if (towerType.canUpgrade) {
             
            upgrade = towerType.upgradeScriptable;
             
            //Set everything to nothing
            for (int i = currentStat; i < upgradeUI.currentStatValues.Length; i++)
            {
                upgradeUI.statArrows[i].SetActive(true);
                upgradeUI.currentStatValues[i].text = " ";
                upgradeUI.currentStatValues[i].color = new Color(1, 0, 0, 1);
                upgradeUI.upgradeStatValues[i].text = " ";
                upgradeUI.statIcons[i].color = new Color(1, 1, 1, 1);
            }
             
            upgradeUI.upgradeCostText.text = Bitscript.instance.CalculateWithDiscount(upgrade.cost).ToString();

            //These are all different stats, this one has comments
            if (towerType.damage < upgrade.damage)
            {
                //Technically resistor has "9999999999999" damage, but thats practically infinite
                if (towerType.damage > 999)
                {
                    upgradeUI.currentStatValues[currentStat].text = "Infinite";
                }
                else
                {
                    upgradeUI.currentStatValues[currentStat].text = towerType.damage.ToString();
                }
                upgradeUI.upgradeStatValues[currentStat].text = upgrade.damage.ToString();
                upgradeUI.statIcons[currentStat].sprite = statSprites[0];
                currentStat++;
            }
            if (towerType.firerate > upgrade.firerate)
            {
                upgradeUI.currentStatValues[currentStat].text = (towerType.firerate/5).ToString() + "s";
                upgradeUI.upgradeStatValues[currentStat].text = (upgrade.firerate/5).ToString() + "s";
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

            //Attacking towers dont have these stats
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

            //Turn off all the empty spots
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
            //Check upper code, this is practically a copy that doesnt do upgrade stuff. This could be done better.
            for(int i = 0;i < upgradeUI.currentStatValues.Length; i++)
            {
                upgradeUI.statArrows[i].SetActive(false);
                upgradeUI.currentStatValues[i].text = " ";
                upgradeUI.currentStatValues[i].color = new Color(1, 1, 1, 1);
                upgradeUI.upgradeStatValues[i].text = " ";
                upgradeUI.statIcons[i].color = new Color(1, 1, 1, 1);
            }
            if (towerType.damage > 0 && currentStat < 3)
            {
                if (towerType.damage > 999)
                {
                    upgradeUI.currentStatValues[currentStat].text = "Infinite";
                }
                else
                {
                    upgradeUI.currentStatValues[currentStat].text = towerType.damage.ToString();
                }
                upgradeUI.statIcons[currentStat].sprite = statSprites[0];
                currentStat++;
            }
            if (towerType.firerate > 0 && currentStat < 3)
            {
                upgradeUI.currentStatValues[currentStat].text = (towerType.firerate / 5).ToString() + "s";
                upgradeUI.statIcons[currentStat].sprite = statSprites[1];
                currentStat++;
            }
            if (towerType.range > 0 && currentStat < 3)
            {
                upgradeUI.currentStatValues[currentStat].text = towerType.range.ToString();
                upgradeUI.statIcons[currentStat].sprite = statSprites[2];
                currentStat++;
            }
            if (towerType.moneyPerWave > 0 && currentStat < 3)
            {
                upgradeUI.currentStatValues[currentStat].text = towerType.moneyPerWave.ToString();
                upgradeUI.statIcons[currentStat].sprite = statSprites[3];
                currentStat++;
            }

            if (towerType.discount > 0 && currentStat < 3)
            {
                upgradeUI.currentStatValues[currentStat].text = towerType.discount.ToString() + "%";
                upgradeUI.statIcons[currentStat].sprite = statSprites[4];
                currentStat++;
            }
            if (towerType.detectionRange > 0 && currentStat < 3)
            {
                upgradeUI.currentStatValues[currentStat].text = towerType.detectionRange.ToString();
                upgradeUI.statIcons[currentStat].sprite = statSprites[5];
                currentStat++;
            }
            if (towerType.uses > 0 && currentStat < 3)
            {
                upgradeUI.currentStatValues[currentStat].text = towerType.uses.ToString();
                upgradeUI.statIcons[currentStat].sprite = statSprites[6];
                currentStat++;
            }
            if (towerType.cooldown > 0 && currentStat < 3)
            {
                upgradeUI.currentStatValues[currentStat].text = towerType.cooldown.ToString();
                upgradeUI.statIcons[currentStat].sprite = statSprites[7];
                currentStat++;
            }

            for (int i = currentStat; i < upgradeUI.currentStatValues.Length; i++)
            {
                upgradeUI.statArrows[i].SetActive(false);
                upgradeUI.currentStatValues[i].text = " ";
                upgradeUI.upgradeStatValues[i].text = " ";
                upgradeUI.statIcons[i].color = new Color(0, 0, 0, 0);
            }

            //If a tower cant upgrade it must be max upgrade.
            upgradeUI.upgradeCostText.text = "MAX";
        }

        //Sets the basic settings
        upgradeUI.towerName.text = towerType.towerName;
        upgradeUI.towerImage.sprite = towerType.towerIcon;
        upgradeUI.levelSlider.maxValue = towerType.maxTier;
        upgradeUI.levelSlider.value = towerType.currentTier;
        upgradeUI.levelText.text = towerType.currentTier.ToString();
        upgradeUI.sellText.text = towerType.sellValue.ToString();
    }

    public void DeSelect()
    {
        //Cant deselect if paused, or if you have nothing selected
        if (PauseClass.instance.isPaused) { return; }
        if (selectedObject == null) return;
         
        //Disable the Outline
        selectedObject.GetComponent<Outline>().enabled = false;
         
        //Disable any range
        if (selectedObject.GetComponent<RangeScript>()) { selectedObject.GetComponent<RangeScript>().ShowRange(false,false); }
         
        upgradeUI.gameObject.SetActive(false);
        selectedObject = null;
    }

    //Below is not my code vvv (I do understand it tho)


    /// <summary>
    /// Returns 'true' if we touched or hovering on Unity UI element.
    /// </summary>
    /// <returns></returns>
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
