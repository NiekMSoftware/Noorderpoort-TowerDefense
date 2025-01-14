using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLooking : MonoBehaviour
{
    public Transform Base;
    public bool lookingAtBase = false;

    void Update()
    {
        if (lookingAtBase)
        {
            transform.LookAt(Base);
        }
    }
}
