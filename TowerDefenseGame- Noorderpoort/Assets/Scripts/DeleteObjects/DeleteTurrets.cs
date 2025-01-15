using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTurrets : MonoBehaviour {
    public static DeleteTurrets instance;
    [SerializeField] GameObject obj;

    [SerializeField] GameObject mainMenu;
    GameObject destinationL1;
    GameObject destinationL2;

    bool removedKids;

    Bitscript _bitscript;
    Healthscript _healthscript;

    void Awake() {
        this._bitscript = FindObjectOfType<Bitscript>();
        this._healthscript = FindObjectOfType<Healthscript>();
        instance = this;
    }

    void Update() {
        if (this.mainMenu.activeSelf && !this.removedKids) {
            this.removedKids = true;

            FullReset();
            
        }
        else {
            this.removedKids = false;
        }
    }

    public void FullReset()
    {
        this.RemoveKids();
        this.RemoveLife();
        this.ResetStuff();
    }

    // Remove children of parent
    public void RemoveKids() {
        foreach (Transform child in obj.transform) {
            // Pull out a glock at your local school
            Destroy(child.gameObject);
            print("Delete kid");
        }
    }

    // Remove the destination and spawner
    public void RemoveLife() {
        this.destinationL1 = GameObject.Find("Level 1(Clone)");
        this.destinationL2 = GameObject.Find("Level 2(Clone)");
        Destroy(this.destinationL1);
        Destroy(this.destinationL2);
    }

    // Reset the money and health
    public void ResetStuff() {
        this._bitscript.bitIndex = this._bitscript.starterMoney;
        _bitscript.discountAmount = 0;
        ShopReferences.Instance.UpdateCosts();
        this._healthscript.HealthIndex = 11;
    }
}
