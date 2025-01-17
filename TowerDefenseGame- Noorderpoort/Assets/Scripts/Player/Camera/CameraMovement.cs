using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera panning settings")]
    public float panSpeed = 20f;
    public float panBorderThickness = 20f;
    public Vector2 panLimit;
    
    [Header("Scroll Settings")]
    public float scrollSpeed;
    public float minY = 20f;
    public float maxY = 120f;

    [Header("Starting Pos")]
    [SerializeField] private Vector3 startPos;

    [SerializeField] private TMP_Text sliderText;

    [SerializeField] private GameObject mainMenu;
    private void Start()
    {
        ResetToStartPos();
    }
    private void Update()
    {
        if (PauseClass.instance && PauseClass.instance.isPaused) { return; }
        if (!mainMenu.activeInHierarchy) { return; }
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
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

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 50f * (Time.deltaTime / Time.timeScale);
        if(pos.y !>= minY + 1)
        {
            pos.z += scroll * scrollSpeed * 100f * (Time.deltaTime / Time.timeScale);
        }

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
