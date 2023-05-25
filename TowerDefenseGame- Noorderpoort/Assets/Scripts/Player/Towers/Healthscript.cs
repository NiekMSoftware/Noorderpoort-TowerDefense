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
    public int HealthIndex;
    public Image Heart;
    public Sprite[] Spritearray;
    public TMP_Text HealthAmount;
    
    //When the health was set to 0 it would still display 1 so i made another TMP text wich it toggles on and off based on the health value
    public GameObject NormalHealth;
    public GameObject ZeroHealth;

    // Start is called before the first frame update
    private void Start()
    {
        //This matches the health to the length of the array
        HealthIndex = Spritearray.Length - 1;
    }



    // Update is called once per frame
    void Update()
    {
        if(HealthIndex <= 0)
        {
            NormalHealth.SetActive(false);
            ZeroHealth.SetActive(true);
            FindObjectOfType<SceneLoader>().LoadScene("Main Menu");
        }
        else
        {
            NormalHealth.SetActive(true);
            ZeroHealth.SetActive(false);
        }

        //If the HealthIndex goes below 0 it will give a error so im only allowing it to function if it is larger or equal to 0
        if (HealthIndex >= 0) 
        { 
            Heart.sprite = Spritearray[HealthIndex];
        }
        //This converts the amount of health you have into text
        HealthAmount.text = HealthIndex.ToString();

        //I made this to test if it would actually update
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HealthIndex--;
        }
        
    }
}