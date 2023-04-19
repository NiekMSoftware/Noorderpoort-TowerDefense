using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyRangeScript : MonoBehaviour
{
    public GameObject MTRange;
    public bool MTInRange = false;
    public List<GameObject> MTInRangeList = new List<GameObject>();
    public Material MTIRMat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MTInRangeList.Count == 0)
        {
            MTInRange = false;
        }
        else
        {
            MTInRange = true;
        }
    }
}
