using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptRude : MonoBehaviour
{
    //Test Script

    float timeTillTeleport = 10;
    Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0,-0.01f);
        timeTillTeleport -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.position = spawnPos;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            gameObject.transform.position = spawnPos;
        }
    }
}
