using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseClass : MonoBehaviour
{
    public static PauseClass instance;
    [SerializeField] private GameObject pausescreen;
    [SerializeField] private GameObject[] settingEmpties;

    //[SerializeField] private GameSpeed gameSpeed;

    //Speed before pausing
    private float prevTimeSpeed = 1;

    public bool isPaused = false;

    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (Time.timeScale != 0) 
        {
            isPaused = true;
            prevTimeSpeed = Time.timeScale;
            pausescreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            //Resume game if game is paused already
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausescreen.SetActive(false);
        Time.timeScale = prevTimeSpeed;

        //Turns off all the settings tabs, so you dont have game setting active when you press esc again
        for (int i = 0; i < settingEmpties.Length; i++)
        {
            settingEmpties[i].SetActive(false);
        }
    }
}
