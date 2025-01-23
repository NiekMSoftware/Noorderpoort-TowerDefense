using UnityEngine;
using TMPro;

public class TimeScript : MonoBehaviour {
    public float currentTime = 0;
    public bool timeIsRunning;
    public TMP_Text textTime;
   
    void Start() {
        //Start counting time when script spawns in, which is at the same time as the level
        timeIsRunning = true;
    }

    void Update()
    {
        if (timeIsRunning) {
            if (currentTime >= 0) {
                currentTime += Time.deltaTime;
                DisplayTime(currentTime);
            }
        }
    }

    void DisplayTime(float timeToDisplay) {
        //Getting the time in minutes/seconds
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        textTime.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    /// <summary>
    /// Sets time to 0. Self explanatory
    /// </summary>
    public void ResetTime() {
        currentTime = 0;
    }
}

