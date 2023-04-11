using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLooking : MonoBehaviour
{
    [SerializeField] Transform EnTarget;
    public bool lookingAtBase = false;
    //GameObject voor enemy zichzelf

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lookingAtBase)
        {
            gameObject.transform.LookAt(EnTarget);
        }
        //maakt zodat de enemy naar de gameObject (DefensePoint) kijkt
    }
}
