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
            gameObject.transform.LookAt(Base);
        }
        //maakt zodat de enemy naar de gameObject (DefensePoint) kijkt
    }
}
