using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseClass : MonoBehaviour
{
    public GameObject pausescreen;

    public GameSpeed gameSpeed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("paused"); 
            PauseGame();
        }
    }

    public void PauseGame()
    {
       pausescreen.SetActive(true);
       Time.timeScale = 0f; 
    }

    public void ResumeGame()
    {
        pausescreen.SetActive(false);

        if (this.gameSpeed.twoTimeSpeed) {
            Time.timeScale = 2f;
        }
        else {
            Time.timeScale = 1f;
        }
    }
}
