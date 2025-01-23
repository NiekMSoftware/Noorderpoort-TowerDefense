using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;

public class Healthscript : MonoBehaviour
{
    [Header("Health")]
    //This int is the amount of health you have
    public int healthAmount;

    [Header("UI")]
    [SerializeField] private Image heartImage;
    [SerializeField] private Sprite[] heartSprites;
    [SerializeField] private TMP_Text healthText;


    public bool isDead = false;
    public static Healthscript instance;

    private void Start()
    {
        //This matches the health to the length of the array
        healthAmount = heartSprites.Length - 1;
        instance = this;
    }



    void Update()
    {
        if(healthAmount <= 0)
        {
            healthAmount = 0;

            //So you dont die multiple times
            if (isDead == false)
            {
                healthText.text = healthAmount.ToString();
                isDead = true;
                FindObjectOfType<EndGame>().BlueScreen();
                
            }
        }

        //Updates the UI
        if (healthAmount >= 0 && healthAmount < heartSprites.Length) 
        { 
            heartImage.sprite = heartSprites[healthAmount];
        }
        healthText.text = healthAmount.ToString();
    }
}