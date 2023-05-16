using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScript : MonoBehaviour
{
    [Header("Difficulties")]
    public bool Difeasy;
    public bool Difnormal;
    public bool Difhard;
    public bool Difinsane;

    string difficulty;
    SaveData savedata;
    
    // Start is called before the first frame update
    void Start()
    {
        savedata = FindObjectOfType<SaveData>();
        difficulty = savedata.getstring("dif");
        
        //Set everything to false at first
        Difeasy = false;
        Difnormal = false;
        Difhard = false;
        Difinsane = false;
        /*if (difficulty == "Easy")
        {

        }*/
    }
    
    public void Difficulties(int difficultyPreset)
    {
        //Make a switch for different difficulties
        switch (difficultyPreset)
        {
            case 0:
                Difeasy = true;
                print("switched to easy");
                break;
            case 1:
                Difnormal = true;
                print("switched to normal");
                break;
            case 2:
                Difhard = true;
                print("switched to hard");
                break;
            case 3:
                Difinsane = true;
                print("switched to insane");
                break;
        }
    }
}
