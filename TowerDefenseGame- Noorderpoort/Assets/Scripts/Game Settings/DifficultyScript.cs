using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScript : MonoBehaviour
{
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
        Difeasy = false;
        Difnormal = false;
        Difhard = false;
        Difinsane = false;
        /*if (difficulty == "Easy")
        {

        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
