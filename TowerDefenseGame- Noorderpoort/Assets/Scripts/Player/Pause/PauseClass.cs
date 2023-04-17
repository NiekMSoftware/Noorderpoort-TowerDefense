using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseClass : MonoBehaviour
{
    public GameObject pausescreen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
                ResumeGame();
            else
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
        Time.timeScale = 1f;
    }
}
