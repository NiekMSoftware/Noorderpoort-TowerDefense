using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLooking : MonoBehaviour
{
    [SerializeField] Transform EnTarget;
    public bool lookingAtBase = false;
    //GameObject voor enemy zichzelf

    void Update()
    {
        if (lookingAtBase)
        {
            gameObject.transform.LookAt(EnTarget);
        }
        //maakt zodat de enemy naar de gameObject (DefensePoint) kijkt
    }
}
