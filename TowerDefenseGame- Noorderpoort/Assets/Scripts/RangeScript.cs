using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeScript : MonoBehaviour
{
    public GameObject inRange;
    public bool enemyInRange = false;
    public List<GameObject> inRangeList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            inRangeList.Add(other.gameObject);
            inRange = other.gameObject;
            enemyInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            inRangeList.Remove(other.gameObject);
            inRange = null;
            enemyInRange = false;
        }
    }
}
