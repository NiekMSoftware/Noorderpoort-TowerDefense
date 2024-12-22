using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseClass : MonoBehaviour
{
    public GameObject pausescreen;

    public GameSpeed gameSpeed;

    private float prevTimeSpeed = 1;

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
        prevTimeSpeed = Time.timeScale;
        Time.timeScale = 0f; 
    }

    public void ResumeGame()
    {
        pausescreen.SetActive(false);
        Time.timeScale = prevTimeSpeed;
    }
}
