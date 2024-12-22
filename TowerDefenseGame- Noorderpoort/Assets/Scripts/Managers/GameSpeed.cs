using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeed : MonoBehaviour
{
    [SerializeField]
    Image[] buttons;
    [SerializeField] Color activeColor;
    [SerializeField] Color inactiveColor;
    void Start()
    {
        foreach(var button in buttons)
        {
            button.color = inactiveColor;
        }
        buttons[0].color = activeColor;
    }

    public void ChangeSpeed(float amount)
    {
        Time.timeScale = amount;
        foreach (var button in buttons)
        {
            button.color = inactiveColor;
        }

        //This could 100% be made better but im too lazy
        switch (amount)
        {
            case 1:
                buttons[0].color = activeColor;
                break;
            case 2:
                buttons[1].color = activeColor;
                break;
            case 5:
                buttons[2].color = activeColor;
                break;
        }
    }
}
