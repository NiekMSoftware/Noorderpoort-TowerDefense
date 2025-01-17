using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonReferences : MonoBehaviour
{
    public TMP_Text[] currentStatValues = new TMP_Text[3];
    public Image[] statIcons = new Image[3];
    public TMP_Text towerName;

    public void UpdateUI(TowerScriptable towerType)
    {
        int currentStat = 0;
        for (int i = 0; i < currentStatValues.Length; i++)
        {
            currentStatValues[i].text = " ";
            currentStatValues[i].color = new Color(1, 1, 1, 1);
            statIcons[i].color = new Color(1, 1, 1, 1);
        }

        towerName.text = towerType.towerName;

        if (towerType.damage > 0 && currentStat < 3)
        {
            if(towerType.damage > 999)
            {
                currentStatValues[currentStat].text = "Infinite";
            }
            else
            {
                currentStatValues[currentStat].text = towerType.damage.ToString();
            }
            
            statIcons[currentStat].sprite = Selection.instance.statSprites[0];
            currentStat++;
        }
        if (towerType.firerate > 0 && currentStat < 3)
        {
            currentStatValues[currentStat].text = (towerType.firerate / 5).ToString() + "s";
            statIcons[currentStat].sprite = Selection.instance.statSprites[1];
            currentStat++;
        }
        if (towerType.range > 0 && currentStat < 3)
        {
            currentStatValues[currentStat].text = towerType.range.ToString();
            statIcons[currentStat].sprite = Selection.instance.statSprites[2];
            currentStat++;
        }
        if (towerType.moneyPerWave > 0 && currentStat < 3)
        {
            currentStatValues[currentStat].text = towerType.moneyPerWave.ToString();
            statIcons[currentStat].sprite = Selection.instance.statSprites[3];
            currentStat++;
        }

        if (towerType.discount > 0 && currentStat < 3)
        {
            currentStatValues[currentStat].text = towerType.discount.ToString() + "%";
            statIcons[currentStat].sprite = Selection.instance.statSprites[4];
            currentStat++;
        }

        if (towerType.detectionRange > 0 && currentStat < 3)
        {
            currentStatValues[currentStat].text = towerType.detectionRange.ToString();
            statIcons[currentStat].sprite = Selection.instance.statSprites[5];
            currentStat++;
        }
        if (towerType.uses > 0 && currentStat < 3)
        {
            currentStatValues[currentStat].text = towerType.uses.ToString();
            statIcons[currentStat].sprite = Selection.instance.statSprites[6];
            currentStat++;
        }
        if (towerType.cooldown > 0 && currentStat < 3)
        {
            currentStatValues[currentStat].text = towerType.cooldown.ToString();
            statIcons[currentStat].sprite = Selection.instance.statSprites[7];
            currentStat++;
        }

        for (int i = currentStat; i < currentStatValues.Length; i++)
        {
            currentStatValues[i].text = " ";
            statIcons[i].color = new Color(0, 0, 0, 0);
        }
        for (int i = currentStat; i < currentStatValues.Length; i++)
        {
            currentStatValues[i].text = " ";
            statIcons[i].color = new Color(0, 0, 0, 0);
        }
    }
}
