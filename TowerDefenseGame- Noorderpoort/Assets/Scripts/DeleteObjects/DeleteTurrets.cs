using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTurrets : MonoBehaviour {
    public static DeleteTurrets instance;
    [SerializeField] private GameObject turretEmpty;

    [SerializeField] private GameObject mainMenu;
    private GameObject destinationL1;
    private GameObject destinationL2;

    private bool resetGame;

    private Bitscript bitscript;
    private Healthscript healthscript;

    void Awake() {
        bitscript = FindObjectOfType<Bitscript>();
        healthscript = FindObjectOfType<Healthscript>();
        instance = this;
    }

    void Update() {
        //if you are in the main menu and stuff isnt reset, reset them
        if (mainMenu.activeSelf && !resetGame) {
            resetGame = true;

            FullReset();
            
        }
        else {
            resetGame = false;
        }
    }

    public void FullReset()
    {
        RemoveTurrets();
        RemoveLevels();
        ResetStuff();
    }

    public void RemoveTurrets() {
        foreach (Transform child in turretEmpty.transform) {

            //Removes any range circles, then destroys the object
            if (child.gameObject.GetComponent<RangeScript>()) { child.gameObject.GetComponent<RangeScript>().ShowRange(false, false); }
            Destroy(child.gameObject);
        }
    }

    public void RemoveLevels() {

        //Grab both levels
        destinationL1 = GameObject.Find("Level 1(Clone)");
        destinationL2 = GameObject.Find("Level 2(Clone)");

        //Destroy them, even if they dont exist
        Destroy(destinationL1);
        Destroy(destinationL2);
    }

    // Reset the money and health
    public void ResetStuff() {

        //Resets money and discount
        bitscript.bitIndex = bitscript.starterMoney;
        bitscript.discountAmount = 0;
        if (ShopReferences.Instance) { ShopReferences.Instance.UpdateCosts(); }

        //Sets health back
        healthscript.healthAmount = 11;
    }
}
