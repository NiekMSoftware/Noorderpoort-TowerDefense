using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeScript : MonoBehaviour
{
    public GameObject inRange;
    public bool enemyInRange = false;
    public List<GameObject> inRangeList = new List<GameObject>();
    public Material rangeMat;
    void Update()
    {
        if (inRangeList.Count == 0)
        {
            enemyInRange = false;
        }
        else
        {
            enemyInRange = true;
        }
        gameObject.GetComponent<Renderer>().material = rangeMat;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            inRangeList.Add(other.gameObject);
            inRange = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            inRangeList.Remove(other.gameObject);
            inRange = null;
        }
    }
}
