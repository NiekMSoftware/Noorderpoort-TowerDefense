using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    //string dif = difficulty
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    public void Savestring(string difficulty)
    {
        PlayerPrefs.SetString("dif", difficulty);
    }
    public string getstring(string name)
    {
        return PlayerPrefs.GetString(name);    
    }
}
