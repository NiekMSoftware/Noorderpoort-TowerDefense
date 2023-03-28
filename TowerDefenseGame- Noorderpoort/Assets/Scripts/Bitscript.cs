using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bitscript : MonoBehaviour
{
    public int BitIndex;
    public TMP_Text BitAmount;

    // Start is called before the first frame update
    void Start()
    {
        BitIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
        Invoke("AddBits", 0f);
        }
        BitAmount.text = BitIndex.ToString();
    }
    public void AddBits()
    {
        BitIndex++;
    }
}
