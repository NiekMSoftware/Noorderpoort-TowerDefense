using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLooking : MonoBehaviour
{
    [SerializeField] Transform EnTarget;
    //GameObject voor enemy zichzelf

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(EnTarget);
        //maakt zodat de enemy naar de gameObject (DefensePoint) kijkt
    }
}
