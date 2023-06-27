using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTurrets : MonoBehaviour {
    [SerializeField] GameObject obj;

    [SerializeField] GameObject mainMenu;
    GameObject destination;

    bool removedKids;

    Bitscript _bitscript;
    Healthscript _healthscript;

    void Awake() {
        this._bitscript = FindObjectOfType<Bitscript>();
        this._healthscript = FindObjectOfType<Healthscript>();
    }

    void Update() {
        if (this.mainMenu.activeSelf && !this.removedKids) {
            this.removedKids = true;
            
            this.RemoveKids();
            this.RemoveLife();
            this.ResetStuff();
        }
        else {
            this.removedKids = false;
        }
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
    void RemoveLife() {
        destination = GameObject.Find("Level 1(Clone)");
        Destroy(this.destination);
    }

    // Reset the money and health
    void ResetStuff() {
        this._bitscript.BitIndex = this._bitscript.starterMoney;
        this._healthscript.HealthIndex = 11;
    }
}
