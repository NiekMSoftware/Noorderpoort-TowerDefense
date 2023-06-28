using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeScript : MonoBehaviour {
    public float timeRemaining = 0;
    public bool timeIsRunning;
    public TMP_Text textTime;
   
    // Start is called before the first frame update
    void Start() {
        this.timeIsRunning = true;
    }

    void Update()
    {
        if (this.timeIsRunning) {
            if (this.timeRemaining >= 0) {
                this.timeRemaining += Time.deltaTime;
                DisplayTime(this.timeRemaining);
            }
        }
    }

    void DisplayTime(float timeToDisplay) {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        this.textTime.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    public void ResetTime() {
        this.timeRemaining = 0;
    }
}

