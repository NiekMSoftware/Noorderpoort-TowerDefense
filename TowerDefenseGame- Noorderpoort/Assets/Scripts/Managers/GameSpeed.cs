using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameSpeed : MonoBehaviour
{
    [SerializeField]
    Image[] buttons;
    [SerializeField] GameObject button2x;
    [SerializeField] GameObject button1x;
    [SerializeField] Color activeColor;
    [SerializeField] Color inactiveColor;
    
    public bool oneTimeSpeed = true;
    public bool twoTimeSpeed;
    void Start()
    {
        button1x.GetComponent<Image>().color = activeColor;
        button2x.GetComponent<Image>().color = inactiveColor;
    }

    public void ChangeSpeed(float amount)
    {
        Time.timeScale = amount;
        if (amount == 2)
        {
            button2x.GetComponent<Image>().color = activeColor;
            button1x.GetComponent<Image>().color = inactiveColor;

            this.oneTimeSpeed = false;
            this.twoTimeSpeed = true;
        }
        else if (amount == 1)
        {
            button1x.GetComponent<Image>().color = activeColor;
            button2x.GetComponent<Image>().color = inactiveColor;

            this.twoTimeSpeed = false;
            this.oneTimeSpeed = true;
        }
    }
}
