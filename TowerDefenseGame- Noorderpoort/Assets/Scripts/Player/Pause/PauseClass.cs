using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseClass : MonoBehaviour
{
    public static PauseClass instance;
    public GameObject pausescreen;
    [SerializeField] private GameObject[] settingEmpties;

    public GameSpeed gameSpeed;

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
            print("paused"); 
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (Time.timeScale != 0) 
        {
            print(gameObject.transform.parent.name);
            isPaused = true;
            prevTimeSpeed = Time.timeScale;
            pausescreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausescreen.SetActive(false);
        Time.timeScale = prevTimeSpeed;
        for (int i = 0; i < settingEmpties.Length; i++)
        {
            settingEmpties[i].SetActive(false);
        }
    }
}
