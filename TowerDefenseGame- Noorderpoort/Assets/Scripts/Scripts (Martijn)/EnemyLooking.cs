using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLooking : MonoBehaviour
{
    public Transform Base;
    public bool lookingAtBase = false;
    //GameObject voor enemy zichzelf

    void Update()
    {
        if (lookingAtBase)
        {
            transform.LookAt(Base);
            //transform.rotation = new Quaternion(transform.localEulerAngles.x, 180, transform.localEulerAngles.z,0);
        }
        //maakt zodat de enemy naar de gameObject (DefensePoint) kijkt
    }
}
