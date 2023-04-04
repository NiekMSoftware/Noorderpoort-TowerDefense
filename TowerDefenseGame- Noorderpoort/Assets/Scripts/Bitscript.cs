using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bitscript : MonoBehaviour
{
    public int BitIndex;
    //UI doesnt work for me - Rudo
    void Start()
    {
        BitIndex = 0;
    }
    public void AddBits(int amount)
    {
        BitIndex = BitIndex + amount;
    }
}
