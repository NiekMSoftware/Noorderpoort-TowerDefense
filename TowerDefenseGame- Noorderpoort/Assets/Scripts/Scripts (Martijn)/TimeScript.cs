using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class TimeScript : MonoBehaviour
{
    public float currentTimeInt = 0;
    public TMP_Text currentTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        currentTimeInt += Time.deltaTime;
        currentTime.SetText(currentTimeInt.ToString("0.00"));
    }
}

