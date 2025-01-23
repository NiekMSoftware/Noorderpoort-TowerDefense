using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera panning settings")]
    [SerializeField] private float panSpeed = 20f;
    [SerializeField] private Vector2 panLimit;
    
    [Header("Scroll Settings")]
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float minY = 20f;
    [SerializeField] private float maxY = 120f;

    [Header("Misc")]
    [SerializeField] private Vector3 startPos;
    [SerializeField] private TMP_Text sliderText;
    [SerializeField] private GameObject mainMenu;

    private void Start()
    {
        ResetToStartPos();
    }
    private void Update()
    {
        //If the game is paused or you are in the main menu you cannot move the camera
        if (PauseClass.instance && PauseClass.instance.isPaused) { return; }
        if (!mainMenu.activeInHierarchy) { return; }

        //makes a seperate vector3 to adjust for movement
        Vector3 pos = transform.position;

        //(bad) input
        if (Input.GetKey("w"))
        {
            //Unaffected by timescale
            pos.z += panSpeed * (Time.deltaTime/Time.timeScale);
        }
        if (Input.GetKey("s"))
        {
            pos.z -= panSpeed * (Time.deltaTime / Time.timeScale);
        }
        if (Input.GetKey("d"))
        {
            pos.x += panSpeed * (Time.deltaTime / Time.timeScale);
        }
        if (Input.GetKey("a"))
        {
            pos.x -= panSpeed * (Time.deltaTime / Time.timeScale);
        }

        //Scroll with the mouse to zoom in
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 50f * (Time.deltaTime / Time.timeScale);
        if(pos.y !>= minY + 1)
        {
            pos.z += scroll * scrollSpeed * 100f * (Time.deltaTime / Time.timeScale);
        }

        //Clamp in an area
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        
        transform.position = pos;
    }

    public void ResetToStartPos()
    {
        transform.position = startPos;
    }

    public void SetCameraSpeed(float speed)
    {
        panSpeed = speed;
        sliderText.text = speed.ToString();
    }
}
