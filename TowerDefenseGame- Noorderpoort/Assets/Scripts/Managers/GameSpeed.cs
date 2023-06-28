using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeed : MonoBehaviour
{
    [SerializeField]GameObject button2x;
    [SerializeField] GameObject button1x;
    [SerializeField] Color activeColor;
    [SerializeField] Color inactiveColor;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSpeed(float amount)
    {
        Time.timeScale = amount;
        if (amount == 2)
        {
            button2x.GetComponent<Image>().color = activeColor;
            button1x.GetComponent<Image>().color = inactiveColor;
        }
        else if (amount == 1)
        {
            button1x.GetComponent<Image>().color = activeColor;
            button2x.GetComponent<Image>().color = inactiveColor;
        }
    }
}
