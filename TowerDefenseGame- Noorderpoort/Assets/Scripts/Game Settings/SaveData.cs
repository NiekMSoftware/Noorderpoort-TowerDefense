using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public void Savestring(string difficulty)
    {
        PlayerPrefs.SetString("dif", difficulty);
    }
    public string getstring(string name)
    {
        return PlayerPrefs.GetString(name);    
    }
    public void SaveInt(int amount,string name)
    {
        PlayerPrefs.SetInt(name, amount);
    }
    public int GetInt(string name)
    {
        return PlayerPrefs.GetInt(name);
    }
}
